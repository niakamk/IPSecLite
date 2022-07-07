using System;
using adabtek.IPsecLite.EventArguments;
using adabtek.IPsecLite.IKEv2;
using adabtek.IPsecLite.NetworkProtocols;
using adabtek.IPsecLite.SADB;
using adabtek.IPsecLite.SPDB;
using adabtek.IPsecLite.Types;
using adabtek.IPsecLite.Utilities;
using adabtek.IPsecLite.Constants;
namespace adabtek.IPsecLite.IKEExchange
{
    public class IKEv2Exchange
    {
        public delegate void IKEMessageProcessingStartedHandler(object sender, IKEMessageProcessingStartedEventArgs e);
        public delegate void IKEMessageProcessingFinishedHandler(object sender, IKEMessageProcessingFinishedEventArgs e);
        public delegate void IKERawMessageArrivedHandler(object sender, NetworkDataEventArgs e);
        public delegate void IKEMessageArrivedHandler(object sender, IKEMessageArrivedEventArgs e);
        public delegate void IKEMessageSentHandler(object sender, NetworkDataEventArgs e);
        public delegate void IKESAEstablishedHandler(object sender, IKESAEstablishedEventArgs e);

        public event IKEMessageProcessingStartedHandler IKEMessageProcessingStarted;
        public event IKEMessageProcessingFinishedHandler IKEMessageProcessingFinished;
        public event IKEMessageArrivedHandler IKEMessageArrived;
        public event IKERawMessageArrivedHandler IKERawMessageArrived;
        public event IKEMessageSentHandler IKEMessageSent;
        public event IKESAEstablishedHandler IKESAEstablished;

        public IKEv2Exchange()
        {
            Program.UDP.IKEMessageArrived +=new UDP.IKEMessageArrivedHandler(UDP_IKEMessageArrived);
            SA.SADeleted += new SA.SADeletedHandler(SA_SADeleted);
        }

        void SA_SADeleted(object sender, long SAKey)
        {
            SA.Delete(SAKey);
        }

