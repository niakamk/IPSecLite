using System.Security.Cryptography;
using adabtek.IPsecLite.Constants;
using adabtek.IPsecLite.Timing;
using adabtek.IPsecLite.Utilities;
using System;
namespace adabtek.IPsecLite.Cryptography
{
    public enum HASH_ALGS_OUTPUT_LENGTH : short
    {
        SHA1 = 20,
        HMAC_SHA1 = 20,
        HMAC_SHA1_96 = 12,
        MD5 = 20,
        HMAC_MD5 = 20,
        HMAC_MD5_96 = 12,
        AES_XCBC_MAC = 16,
        AES_XCBC_MAC_96 = 12,
    }

    public class HMAC_SHA1
    {
        public static byte[] GetMAC(byte[] Data, int Offset, int Length, byte[] Key, ref double ExecutionTime)
        {
            long startTime = HiPerfTimer.GetPeformanceCount();
            byte[] mac;
            HMACSHA1 alg = new HMACSHA1(Key);
            mac = alg.ComputeHash(Data, Offset, Length);
            alg.Clear();
            long stopTime = HiPerfTimer.GetPeformanceCount();
            long frequency = HiPerfTimer.GetPerformanceFrequency();
            ExecutionTime = ((stopTime - startTime) / (double) frequency);
            return mac;
        }
    }
    public class HMAC_MD5
    {
        public static byte[] GetMAC(byte[] Data, int Offset, int Length, byte[] Key, ref double ExecutionTime)
        {
            long startTime = HiPerfTimer.GetPeformanceCount();
            byte[] mac;
            HMACMD5 alg = new HMACMD5(Key);
            mac = alg.ComputeHash(Data, Offset, Length);
            alg.Clear();
            long stopTime = HiPerfTimer.GetPeformanceCount();
            long frequency = HiPerfTimer.GetPerformanceFrequency();
            ExecutionTime = ((stopTime - startTime) / (double)frequency);
            return mac;
        }
    }
    public class AES_XCBC_MAC
    {
        //RFC3566 - The AES-XCBC-MAC-96 Algorithm and Its Use With IPsec
        public static byte[] GetMAC(byte[] Data, int Offset, int Length, byte[] Key, ref double ExecutionTime)
        {
            long startTime = HiPerfTimer.GetPeformanceCount();
            byte[] mac = new byte[16];

            byte[] K1 = {0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01};
            byte[] K2 = {0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02};
            byte[] K3 = {0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03};

            int n = (int) Math.Ceiling((double)Length / 16.0);

            byte[] M = new byte[(n + 1) * 16];
            Utils.MemCpy(Data, 0, ref M, 16, Length);

            int lastBlockOffset = Length % 16;
            bool MnIsBy16 = (lastBlockOffset == 0);
            if (!MnIsBy16)
            {
                lastBlockOffset++;
                M[n * 16 + lastBlockOffset] = 128;
                lastBlockOffset++;
                for (int k = lastBlockOffset; k < 16; k++)
                    M[n  * 16 + k] = 0;
            }

            byte[] E;

            E = new byte[(n + 1) * 16];
            for (int i = 0; i < 16; i++)
                E[i] = 0x00;

            AesCryptoServiceProvider alg = new AesCryptoServiceProvider();
            alg.Mode = CipherMode.ECB;
            alg.Padding = PaddingMode.None;
            alg.BlockSize = 128;
            alg.KeySize = 128;
            byte[] Key16;
            if (Key.Length > 16)
            {
                Key16 = new byte[16];
                Utils.MemCpy(Key, 0, ref Key16, 0, 16);
                alg.Key = Key16;
            }
            else
                alg.Key = Key;

            ICryptoTransform encryptor = alg.CreateEncryptor();

            encryptor.TransformBlock(K1, 0, K1.Length, K1, 0);
            encryptor.TransformBlock(K2, 0, K2.Length, K2, 0);
            encryptor.TransformBlock(K3, 0, K3.Length, K3, 0);

            alg.Key = K1;
            encryptor = alg.CreateEncryptor();

            for (int i = 1; i < n - 1; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    M[i * 16 + j] ^= E[(i - 1) * 16 + j];
                }
                encryptor.TransformBlock(M, i * 16, 16, E, i * 16);
            }

            for (int j = 0; j < 16; j++)
                M[n * 16 + j] ^= E[(n - 1) * 16 + j];

