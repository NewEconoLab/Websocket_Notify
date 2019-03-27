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

        public static int ping_interval_minutes = 2;   // 单位: 分钟
        public static int loop_interval_seconds = 2;   // 单位: 秒

    }
}
