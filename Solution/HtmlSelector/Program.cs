using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace HtmlSelector
{
    public class Program
    {
        private static readonly string APP_VERSION = "0.0.0.1";

        /// <summary>Returns assembly version.</summary>
        public static string ApplicationVersion { get { return APP_VERSION; } }

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