            if (MnIsBy16)
            {
                for (int j = 0; j < 16; j++)
                    M[n * 16 + j] ^= K2[j];
            }
            else
            {
                for (int j = 0; j < 16; j++)
                    M[n * 16 + j] ^= K3[j];
            }
            encryptor.TransformBlock(M, n * 16, 16, E, n * 16);

            alg.Clear();
            encryptor.Dispose();

            Utils.MemCpy(E, n * 16, ref mac, 0, 16);

            long stopTime = HiPerfTimer.GetPeformanceCount();
            long frequency = HiPerfTimer.GetPerformanceFrequency();
            ExecutionTime = ((stopTime - startTime) / (double)frequency);
            return mac;
        }
    }
    public class AES
    {
        byte[] encryptedData;
        byte[] iv;
        byte[] data;
        byte[] key;
        short blockSize;
        CipherMode mode;
        long startTime = 0;
        long stopTime = 0;

        public AES(byte[] Data, byte[] Key, short BlockSize, CipherMode Mode)
        {
            this.startTime = HiPerfTimer.GetPeformanceCount();

            this.blockSize = BlockSize;
            this.data = Data;
            this.key = Key;
            this.mode = Mode;

            byte paddingLength = (byte)(this.blockSize - ((this.data.Length + 1) % this.blockSize));

            this.encryptedData = new byte[(this.data.Length + 1) + paddingLength];
            this.data.CopyTo(encryptedData, 0);
            int index = data.Length;
            for (byte i = 1; i <= paddingLength; i++)
                encryptedData[index++] = i;
            this.encryptedData[index] = paddingLength;
            this.data = null;

            this.data = new byte[encryptedData.Length];
            this.encryptedData.CopyTo(data, 0);
        }
        public AES(byte[] Data, byte[] Key, short BlockSize, CipherMode Mode, PROTOCOLS NextProtocol)
        {
            this.startTime = HiPerfTimer.GetPeformanceCount();

            this.blockSize = BlockSize;
            this.data = Data;
            this.key = Key;
            this.mode = Mode;

            byte paddingLength = (byte)(this.blockSize - ((this.data.Length + 2) % this.blockSize));

            this.encryptedData = new byte[(this.data.Length + 2) + paddingLength];
            this.data.CopyTo(encryptedData, 0);
            int index = data.Length;
            for (byte i = 1; i <= paddingLength; i++)
                encryptedData[index++] = i;
            this.encryptedData[index++] = paddingLength;
            this.encryptedData[index] = (byte)NextProtocol;
            this.data = null;

            this.data = new byte[encryptedData.Length];
            this.encryptedData.CopyTo(data, 0);
        }
        public AES(byte[] EncryptedData, byte[] Key, short BlockSize, CipherMode Mode, byte[] IV)
        {
            this.startTime = HiPerfTimer.GetPeformanceCount();

            this.blockSize = BlockSize;
            this.encryptedData = EncryptedData;
            this.key = Key;
            this.iv = IV;
            this.mode = Mode;

            this.data = new byte[encryptedData.Length];
        }

        public void Encrypt()    
        {
            AesCryptoServiceProvider alg = new AesCryptoServiceProvider();
            alg.Mode = this.mode;
            alg.Padding = PaddingMode.None;
            alg.BlockSize = this.blockSize * 8;
            alg.GenerateIV();
            alg.KeySize = this.key.Length * 8;
            alg.Key = this.key;
            ICryptoTransform encryptor = alg.CreateEncryptor();
            encryptor.TransformBlock(this.data, 0, this.data.Length, this.encryptedData, 0);
            encryptor.TransformFinalBlock(this.data, 0, this.data.Length);
            iv = alg.IV;
            alg.Clear();
            this.stopTime = HiPerfTimer.GetPeformanceCount();

        }
        public void Decrypt()
        {
            AesCryptoServiceProvider alg = new AesCryptoServiceProvider();
            alg.Mode = this.mode;
            alg.Padding = PaddingMode.None;
            alg.BlockSize = this.blockSize * 8;
            alg.IV = this.iv;
            alg.KeySize = this.key.Length * 8;
            alg.Key = this.key;
            ICryptoTransform decryptor = alg.CreateDecryptor();
            decryptor.TransformBlock(this.encryptedData, 0, this.encryptedData.Length, data, 0);
            decryptor.TransformFinalBlock(this.encryptedData, 0, this.encryptedData.Length);
            alg.Clear();

            this.stopTime = HiPerfTimer.GetPeformanceCount();

        }
        public byte[] Encrypted
        {
            get { return this.encryptedData; }
        }
        public byte[] Decrypted
        {
            get { return this.data; }
        }
        public byte[] IV
        {
            get { return this.iv; }
        }
        public double ExecutionTime
        {
            get
            {
                long freqency = HiPerfTimer.GetPerformanceFrequency();
                return (double)(this.stopTime - this.startTime) / (double)freqency;
            }
        }
    }
    public class DES
    {
        byte[] encryptedData;
        byte[] iv;
        byte[] data;
        byte[] key;
        short blockSize;
        CipherMode mode;
        long startTime = 0;
        long stopTime = 0;

        public DES(byte[] Data, byte[] Key, short BlockSize, CipherMode Mode)
        {
            this.startTime = HiPerfTimer.GetPeformanceCount();

            this.blockSize = BlockSize;
            this.data = Data;
            this.key = Key;
            this.mode = Mode;

            byte paddingLength = (byte)(this.blockSize - ((this.data.Length + 1) % this.blockSize));

            this.encryptedData = new byte[(this.data.Length + 1) + paddingLength];
            this.data.CopyTo(encryptedData, 0);
            int index = data.Length;
            for (byte i = 1; i <= paddingLength; i++)
                encryptedData[index++] = i;
            this.encryptedData[index] = paddingLength;
            this.data = null;

            this.data = new byte[encryptedData.Length];
            this.encryptedData.CopyTo(data, 0);
        }
        public DES(byte[] Data, byte[] Key, short BlockSize, CipherMode Mode, PROTOCOLS NextProtocol)
        {
            this.startTime = HiPerfTimer.GetPeformanceCount();

            this.blockSize = BlockSize;
            this.data = Data;
            this.key = Key;
            this.mode = Mode;

            byte paddingLength = (byte)(this.blockSize - ((this.data.Length + 2) % this.blockSize));

            this.encryptedData = new byte[(this.data.Length + 2) + paddingLength];
            this.data.CopyTo(encryptedData, 0);
            int index = data.Length;
            for (byte i = 1; i <= paddingLength; i++)
                encryptedData[index++] = i;
            this.encryptedData[index++] = paddingLength;
            this.encryptedData[index] = (byte)NextProtocol;
            this.data = null;

            this.data = new byte[encryptedData.Length];
            this.encryptedData.CopyTo(data, 0);
        }
        public DES(byte[] EncryptedData, byte[] Key, short BlockSize, CipherMode Mode, byte[] IV)
        {
            this.startTime = HiPerfTimer.GetPeformanceCount();

            this.blockSize = BlockSize;
            this.encryptedData = EncryptedData;
            this.key = Key;
            this.iv = IV;
            this.mode = Mode;

            this.data = new byte[encryptedData.Length];
        }
        public void Encrypt()
        {
            DESCryptoServiceProvider alg = new DESCryptoServiceProvider();
            alg.Mode = this.mode;
            alg.Padding = PaddingMode.None;
            alg.BlockSize = this.blockSize * 8;
            alg.GenerateIV();
            alg.KeySize = this.key.Length * 8;
            alg.Key = this.key;
            ICryptoTransform encryptor = alg.CreateEncryptor();
            encryptor.TransformBlock(this.data, 0, this.data.Length, this.encryptedData, 0);
            encryptor.TransformFinalBlock(this.data, 0, this.data.Length);
            iv = alg.IV;
            alg.Clear();

            this.stopTime = HiPerfTimer.GetPeformanceCount();

        }
        public void Decrypt()
        {
            DESCryptoServiceProvider alg = new DESCryptoServiceProvider();
            alg.Mode = this.mode;
            alg.Padding = PaddingMode.None;
            alg.BlockSize = this.blockSize * 8;
            alg.IV = this.iv;
            alg.KeySize = this.key.Length * 8;
            alg.Key = this.key;
            ICryptoTransform decryptor = alg.CreateDecryptor();
            decryptor.TransformBlock(this.encryptedData, 0, this.encryptedData.Length, data, 0);
            decryptor.TransformFinalBlock(this.encryptedData, 0, this.encryptedData.Length);
            alg.Clear();

            this.stopTime = HiPerfTimer.GetPeformanceCount();

        }
        public byte[] Encrypted
        {
            get { return this.encryptedData; }
        }
        public byte[] Decrypted
        {
            get { return this.data; }
        }
        public byte[] IV
        {
            get { return this.iv; }
        }
        public double ExecutionTime
        {
            get
            {
                long freqency = HiPerfTimer.GetPerformanceFrequency();
                return (double)(this.stopTime - this.startTime) / (double)freqency;
            }
        }
    }
    public class DES3
    {
        byte[] encryptedData;
        byte[] iv;
        byte[] data;
        byte[] key;
        short blockSize;
        CipherMode mode;
        long startTime = 0;
        long stopTime = 0;

        public DES3(byte[] Data, byte[] Key, short BlockSize, CipherMode Mode)
        {
            this.startTime = HiPerfTimer.GetPeformanceCount();

            this.blockSize = BlockSize;
            this.data = Data;
            this.key = Key;
            this.mode = Mode;

            byte paddingLength = (byte)(this.blockSize - ((this.data.Length + 1) % this.blockSize));

            this.encryptedData = new byte[(this.data.Length + 1) + paddingLength];
            this.data.CopyTo(encryptedData, 0);
            int index = data.Length;
            for (byte i = 1; i <= paddingLength; i++)
                encryptedData[index++] = i;
            this.encryptedData[index] = paddingLength;
            this.data = null;

            this.data = new byte[encryptedData.Length];
            this.encryptedData.CopyTo(data, 0);
        }
        public DES3(byte[] Data, byte[] Key, short BlockSize, CipherMode Mode, PROTOCOLS NextProtocol)
        {
            this.startTime = HiPerfTimer.GetPeformanceCount();

            this.blockSize = BlockSize;
            this.data = Data;
            this.key = Key;
            this.mode = Mode;

            byte paddingLength = (byte)(this.blockSize - ((this.data.Length + 2) % this.blockSize));

            this.encryptedData = new byte[(this.data.Length + 2) + paddingLength];
            this.data.CopyTo(encryptedData, 0);
            int index = data.Length;
            for (byte i = 1; i <= paddingLength; i++)
                encryptedData[index++] = i;
            this.encryptedData[index++] = paddingLength;
            this.encryptedData[index] = (byte)NextProtocol;
            this.data = null;

            this.data = new byte[encryptedData.Length];
            this.encryptedData.CopyTo(data, 0);
        }
        public DES3(byte[] EncryptedData, byte[] Key, short BlockSize, CipherMode Mode, byte[] IV)
        {
            this.startTime = HiPerfTimer.GetPeformanceCount();

            this.blockSize = BlockSize;
            this.encryptedData = EncryptedData;
            this.key = Key;
            this.iv = IV;
            this.mode = Mode;

            this.data = new byte[encryptedData.Length];
        }
        public void Encrypt()
        {
            TripleDESCryptoServiceProvider alg = new TripleDESCryptoServiceProvider();
            alg.Mode = this.mode;
            alg.Padding = PaddingMode.None;
            alg.BlockSize = this.blockSize * 8;
            alg.GenerateIV();
            alg.KeySize = this.key.Length * 8;
            alg.Key = this.key;
            ICryptoTransform encryptor = alg.CreateEncryptor();
            encryptor.TransformBlock(this.data, 0, this.data.Length, this.encryptedData, 0);
            encryptor.TransformFinalBlock(this.data, 0, this.data.Length);
            iv = alg.IV;
            alg.Clear();

            this.stopTime = HiPerfTimer.GetPeformanceCount();

        }
        public void Decrypt()
        {
            TripleDESCryptoServiceProvider alg = new TripleDESCryptoServiceProvider();
            alg.Mode = this.mode;
            alg.Padding = PaddingMode.None;
            alg.BlockSize = this.blockSize * 8;
            alg.IV = this.iv;
            alg.KeySize = this.key.Length * 8;
            alg.Key = this.key;
            ICryptoTransform decryptor = alg.CreateDecryptor();
            decryptor.TransformBlock(this.encryptedData, 0, this.encryptedData.Length, data, 0);
            decryptor.TransformFinalBlock(this.encryptedData, 0, this.encryptedData.Length);
            alg.Clear();

            this.stopTime = HiPerfTimer.GetPeformanceCount();

        }
        public byte[] Encrypted
        {
            get { return this.encryptedData; }
        }
        public byte[] Decrypted
        {
            get { return this.data; }
        }
        public byte[] IV
        {
            get { return this.iv; }
        }
        public double ExecutionTime
        {
            get
            {
                long freqency = HiPerfTimer.GetPerformanceFrequency();
                return (double)(this.stopTime - this.startTime) / (double)freqency;
            }
        }
    }

}
