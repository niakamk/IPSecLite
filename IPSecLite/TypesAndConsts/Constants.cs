using System.Collections.Generic;
namespace adabtek.IPsecLite.Constants
{
    public enum PROTOCOLS
    {
        UNKNOWN = 0,
        IP = 4,
        TCP = 5,
        UDP = 17,
        ESP = 50,
        AH = 51,
        ICMP = 1
    }
    public enum ICMP_TYPE
    {
        REQUEST = 8,
        RESPONSE = 0
    }
    public enum PROTECTION_RESULTS
    {
        NONE = 0,
        OK = 1,
        AUTH_FAILED = 2,
        ENC_FAILED = 3,
        DECR_FAILED = 4,
        REPLAYED = 5,
        INVALID_MAC = 6,
        INVALID_PAD = 7,
        NO_SA = 8,
        TOO_OLD = 9
    }
    
    public static class APP_CONST
    {
        public const byte ANTI_REPLAY_WINDOW_SIZE = 16;
        public static long EVENT_COUNTER = 0;

        public const string RECEIVED_INVALID_IPv4 = "Received an invalid IPv4 datagram.";
        public const string ENTER_DESTINATION_IP = "Please enter a destination IP address.";
    }
    public static class APP_CONFIG
    {
        public static byte PACKET_LOSS_PERCENT = 0;
        public static byte SEND_OUT_OF_SEQUENCE_PERCENT = 0;
        public static byte CORRUPTED_PACKET_PERCENT = 0;

        public static string ETHERNET_IP;
        public static short ETHERNET_PORT;

        public static bool IS_GATEWAY;
        public static byte[] GATEWAY_IP;
        public static List<byte[]> HOSTS = new List<byte[]>();

        public const short ISKAMP = 500;

        public static bool FRAMED = false;

    }
}