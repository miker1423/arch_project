using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HabitsServiceApi.Services
{
    public class PresenceService : IHostedService
    {
        private ILogger<PresenceService> _logger;
        public PresenceService(ILogger<PresenceService> logger)
            => _logger = logger;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting habits service...");
            // todo registarte en service registry
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping habits service...");
            // todo unregistarte en service registry
            return Task.CompletedTask;
        }
    }
}
