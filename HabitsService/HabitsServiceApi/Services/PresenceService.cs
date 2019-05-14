using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using ServiceRegistry.Client;
using System.Net.Http;

namespace HabitsServiceApi.Services
{
    public class PresenceService : IHostedService
    {
        private ILogger<PresenceService> _logger;
        private Client _client;
        private Guid _serviceId;
        public PresenceService(ILogger<PresenceService> logger)
        {
            _logger = logger;
            _client = new Client(new HttpClient());
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting habits service...");
            var ip = GetIP();
            _logger.LogInformation($"IP: {ip}");
            var registration = await _client.RegisterService(new ServiceDefinition
            {
                ServiceType = "Habits",
                IpAddress = ip,
                Port = 5080,
                ApiVersion = "1"

            });

            _serviceId = registration.Id;
        }


        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping habits service...");
            await _client.DeleteService(_serviceId);}

        private string GetIP()
        {
            var hostname = Dns.GetHostName();
            var ipEntry = Dns.GetHostAddresses(hostname).Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).First();
            return ipEntry.ToString();
        }
    }
}
