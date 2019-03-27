using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Websocket_Notify
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options => {
                    options.Listen(System.Net.IPAddress.Any, WsConst.appPort);
                })
                .Build();
    }
}
