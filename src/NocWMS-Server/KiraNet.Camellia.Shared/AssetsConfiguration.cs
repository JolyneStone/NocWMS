using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace KiraNet.Camellia.Shared
{
    public class AssetsConfiguration
    {
        private static Lazy<AssetsConfiguration> _instance = new Lazy<AssetsConfiguration>(() => new AssetsConfiguration());

        public static AssetsConfiguration Configs => _instance.Value;

        private IConfigurationRoot _configuration;
        private AssetsConfiguration()
        {
            var builder = new ConfigurationBuilder()
             //.SetBasePath(@"C:\Users\99752\Desktop\KiraNet.Camellia\Server\KiraNet.Camellia.Shared")
             .SetBasePath(Directory.GetParent(System.Reflection.Assembly.GetEntryAssembly().Location).FullName)
             .AddJsonFile("settings.json", optional: false, reloadOnChange: true)
             .AddEnvironmentVariables();
            _configuration = builder.Build();
        }

        public string Assets  => _configuration["Assets"];
    }
}
