using System;
using System.Collections.Generic;
using adabtek.IPsecLite.IKEv2;
namespace adabtek.IPsecLite.SPDB
{
    public class SPDEntry
    {
        public string Key;
        public long SAKey;
        public string Application;
        public IKE_MODE Mode;
        public IKE_PROTOCOLS Protocol;
        public IKE_ENCR_ALGS EncrAlg;
        public short EncrKeyLength;
        public short EncrBlockSize;
        public IKE_INTEG_ALGS IntgAlg;
        public IKE_PRFS PRF;
        public IKE_DH_GROUPS DHGroup;
    }
    public class SPDChangedEventArgs : EventArgs
    {
        SPDEntry spdEntry;
        char updateType;
        public SPDEntry Rule 
        { 
            get { return this.spdEntry; } 
        }
        public char UpdateType 
        { 
            get { return this.updateType; } 
        }
        public SPDChangedEventArgs(SPDEntry Rule, char UpdateType)
        {
            this.spdEntry = Rule;
            this.updateType = UpdateType;
        }
    }
    public class SPD
    {
        public delegate void ChangedSPDHandler(SPDChangedEventArgs e);
        public static event ChangedSPDHandler SPDUpdated;

        static Dictionary<string, SPDEntry> spd = new Dictionary<string, SPDEntry>();

        public static SPDEntry GetPolicy(string Key)
        {
            SPDEntry spdEntry;
            spd.TryGetValue(Key, out spdEntry);
            return spdEntry;
        }

        public static Dictionary<string, SPDEntry> SPDB
        {
            get { return spd; }
        }
        public static void RemoveIKESA(long IKESAKey)
        {
            foreach (KeyValuePair<string, SPDEntry> policy in SPDB)
            {
                if (policy.Value.SAKey == IKESAKey)
                {
                    policy.Value.SAKey = 0;
                    break;
                }
            }
        }
        public void AddPolicy(SPDEntry Policy)
        {
            SPDEntry oldPolicy;
            SPDChangedEventArgs e;
            if (!spd.TryGetValue(Policy.Key, out oldPolicy))
            {
                spd.Add(Policy.Key, Policy);
                e = new SPDChangedEventArgs(Policy, 'A');
                if (SPDUpdated != null)
                    SPDUpdated(e);
            }
            else
            {
                if (oldPolicy.SAKey == 0)
                {
                    spd.Remove(Policy.Key);
                    e = new SPDChangedEventArgs(oldPolicy, 'D');
                    if (SPDUpdated != null)
                        SPDUpdated(e);
                    e = new SPDChangedEventArgs(Policy, 'A');
                    spd.Add(Policy.Key, Policy);
                    if (SPDUpdated != null)
                        SPDUpdated(e);
                }
                else
                {
                    e = new SPDChangedEventArgs(oldPolicy, 'N');
                    if (SPDUpdated != null)
                        SPDUpdated(e);
                }
            }
        }

        public static void NewSA(string Key, long SAKey)
        {
            SPDEntry spdEntry;
            if (spd.TryGetValue(Key, out spdEntry))
            {
                spdEntry.SAKey = SAKey;
            }

        }
        public static long GetSA(string Key)
        {
            SPDEntry spdEntry;
            if (spd.TryGetValue(Key, out spdEntry))
                return spdEntry.SAKey;
            else
                return 0;
        }
    }
}
