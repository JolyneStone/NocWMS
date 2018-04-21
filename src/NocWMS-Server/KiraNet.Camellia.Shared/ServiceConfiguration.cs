using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace KiraNet.Camellia.Shared
{
    public class ServiceConfiguration
    {
        private static Lazy<ServiceConfiguration> _instance = new Lazy<ServiceConfiguration>(()=> new ServiceConfiguration());

        public static ServiceConfiguration Configs => _instance.Value;

        private IConfigurationRoot _configuration;
        private ServiceConfiguration()
        {
               var builder = new ConfigurationBuilder()
                //.SetBasePath(@"C:\Users\99752\Desktop\KiraNet.Camellia\Server\KiraNet.Camellia.Shared")
                .SetBasePath(Directory.GetParent(System.Reflection.Assembly.GetEntryAssembly().Location).FullName)
                .AddJsonFile("settings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();
        }

        public string ServiceName => _configuration["ServiceSettings:ServiceName"];
        public string ServiceDisplay => _configuration["ServiceSettings:ServiceDisplay"];
        public string AuthId => _configuration["Services:Authorization:AuthId"];
        public string AuthName => _configuration["Services:Authorization:AuthName"];
        public string AuthBase => _configuration["Services:Authorization:AuthBase"];
        public string ApiId => _configuration["Services:Api:ClientId"];
        public string ApiName=> _configuration["Services:Api:ClientName"];
        public string ApiBase => _configuration["Services:Api:ClientBase"];
        public string ClientId => _configuration["Services:Client:ClientId"];
        public string ClientName => _configuration["Services:Client:ClientName"];
        public string ClientBase => _configuration["Services:Client:ClientBase"];

    }
}
