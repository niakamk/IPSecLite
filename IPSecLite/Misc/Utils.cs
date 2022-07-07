using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.IO;
using adabtek.IPsecLite.Constants;
namespace adabtek.IPsecLite.Utilities
{
    public enum DH_GROUPS : short
    {
        GROUP1_768BIT_MODP = 1, /* RFC 4306 */
        GROUP2_1024BIT_MODP = 2, /* RFC 4306 */
        GROUP5_1536BIT_MODP = 5, /* RFC 3526 */
        GROUP14_2048BIT_MODP = 14, /* RFC 3526 */
        GROUP15_3072BIT_MODP = 15, /* RFC 3526 */
        GROUP16_4096BIT_MODP = 16, /* RFC 3526 */
        GROUP17_6144BIT_MODP = 17, /* RFC 3526 */
        GROUP18_8192BIT_MODP = 18 /* RFC 3526 */
    }
    public class Utils
    {
        public static short Checksum(byte[] data)
        {

            short word16;
            int sum = 0;

            //Add up all the numbers (2 byte words at the time)
            if ((data.Length % 2) == 0)
                for (int i = 0; i < data.Length; i = i + 2)
                {
                    word16 = Utils.BytesToShort(data[i], data[i + 1]);
                    sum += word16;
                }
            else
                for (int i = 0; i < data.Length-1; i = i + 2)
                {
                    word16 = Utils.BytesToShort(data[i], data[i + 1]);
                    sum += word16;
                }


            //Keep 4 hex digits and add the LO to the them
            while (sum > 0xffff)
                sum = (short)(sum & 0x00000ffff) + (short)((sum & 0xffff0000) >> 16);

            //Subtract from 0xffff
            return (short) (~sum);
        }
        public static void LogToTextFile(string FileName, string Message)
        {
            StreamWriter sw = new StreamWriter(FileName, true);
            sw.WriteLine(Message);
            sw.Flush();
            sw.Close();
        }
        public static void ExportListViewToTextFile(ListView ListViewToExport, string FileName)
        {
            Cursor c = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            StreamWriter sw = new StreamWriter(FileName);
            for (int i = 0; i < ListViewToExport.Columns.Count; i++)
                sw.Write(ListViewToExport.Columns[i].Text + "\t");
            sw.WriteLine();

            for (int i = 0; i < ListViewToExport.Items.Count; i++)
            {
                for (int j = 0; j < ListViewToExport.Columns.Count; j++)
                    sw.Write(ListViewToExport.Items[i].SubItems[j].Text + "\t");

                sw.WriteLine();
            }
            sw.Flush();
            sw.Close();

            Cursor.Current = c;
        }
        public static string HoursThroughMilliseconds(DateTime Time)
        {
            return Time.Hour.ToString().PadLeft(2,'0') + ":" + Time.Minute.ToString().PadLeft(2, '0') + ":" + Time.Second.ToString().PadLeft(2, '0') + ":" + Time.Millisecond.ToString().PadLeft(4, '0');
        }
        public static string ToCharString(byte[] RawData, bool WithSegments, byte SegmentLength)
        {
            string toText = "";
            int counter = 0;
            if (WithSegments)
            {
                for (int i = 0; i < RawData.Length; i++)
                {
                    counter++;
                    toText += (char)(RawData[i] < 32 ? (byte) '.' : RawData[i]);
                    if (counter % SegmentLength == 0)
                    {
                        toText += "\r\n";
                        counter = 0;
                    }
                }
            }else
                for (int i = 0; i < RawData.Length; i++)
                    toText += (char)(RawData[i] < 32 ? (byte)'.' : RawData[i]);
            return toText;
        }
        public static string ToHexString(byte[] RawData, bool WithSegments, byte SegmentLength)
        {
            if (RawData == null)
                return "";

            string toText = "";
            byte[] ch = new byte[2];
            if (WithSegments)
            {
                int counter = 0;
                for (int i = 0; i < RawData.Length; i++)
                {
                    counter++;
                    ch[0] = (byte)((RawData[i] & 0xf0) >> 4);
                    ch[0] += (byte)(ch[0] > 9 ? 55 : 48);
                    ch[1] = (byte)(RawData[i] & 0x0f);
                    ch[1] += (byte)(ch[1] > 9 ? 55 : 48);
                    toText += Encoding.ASCII.GetString(ch) + " ";
                    if (counter % SegmentLength == 0)
                    {
                        toText += "\r\n";
                        counter = 0;
                    }
                }
            }
            else
                for (int i = 0; i < RawData.Length; i++)
                {
                    ch[0] = (byte)((RawData[i] & 0xf0) >> 4);
                    ch[0] += (byte)(ch[0] > 9 ? 55 : 48);
                    ch[1] = (byte)(RawData[i] & 0x0f);
                    ch[1] += (byte)(ch[1] > 9 ? 55 : 48);
                    toText += Encoding.ASCII.GetString(ch) + " ";
                }

            return toText;
        }
        public static bool AreSameIPs(byte[] IP1, byte[] IP2)
        {
            bool areSame = true;
            for (int i = 0; i < 4; i++)
                if (IP1[i] != IP2[i])
                {
                    areSame = false;
                    break;
                }
            return areSame;
        }
        public static short BytesToShort(byte Byte1, byte Byte2)
        {
            short n = Byte1;
            n <<= 8;
            n += Byte2;
            return n;
        }
        public static byte[] HexStringToBytes(string HexString)
        {
            string cleanedHexString = "";
            cleanedHexString.Replace(' ', (char)0);
            cleanedHexString = cleanedHexString.ToUpper();
            for(int i = 0; i < HexString.Length; i++)
                if (((HexString[i] >= '0') && (HexString[i] <= '9')) || ((HexString[i] >= 'A') && (HexString[i] <= 'F')))
                    cleanedHexString += HexString[i];
            if ((cleanedHexString.Length % 2) == 1)
                cleanedHexString = '0' + cleanedHexString;

            byte[] toBytes = new byte[cleanedHexString.Length / 2];
            int j = 0;
            int k = 0;
            while (j < cleanedHexString.Length)
            {
                toBytes[k] = (byte)(cleanedHexString[j] <= '9' ? cleanedHexString[j] - '0' : cleanedHexString[j] - 'A' + 10);
                toBytes[k] <<= 4;
                toBytes[k] |= (byte)(cleanedHexString[j+1] <= '9' ? cleanedHexString[j+1] - '0' : cleanedHexString[j+1] - 'A' + 10);
                j += 2;
                k++;
            }
            return toBytes;
        }
        public static byte[] LongToBytes(long n)
        {
            byte[] nBytes = new byte[8];
            for (int i = 0; i < nBytes.Length; i++)
                nBytes[i] = (byte)(n >> ((8 - (i + 1)) * 8));

            return nBytes;
        }
        public static byte[] IntToBytes(int n)
        {
            byte[] nBytes = new byte[4];
            for (int i = 0; i < nBytes.Length; i++)
                nBytes[i] = (byte)(n >> ((8 - (i + 1)) * 8));

            return nBytes;
        }
        public static byte[] ShortToBytes(short n)
        {
            byte[] nBytes = new byte[2];
            nBytes[0] = (byte)((n & 0xff00) >> 8);
            nBytes[1] = (byte)(n & 0x00ff);

            return nBytes;
        }
        public static long BytesToLong(byte[] bytes)
        {
            long n =0;
            for (int i = 0; i < sizeof(long); i++){
                n *= 256;
                n += bytes[i];
            }
            return n;
        }
        public static int BytesToInt(byte[] bytes)
        {
            int n = 0;
            for (int i = 0; i < sizeof(int); i++)
            {
                n *= 256;
                n += bytes[i];
            }
            return n;
        }
        public static string ToBinary(Int64 Decimal, int Length)
        {
            // Declare a few variables we're going to need
            Int64 BinaryHolder;
            char[] BinaryArray;
            string BinaryResult = "";

            while (Decimal > 0)
            {
                BinaryHolder = Decimal % 2;
                BinaryResult += BinaryHolder;
                Decimal = Decimal / 2;
            }

            // The algoritm gives us the binary number in reverse order (mirrored)
            // We store it in an array so that we can reverse it back to normal
            BinaryArray = BinaryResult.ToCharArray();
            Array.Reverse(BinaryArray);
            BinaryResult = new string(BinaryArray);
            BinaryResult = BinaryResult.PadLeft(Length, '0');

            return BinaryResult;
        }
        public static int ToDecimal(string bin)
        {
            long l = Convert.ToInt64(bin, 2);
            int i = (int)l;
            return i;
        }
        public static string ToBinaryIP(string decimalIP)
        {

            string ip;
            string[] ipOctects = decimalIP.Split('.');
            ip = ToBinary(long.Parse(ipOctects[0]), 8);
            ip += ".";
            ip += ToBinary(long.Parse(ipOctects[1]), 8);
            ip += ".";
            ip += ToBinary(long.Parse(ipOctects[2]), 8);
            ip += ".";
            ip += ToBinary(long.Parse(ipOctects[3]), 8);

            return ip;
        }
        public static string ToDecimalIP(string binIP)
        {

            string ip;
            string[] ipOctects = binIP.Split('.');
            ip = ToDecimal(ipOctects[0]).ToString();
            ip += ".";
            ip += ToDecimal(ipOctects[1]).ToString();
            ip += ".";
            ip += ToDecimal(ipOctects[2]).ToString();
            ip += ".";
            ip += ToDecimal(ipOctects[3]).ToString();

            return ip;
        }
        public static string ToLongStringIP(byte[] IPAddress)
        {
            string ip;
            ip = IPAddress[0].ToString().PadLeft(3, '0');
            ip += ".";
            ip += IPAddress[1].ToString().PadLeft(3, '0');
            ip += ".";
            ip += IPAddress[2].ToString().PadLeft(3, '0');
            ip += ".";
            ip += IPAddress[3].ToString().PadLeft(3, '0');

            return ip;
        }
        public static string ToShortStringIP(byte[] IPAddress)
        {

            string ip;
            ip = IPAddress[0].ToString();
            ip += ".";
            ip += IPAddress[1].ToString();
            ip += ".";
            ip += IPAddress[2].ToString();
            ip += ".";
            ip += IPAddress[3].ToString();

            return ip;
        }
        public static string MinimizeIPLength(string IPAddress)
        {
            string[] ip = IPAddress.Split('.');
            return byte.Parse(ip[0]).ToString() + "." + byte.Parse(ip[1]).ToString() + "." + byte.Parse(ip[2]).ToString() + "." + byte.Parse(ip[3]).ToString();
        }
        public static string ToHexString(byte[] Source)
        {
            string hexString = "0x";
            byte[] ch = new byte[2];
            for (int i = 0; i < Source.Length; i++)
            {
                ch[0] = (byte)((Source[i] & 0xf0) >> 4);
                ch[0] += (byte)(ch[0] > 9 ? 55 : 48);
                ch[1] = (byte)(Source[i] & 0x0f);
                ch[1] += (byte)(ch[1] > 9 ? 55 : 48);
                hexString += Encoding.ASCII.GetString(ch);
            }
            return hexString;
        }
        public static string ToHexString(long Source)
        {
            string hexString = "0x";
            byte[] ch = new byte[2];
            byte b;
            for (int i = 0; i < 8; i++)
            {
                b = (byte)((Source & (((long)0xff) << i)) >> i);
                ch[0] = (byte)((b & 0xf0) >> 4);
                ch[0] += (byte)(ch[0] > 9 ? 55 : 48);
                ch[1] = (byte)(b & 0x0f);
                ch[1] += (byte)(ch[1] > 9 ? 55 : 48);
                hexString += Encoding.ASCII.GetString(ch);
            }
            return hexString;
        }
        public static string ToHexString(int Source)
        {
            string hexString = "0x";
            byte[] ch = new byte[2];
            byte b;
            for (int i = 0; i < 4; i++)
            {
                b = (byte)((Source & (((long)0xff) << i)) >> i);
                ch[0] = (byte)((b & 0xf0) >> 4);
                ch[0] += (byte)(ch[0] > 9 ? 55 : 48);
                ch[1] = (byte)(b & 0x0f);
                ch[1] += (byte)(ch[1] > 9 ? 55 : 48);
                hexString += Encoding.ASCII.GetString(ch);
            }
            return hexString;
        }
        public static string ToString(byte[] Source)
        {
            string toString = "";
            for (int i = 0; i < Source.Length; i++)
            {
                toString += Source[i].ToString();
            }
            return toString;
        }
        public static void MemCpy(byte[] Source, int SourceIndex, ref byte[] Destination, int DestinationIndex, int NoOfBytes) 
        {
            for (int i = 0; i < NoOfBytes; i++)
                Destination[i + DestinationIndex] = Source[i + SourceIndex];
        }
        public static void MemSet(ref byte[] Source, int SourceIndex, byte Value, int NoOfBytes)
        {
            for (int i = 0; i < NoOfBytes; i++)
                Source[i + SourceIndex] = Value;
        }
        public static string GetHostIPAddress()
        {
            return APP_CONFIG.ETHERNET_IP;
        }
        public static byte[] IPToBytes(string IPAddress)
        {
            byte[] ipBytes = new byte[4];
            string[] ip = IPAddress.Split('.');
            ipBytes[0] = byte.Parse(ip[0]);
            ipBytes[1] = byte.Parse(ip[1]);
            ipBytes[2] = byte.Parse(ip[2]);
            ipBytes[3] = byte.Parse(ip[3]);

            return ipBytes;
        }
        public static string ModP(DH_GROUPS DHGroup, out string GroupGenerator)
        {
            switch (DHGroup)
            {
                case DH_GROUPS.GROUP1_768BIT_MODP:
                    GroupGenerator = "2";
                    return "FFFFFFFFFFFFFFFFC90FDAA22168C234C4C6628B80DC1CD1" +
                            "29024E088A67CC74020BBEA63B139B22514A08798E3404DD" +
                            "EF9519B3CD3A431B302B0A6DF25F14374FE1356D6D51C245" +
                            "E485B576625E7EC6F44C42E9A63A3620FFFFFFFFFFFFFFFF";
                case DH_GROUPS.GROUP2_1024BIT_MODP:
                    GroupGenerator = "2";
                    return "FFFFFFFFFFFFFFFFC90FDAA22168C234C4C6628B80DC1CD1" +
                            "29024E088A67CC74020BBEA63B139B22514A08798E3404DD" +
                            "EF9519B3CD3A431B302B0A6DF25F14374FE1356D6D51C245" +
                            "E485B576625E7EC6F44C42E9A637ED6B0BFF5CB6F406B7ED" +
                            "EE386BFB5A899FA5AE9F24117C4B1FE649286651ECE65381" +
                            "FFFFFFFFFFFFFFFF";
                case DH_GROUPS.GROUP5_1536BIT_MODP:
                    GroupGenerator = "2";
                    return "FFFFFFFFFFFFFFFFC90FDAA22168C234C4C6628B80DC1CD1" +
                            "29024E088A67CC74020BBEA63B139B22514A08798E3404DD" +
                            "EF9519B3CD3A431B302B0A6DF25F14374FE1356D6D51C245" +
                            "E485B576625E7EC6F44C42E9A637ED6B0BFF5CB6F406B7ED" +
                            "EE386BFB5A899FA5AE9F24117C4B1FE649286651ECE45B3D" +
                            "C2007CB8A163BF0598DA48361C55D39A69163FA8FD24CF5F" +
                            "83655D23DCA3AD961C62F356208552BB9ED529077096966D" +
                            "670C354E4ABC9804F1746C08CA237327FFFFFFFFFFFFFFFF";
                case DH_GROUPS.GROUP14_2048BIT_MODP:
                    GroupGenerator = "2";
                    return  "FFFFFFFFFFFFFFFFC90FDAA22168C234C4C6628B80DC1CD1" +
                            "29024E088A67CC74020BBEA63B139B22514A08798E3404DD" +
                            "EF9519B3CD3A431B302B0A6DF25F14374FE1356D6D51C245" +
                            "E485B576625E7EC6F44C42E9A637ED6B0BFF5CB6F406B7ED" +
                            "EE386BFB5A899FA5AE9F24117C4B1FE649286651ECE45B3D" +
                            "C2007CB8A163BF0598DA48361C55D39A69163FA8FD24CF5F" +
                            "83655D23DCA3AD961C62F356208552BB9ED529077096966D" +
                            "670C354E4ABC9804F1746C08CA18217C32905E462E36CE3B" +
                            "E39E772C180E86039B2783A2EC07A28FB5C55DF06F4C52C9" +
                            "DE2BCBF6955817183995497CEA956AE515D2261898FA0510" +
                            "15728E5A8AACAA68FFFFFFFFFFFFFFFF";
                case DH_GROUPS.GROUP15_3072BIT_MODP:
                    GroupGenerator = "2";
                    return "FFFFFFFFFFFFFFFFC90FDAA22168C234C4C6628B80DC1CD1" +
                            "29024E088A67CC74020BBEA63B139B22514A08798E3404DD" +
                            "EF9519B3CD3A431B302B0A6DF25F14374FE1356D6D51C245" +
                            "E485B576625E7EC6F44C42E9A637ED6B0BFF5CB6F406B7ED" +
                            "EE386BFB5A899FA5AE9F24117C4B1FE649286651ECE45B3D" +
                            "C2007CB8A163BF0598DA48361C55D39A69163FA8FD24CF5F" +
                            "83655D23DCA3AD961C62F356208552BB9ED529077096966D" +
                            "670C354E4ABC9804F1746C08CA18217C32905E462E36CE3B" +
                            "E39E772C180E86039B2783A2EC07A28FB5C55DF06F4C52C9" +
                            "DE2BCBF6955817183995497CEA956AE515D2261898FA0510" +
                            "15728E5A8AAAC42DAD33170D04507A33A85521ABDF1CBA64" +
                            "ECFB850458DBEF0A8AEA71575D060C7DB3970F85A6E1E4C7" +
                            "ABF5AE8CDB0933D71E8C94E04A25619DCEE3D2261AD2EE6B" +
                            "F12FFA06D98A0864D87602733EC86A64521F2B18177B200C" +
                            "BBE117577A615D6C770988C0BAD946E208E24FA074E5AB31" +
                            "43DB5BFCE0FD108E4B82D120A93AD2CAFFFFFFFFFFFFFFFF";
                case DH_GROUPS.GROUP16_4096BIT_MODP:
                    GroupGenerator = "2";
                    return "FFFFFFFFFFFFFFFFC90FDAA22168C234C4C6628B80DC1CD1" +
                            "29024E088A67CC74020BBEA63B139B22514A08798E3404DD" +
                            "EF9519B3CD3A431B302B0A6DF25F14374FE1356D6D51C245" +
                            "E485B576625E7EC6F44C42E9A637ED6B0BFF5CB6F406B7ED" +
                            "EE386BFB5A899FA5AE9F24117C4B1FE649286651ECE45B3D" +
                            "C2007CB8A163BF0598DA48361C55D39A69163FA8FD24CF5F" +
                            "83655D23DCA3AD961C62F356208552BB9ED529077096966D" +
                            "670C354E4ABC9804F1746C08CA18217C32905E462E36CE3B" +
                            "E39E772C180E86039B2783A2EC07A28FB5C55DF06F4C52C9" +
                            "DE2BCBF6955817183995497CEA956AE515D2261898FA0510" +
                            "15728E5A8AAAC42DAD33170D04507A33A85521ABDF1CBA64" +
                            "ECFB850458DBEF0A8AEA71575D060C7DB3970F85A6E1E4C7" +
                            "ABF5AE8CDB0933D71E8C94E04A25619DCEE3D2261AD2EE6B" +
                            "F12FFA06D98A0864D87602733EC86A64521F2B18177B200C" +
                            "BBE117577A615D6C770988C0BAD946E208E24FA074E5AB31" +
                            "43DB5BFCE0FD108E4B82D120A92108011A723C12A787E6D7" +
                            "88719A10BDBA5B2699C327186AF4E23C1A946834B6150BDA" +
                            "2583E9CA2AD44CE8DBBBC2DB04DE8EF92E8EFC141FBECAA6" +
                            "287C59474E6BC05D99B2964FA090C3A2233BA186515BE7ED" +
                            "1F612970CEE2D7AFB81BDD762170481CD0069127D5B05AA9" +
                            "93B4EA988D8FDDC186FFB7DC90A6C08F4DF435C934063199" +
                            "FFFFFFFFFFFFFFFF";
                case DH_GROUPS.GROUP17_6144BIT_MODP:
                    GroupGenerator = "2";
                    return "FFFFFFFFFFFFFFFFC90FDAA22168C234C4C6628B80DC1CD1" +
                            "29024E088A67CC74020BBEA63B139B22514A08798E3404DD" +
                            "EF9519B3CD3A431B302B0A6DF25F14374FE1356D6D51C245" +
                            "E485B576625E7EC6F44C42E9A637ED6B0BFF5CB6F406B7ED" +
                            "EE386BFB5A899FA5AE9F24117C4B1FE649286651ECE45B3D" +
                            "C2007CB8A163BF0598DA48361C55D39A69163FA8FD24CF5F" +
                            "83655D23DCA3AD961C62F356208552BB9ED529077096966D" +
                            "670C354E4ABC9804F1746C08CA18217C32905E462E36CE3B" +
                            "E39E772C180E86039B2783A2EC07A28FB5C55DF06F4C52C9" +
                            "DE2BCBF6955817183995497CEA956AE515D2261898FA0510" +
                            "15728E5A8AAAC42DAD33170D04507A33A85521ABDF1CBA64" +
                            "ECFB850458DBEF0A8AEA71575D060C7DB3970F85A6E1E4C7" +
                            "ABF5AE8CDB0933D71E8C94E04A25619DCEE3D2261AD2EE6B" +
                            "F12FFA06D98A0864D87602733EC86A64521F2B18177B200C" +
                            "BBE117577A615D6C770988C0BAD946E208E24FA074E5AB31" +
                            "43DB5BFCE0FD108E4B82D120A92108011A723C12A787E6D7" +
                            "88719A10BDBA5B2699C327186AF4E23C1A946834B6150BDA" +
                            "2583E9CA2AD44CE8DBBBC2DB04DE8EF92E8EFC141FBECAA6" +
                            "287C59474E6BC05D99B2964FA090C3A2233BA186515BE7ED" +
                            "1F612970CEE2D7AFB81BDD762170481CD0069127D5B05AA9" +
                            "93B4EA988D8FDDC186FFB7DC90A6C08F4DF435C934028492" +
                            "36C3FAB4D27C7026C1D4DCB2602646DEC9751E763DBA37BD" +
                            "F8FF9406AD9E530EE5DB382F423001AEB06A53ED9027D831" +
                            "179727B0865A8918DA3EDBEBCF9B14ED44CE6CBACED4BB1B" +
                            "DB7F1447E6CC254B332051512BD7AF426FB8F401378CD2BF" +
                            "5983CA01C64B92ECF032EA15D1721D03F482D7CE6E74FEF6" +
                            "D55E702F46980C82B5A84031900B1C9E59E7C97FBEC7E8F3" +
                            "23A97A7E36CC88BE0F1D45B7FF585AC54BD407B22B4154AA" +
                            "CC8F6D7EBF48E1D814CC5ED20F8037E0A79715EEF29BE328" +
                            "06A1D58BB7C5DA76F550AA3D8A1FBFF0EB19CCB1A313D55C" +
                            "DA56C9EC2EF29632387FE8D76E3C0468043E8F663F4860EE" +
                            "12BF2D5B0B7474D6E694F91E6DCC4024FFFFFFFFFFFFFFFF";
                case DH_GROUPS.GROUP18_8192BIT_MODP:
                    GroupGenerator = "2";
                    return "FFFFFFFFFFFFFFFFC90FDAA22168C234C4C6628B80DC1CD1" +
                            "29024E088A67CC74020BBEA63B139B22514A08798E3404DD" +
                            "EF9519B3CD3A431B302B0A6DF25F14374FE1356D6D51C245" +
                            "E485B576625E7EC6F44C42E9A637ED6B0BFF5CB6F406B7ED" +
                            "EE386BFB5A899FA5AE9F24117C4B1FE649286651ECE45B3D" +
                            "C2007CB8A163BF0598DA48361C55D39A69163FA8FD24CF5F" +
                            "83655D23DCA3AD961C62F356208552BB9ED529077096966D" +
                            "670C354E4ABC9804F1746C08CA18217C32905E462E36CE3B" +
                            "E39E772C180E86039B2783A2EC07A28FB5C55DF06F4C52C9" +
                            "DE2BCBF6955817183995497CEA956AE515D2261898FA0510" +
                            "15728E5A8AAAC42DAD33170D04507A33A85521ABDF1CBA64" +
                            "ECFB850458DBEF0A8AEA71575D060C7DB3970F85A6E1E4C7" +
                            "ABF5AE8CDB0933D71E8C94E04A25619DCEE3D2261AD2EE6B" +
                            "F12FFA06D98A0864D87602733EC86A64521F2B18177B200C" +
                            "BBE117577A615D6C770988C0BAD946E208E24FA074E5AB31" +
                            "43DB5BFCE0FD108E4B82D120A92108011A723C12A787E6D7" +
                            "88719A10BDBA5B2699C327186AF4E23C1A946834B6150BDA" +
                            "2583E9CA2AD44CE8DBBBC2DB04DE8EF92E8EFC141FBECAA6" +
                            "287C59474E6BC05D99B2964FA090C3A2233BA186515BE7ED" +
                            "1F612970CEE2D7AFB81BDD762170481CD0069127D5B05AA9" +
                            "93B4EA988D8FDDC186FFB7DC90A6C08F4DF435C934028492" +
                            "36C3FAB4D27C7026C1D4DCB2602646DEC9751E763DBA37BD" +
                            "F8FF9406AD9E530EE5DB382F423001AEB06A53ED9027D831" +
                            "179727B0865A8918DA3EDBEBCF9B14ED44CE6CBACED4BB1B" +
                            "DB7F1447E6CC254B332051512BD7AF426FB8F401378CD2BF" +
                            "5983CA01C64B92ECF032EA15D1721D03F482D7CE6E74FEF6" +
                            "D55E702F46980C82B5A84031900B1C9E59E7C97FBEC7E8F3" +
                            "23A97A7E36CC88BE0F1D45B7FF585AC54BD407B22B4154AA" +
                            "CC8F6D7EBF48E1D814CC5ED20F8037E0A79715EEF29BE328" +
                            "06A1D58BB7C5DA76F550AA3D8A1FBFF0EB19CCB1A313D55C" +
                            "DA56C9EC2EF29632387FE8D76E3C0468043E8F663F4860EE" +
                            "12BF2D5B0B7474D6E694F91E6DBE115974A3926F12FEE5E4" +
                            "38777CB6A932DF8CD8BEC4D073B931BA3BC832B68D9DD300" +
                            "741FA7BF8AFC47ED2576F6936BA424663AAB639C5AE4F568" +
                            "3423B4742BF1C978238F16CBE39D652DE3FDB8BEFC848AD9" +
                            "22222E04A4037C0713EB57A81A23F0C73473FC646CEA306B" +
                            "4BCBC8862F8385DDFA9D4B7FA4C087E879683303ED5BDD3A" +
                            "062B3CF5B3A278A66D2A13F83F44F82DDF310EE074AB6A36" +
                            "4597E899A0255DC164F31CC50846851DF9AB48195DED7EA1" +
                            "B1D510BD7EE74D73FAF36BC31ECFA268359046F4EB879F92" +
                            "4009438B481C6CD7889A002ED5EE382BC9190DA6FC026E47" +
                            "9558E4475677E9AA9E3050E2765694DFC81F56E880B96E71" +
                            "60C980DD98EDD3DFFFFFFFFFFFFFFFFF";
            }
            GroupGenerator = "";
            return "";
        }

    }
}