        void UDP_IKEMessageArrived(object sender, NetworkDataEventArgs e)
        {
            IKE ikeMessage = IKE.TryParse(e.NetworkData);

            if (ikeMessage == null)
                return;

            if (IKERawMessageArrived != null)
                IKERawMessageArrived(this, e);

            if (IKEMessageArrived != null)
                IKEMessageArrived(this, new IKEMessageArrivedEventArgs(ikeMessage, e.SourceIP, e.DestinationIP));

            switch (ikeMessage.ExchangeType)
            {
                case IKE_EXCHANGE_TYPES.INIT:
                    {
                        switch (ikeMessage.Flags)
                        {
                            case 0x08: //0000 1000: request initiator
                                {
                                    if (IKEMessageProcessingStarted != null)
                                        IKEMessageProcessingStarted(this, new IKEMessageProcessingStartedEventArgs("INITi"));

                                    IKE_KE_Payload kePayload = new IKE_KE_Payload(IKE_PAYLOADS.N, ikeMessage.KeyExchangePayload.DHGroup);
                                    IKE_Nonce_Payload noncePayload = new IKE_Nonce_Payload(IKE_PAYLOADS.NONE, 256);

                                    IKE ikeINITr = new IKE();
                                    ikeINITr.INITr(ikeMessage);
                                    ikeINITr.AddPayload(ikeMessage.SAPayload, IKE_PAYLOADS.SA, ikeMessage.SAPayload.Length);
                                    ikeINITr.AddPayload(kePayload, IKE_PAYLOADS.KE, ikeMessage.KeyExchangePayload.Length);
                                    ikeINITr.AddPayload(noncePayload, IKE_PAYLOADS.N, ikeMessage.NoncePayload.Length);

                                    SA sa = new SA(ikeMessage, e.SourceIP, ikeINITr, e.DestinationIP);
                                    this.Send(ikeINITr, sa.ResponderIP, sa.InitiatorIP);
                                    sa.Status = SA_STATUS.INITr_SENT;
                                    //Responder is ready to calculate crypto keys for SA messages
                                    sa.SetKeys();
                                    if (IKEMessageProcessingFinished != null)
                                        IKEMessageProcessingFinished(this, new IKEMessageProcessingFinishedEventArgs("INITi"));
                                }
                                break;
                            case 0x20: //0010 0000: responder response
                                {
                                    if (IKEMessageProcessingStarted != null)
                                        IKEMessageProcessingStarted(this, new IKEMessageProcessingStartedEventArgs("INITr"));

                                    SA sa = SA.ProcessINIT(ikeMessage, ikeMessage.InitiatorSPI);
                                    //Initiator is ready to calculate crypto keys for SA messages
                                    sa.SetKeys();

                                    IKE_Encrypted_Payload encryptedPayload = new IKE_Encrypted_Payload();

                                    IKE_Notify_Payload notifyPayload = null;
                                    if (sa.Mode == IKE_MODE.TRANSPORT)
                                        notifyPayload = new IKE_Notify_Payload(IKE_PAYLOADS.IDi, IKE_PROTOCOLS.RESERVED, IKE_NOTIFY_MSG_TYPES.USE_TRANSPORT_MODE);

                                    //We use the default IP address to identify the Initiator.
                                    IKE_Identification_Payload idPayload = new IKE_Identification_Payload(sa.InitiatorIP, IKE_PAYLOADS.AUTH);

                                    byte[] authData = sa.AuthData(true, idPayload, "TODO:THIS NEEDS TO BE REPLACED.");
                                    IKE_Auth_Payload authPayload = new IKE_Auth_Payload(IKE_PAYLOADS.SA, authData);

                                    SPDEntry spdEntry = SPD.GetPolicy(Utils.ToShortStringIP(sa.ResponderIP));
                                    IKE_Proposal proposal = new IKE_Proposal(1, spdEntry.Protocol, true);
                                    IKE_Transform encrTransform = new IKE_Transform(false, IKE_TRANSFORM_TYPES.ENCR, (byte)spdEntry.EncrAlg);
                                    encrTransform.AddAttribute(new IKE_Transform_Attribute(IKE_ATTRIBUTE_TYPES.KEY_LENGTH, spdEntry.EncrKeyLength));
                                    if (spdEntry.EncrAlg == IKE_ENCR_ALGS.ENCR_DES3)
                                        encrTransform.AddAttribute(new IKE_Transform_Attribute(IKE_ATTRIBUTE_TYPES.BLOCK_SIZE, spdEntry.EncrBlockSize));
                                    IKE_Transform intgTransform = new IKE_Transform(false, IKE_TRANSFORM_TYPES.INTEG, (byte)spdEntry.IntgAlg);
                                    proposal.AddTransform(encrTransform, false);
                                    proposal.AddTransform(intgTransform, false);

                                    IKE_SA_Payload saPayload = new IKE_SA_Payload(IKE_PAYLOADS.NONE);
                                    saPayload.AddProposal(proposal);

                                    if (sa.Mode == IKE_MODE.TRANSPORT)
                                        encryptedPayload.AddInnerPayload(notifyPayload, IKE_PAYLOADS.NOTI, notifyPayload.Length);
                                    encryptedPayload.AddInnerPayload(idPayload, IKE_PAYLOADS.IDi, idPayload.Length);
                                    encryptedPayload.AddInnerPayload(authPayload, IKE_PAYLOADS.AUTH, authPayload.Length);
                                    encryptedPayload.AddInnerPayload(saPayload, IKE_PAYLOADS.SA, saPayload.Length);

                                    sa.AddChildSAProposal(saPayload.Proposals);

                                    sa.ProtectEncryptedPayload(encryptedPayload);

                                    IKE ikeMsg = new IKE();
                                    ikeMsg.AUTHi(ikeMessage.InitiatorSPI, ikeMessage.ResponderSPI);
                                    ikeMsg.AddPayload(encryptedPayload, IKE_PAYLOADS.ENCR, encryptedPayload.Length);

                                    sa.SendProtected(ikeMsg);

                                    if (IKEMessageProcessingFinished != null)
                                        IKEMessageProcessingFinished(this, new IKEMessageProcessingFinishedEventArgs("INITr"));

                                }
                                break;
                        }
                        break;
                    }
                case IKE_EXCHANGE_TYPES.AUTH:
                    {
                        switch (ikeMessage.Flags)
                        {
                            case 0x08: //0000 1000: request initiator
                                {
                                    if (IKEMessageProcessingStarted != null)
                                        IKEMessageProcessingStarted(this, new IKEMessageProcessingStartedEventArgs("AUTHi"));
                                    SA sa = SA.GetSA(ikeMessage.ResponderSPI);
                                    if (sa != null)
                                    {
                                        if (sa.ProcessAUTH(ikeMessage))
                                        {
                                            IKE_Encrypted_Payload encryptedPayload = new IKE_Encrypted_Payload();

                                            IKE_Identification_Payload idPayload = new IKE_Identification_Payload(sa.ResponderIP, IKE_PAYLOADS.AUTH);

                                            byte[] authData = sa.AuthData(false, idPayload, "TODO:THIS NEEDS TO BE REPLACED.");
                                            IKE_Auth_Payload authPayload = new IKE_Auth_Payload(IKE_PAYLOADS.SA, authData);

                                            IKE_SA_Payload saPayload = new IKE_SA_Payload(IKE_PAYLOADS.NONE);

                                            IKE_Proposal proposal = null;
                                            IKE_Notify_Payload notifyPayload = null;
                                            
                                            foreach (PAYLOAD_NODE_TYPE p in ikeMessage.EncryptedPayload.InnerPayloads)
                                                switch (p.PayloadType)
                                                {
                                                    case IKE_PAYLOADS.SA:
                                                        {
                                                            proposal = new IKE_Proposal(1, ((IKE_SA_Payload)p.Payload).Proposals[0].ProtocolID, true);
                                                            foreach (IKE_Transform transform in ((IKE_SA_Payload)p.Payload).Proposals[0].Transforms)
                                                                proposal.AddTransform(transform, false);
                                                        }
                                                        break;
                                                    case IKE_PAYLOADS.NOTI:
                                                        notifyPayload = new IKE_Notify_Payload(IKE_PAYLOADS.IDr, IKE_PROTOCOLS.RESERVED, IKE_NOTIFY_MSG_TYPES.USE_TRANSPORT_MODE);
                                                        break;
                                                }

                                            saPayload.AddProposal(proposal);

                                            if (notifyPayload != null)
                                            {
                                                encryptedPayload.AddInnerPayload(notifyPayload, IKE_PAYLOADS.NOTI, notifyPayload.Length);
                                                sa.Mode = IKE_MODE.TRANSPORT;
                                            }
                                            encryptedPayload.AddInnerPayload(idPayload, IKE_PAYLOADS.IDr, idPayload.Length);
                                            encryptedPayload.AddInnerPayload(authPayload, IKE_PAYLOADS.AUTH, authPayload.Length);
                                            encryptedPayload.AddInnerPayload(saPayload, IKE_PAYLOADS.SA, saPayload.Length);

                                            sa.AddChildSAProposal(saPayload.Proposals);

                                            sa.ProtectEncryptedPayload(encryptedPayload);
                                            IKE ikeMsg = new IKE();
                                            ikeMsg.AUTHr(ikeMessage);
                                            ikeMsg.AddPayload(encryptedPayload, IKE_PAYLOADS.ENCR, encryptedPayload.Length);

                                            sa.SendProtected(ikeMsg);

                                            SPD.NewSA(Utils.MinimizeIPLength(Utils.ToShortStringIP(sa.InitiatorIP)), sa.SAKey);

                                            sa.CreateChildSA();

                                            if (IKESAEstablished != null)
                                                IKESAEstablished(this, new IKESAEstablishedEventArgs(false, sa.InitiatorSPI, sa.ResponderSPI, sa.InitiatorIP, sa.ResponderIP));

                                            if (IKEMessageProcessingFinished != null)
                                                IKEMessageProcessingFinished(this, new IKEMessageProcessingFinishedEventArgs("AUTHi"));

                                        }
                                        else
                                            throw new Exception("Invalid AUTH message from the Inititor.");
                                    }
                                    break;
                                }
                            case 0x20: //0010 0000: responder response
                                {
                                    if (IKEMessageProcessingStarted != null)
                                        IKEMessageProcessingStarted(this, new IKEMessageProcessingStartedEventArgs("AUTHr"));
                                    SA sa = SA.GetSA(ikeMessage.InitiatorSPI);
                                    if (sa != null)
                                    {
                                        if (sa.ProcessAUTH(ikeMessage))
                                        {
                                            SPD.NewSA(Utils.MinimizeIPLength(Utils.ToShortStringIP(sa.ResponderIP)), sa.SAKey);

                                            sa.CreateChildSA();

                                            if (IKESAEstablished != null)
                                                IKESAEstablished(this, new IKESAEstablishedEventArgs(true, sa.InitiatorSPI, sa.ResponderSPI, sa.InitiatorIP, sa.ResponderIP));
                                            if (IKEMessageProcessingFinished != null)
                                                IKEMessageProcessingFinished(this, new IKEMessageProcessingFinishedEventArgs("AUTHr"));
                                        }
                                        else
                                            throw new Exception("Invalid AUTH message from the Responder.");
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case IKE_EXCHANGE_TYPES.INFO:
                    {
                        switch (ikeMessage.Flags)
                        {
                            case 0x08:
                                {
                                    if (IKEMessageProcessingStarted != null)
                                        IKEMessageProcessingStarted(this, new IKEMessageProcessingStartedEventArgs("INFOi"));
                                    SA sa = SA.GetSA(ikeMessage.ResponderSPI);
                                    if (sa != null)
                                        sa.ProcessINFO(ikeMessage);
                                    else
                                        throw new Exception("Invalid IKE message.");
                                    if (IKEMessageProcessingFinished != null)
                                        IKEMessageProcessingFinished(this, new IKEMessageProcessingFinishedEventArgs("INFOi"));

                                }
                                break;
                            case 0x20:
                                {
                                    if (IKEMessageProcessingStarted != null)
                                        IKEMessageProcessingStarted(this, new IKEMessageProcessingStartedEventArgs("INFOr"));
                                    SA sa = SA.GetSA(ikeMessage.InitiatorSPI);
                                    if (sa != null)
                                        sa.ProcessINFO(ikeMessage);
                                    else
                                        throw new Exception("Invalid IKE message.");
                                    if (IKEMessageProcessingFinished != null)
                                        IKEMessageProcessingFinished(this, new IKEMessageProcessingFinishedEventArgs("INFOr"));
                                }
                                break;
                        }
                    }
                    break;
            }
        }
        public void Send(IKE IKEMessage, byte[] SourceIP, byte[] DestinationIP)
        {
            byte[] payload = IKEMessage.ToBytes();
            UserDatagramProtocolPacket udpPacket = new UserDatagramProtocolPacket(APP_CONFIG.ISKAMP, APP_CONFIG.ISKAMP, payload);
            if (IKEMessageSent != null)
                IKEMessageSent(this, new NetworkDataEventArgs(payload, SourceIP, DestinationIP));
            Program.UDP.Send(SourceIP, DestinationIP, udpPacket);

        }
        public void Send(IKE IKEMessage, byte[] SourceIP, byte[] DestinationIP, byte[] MAC)
        {
            byte[] messageBytes = IKEMessage.ToBytes();
            byte[] macedMessage = new byte[messageBytes.Length + MAC.Length];
            messageBytes.CopyTo(macedMessage, 0);
            Utils.MemCpy(MAC, 0, ref macedMessage, messageBytes.Length, MAC.Length);
            UserDatagramProtocolPacket udpPacket = new UserDatagramProtocolPacket(APP_CONFIG.ISKAMP, APP_CONFIG.ISKAMP, macedMessage);
            if (IKEMessageSent != null)
                IKEMessageSent(this, new NetworkDataEventArgs(macedMessage, SourceIP, DestinationIP));
            Program.UDP.Send(SourceIP, DestinationIP, udpPacket);

        }
        public void SendINITi(byte[] ResponderIP, string PolicyID)
        {
            if (IKEMessageProcessingStarted != null)
                IKEMessageProcessingStarted(this, new IKEMessageProcessingStartedEventArgs("INITi"));

            byte[] initiatorIP = new byte[4];
            string[] ip = Utils.GetHostIPAddress().Split('.');
            initiatorIP[0] = byte.Parse(ip[0]);
            initiatorIP[1] = byte.Parse(ip[1]);
            initiatorIP[2] = byte.Parse(ip[2]);
            initiatorIP[3] = byte.Parse(ip[3]);

            SPDEntry policy = SPD.GetPolicy(PolicyID);
            if (policy != null)
            {

                IKE_Transform encrTransform = new IKE_Transform(false, IKE_TRANSFORM_TYPES.ENCR, (byte) policy.EncrAlg);
                encrTransform.AddAttribute(new IKE_Transform_Attribute(IKE_ATTRIBUTE_TYPES.KEY_LENGTH, policy.EncrKeyLength));
                if (policy.EncrAlg == IKE_ENCR_ALGS.ENCR_DES3)
                    encrTransform.AddAttribute(new IKE_Transform_Attribute(IKE_ATTRIBUTE_TYPES.BLOCK_SIZE, policy.EncrBlockSize));
                IKE_Transform intgTransform = new IKE_Transform(false, IKE_TRANSFORM_TYPES.INTEG, (byte)policy.IntgAlg);
                IKE_Transform prfTransform = new IKE_Transform(false, IKE_TRANSFORM_TYPES.PRF, (byte)policy.PRF);
                IKE_Transform dhGroupTransform = new IKE_Transform(true, IKE_TRANSFORM_TYPES.DH, (byte)policy.DHGroup);

                IKE_Proposal proposal = new IKE_Proposal(1, IKE_PROTOCOLS.IKE, true);
                proposal.AddTransform(encrTransform, false);
                proposal.AddTransform(intgTransform, false);
                proposal.AddTransform(prfTransform, false);
                proposal.AddTransform(dhGroupTransform, false);

                IKE_SA_Payload saPayload = new IKE_SA_Payload(IKE_PAYLOADS.KE);
                saPayload.AddProposal(proposal);

                IKE_KE_Payload kePayload = new IKE_KE_Payload(IKE_PAYLOADS.N, policy.DHGroup);

                IKE_Nonce_Payload noncePayload = new IKE_Nonce_Payload(IKE_PAYLOADS.NONE, 256);

                IKE ikeINITi = new IKE();
                ikeINITi.INITi();
                ikeINITi.AddPayload(saPayload, IKE_PAYLOADS.SA, saPayload.Length);
                ikeINITi.AddPayload(kePayload, IKE_PAYLOADS.KE, kePayload.Length);
                ikeINITi.AddPayload(noncePayload, IKE_PAYLOADS.N, noncePayload.Length);

                SA sa = new SA(ikeINITi, initiatorIP, ResponderIP, policy.Mode);
                this.Send(ikeINITi, initiatorIP, ResponderIP);

                sa.Status = SA_STATUS.INITi_SENT;

                if (IKEMessageProcessingFinished != null)
                    IKEMessageProcessingFinished(this, new IKEMessageProcessingFinishedEventArgs("INITi"));
            }
        }
        public void Disconnect(long SAKey)
        {
            if (IKEMessageProcessingStarted != null)
                IKEMessageProcessingStarted(this, new IKEMessageProcessingStartedEventArgs("INFOi"));

            SA sa = SA.GetSA(SAKey);
            if (sa != null)
            {

                IKE_Delete_Payload deletePayload = new IKE_Delete_Payload(IKE_PAYLOADS.NONE, IKE_PROTOCOLS.IKE);
                
                IKE_Encrypted_Payload encryptedPayload = new IKE_Encrypted_Payload();
                encryptedPayload.AddInnerPayload(deletePayload, IKE_PAYLOADS.DEL, deletePayload.Length);

                sa.RemoveSA();                       
                sa.ProtectEncryptedPayload(encryptedPayload);

                IKE ikeNotify = new IKE();
                ikeNotify.NOTIFY(sa.InitiatorSPI, sa.ResponderSPI, sa.IsInitiatorSA);

                ikeNotify.AddPayload(encryptedPayload, IKE_PAYLOADS.ENCR, encryptedPayload.Length);
                sa.SendProtected(ikeNotify);

                if (IKEMessageProcessingFinished != null)
                    IKEMessageProcessingFinished(this, new IKEMessageProcessingFinishedEventArgs("INFOi"));
            }
        }
    }
}
