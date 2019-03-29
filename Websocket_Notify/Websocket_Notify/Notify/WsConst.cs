namespace Websocket_Notify
{
    public class WsConst
    {
        public static int appPort = 1234;
        public static string block_mongodbConnStr_testnet = "";
        public static string block_mongodbDatabase_testnet = "";
        public static string block_mongodbConnStr_mainnet = "";
        public static string block_mongodbDatabase_mainnet = "";

        public static string[] networks = new string[] { "testnet", "mainnet" };

        public static int ping_loop_seconds = 1000;       // 单位: 秒
        public static int ping_interval_seconds = 1000;   // 单位: 秒
        public static int send_loop_seconds = 1000;       // 单位: 秒

    }
}
