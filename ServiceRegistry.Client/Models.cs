using System;

namespace ServiceRegistry.Client 
{
    public class ServiceDefinition 
    {
        public string ServiceType { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string ApiVersion { get; set; }
    }

    public class SavedServiceDefinition 
    {
        public ServiceDefinition ServiceDefinition { get; set; }
        public Guid Id { get; set; }
    }
}