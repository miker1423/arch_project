using System;
using RabbitMQ.Client;

namespace RabbitWrapper
{
    public class Wrapper
    {
        private readonly string _rabbitUrl;
        private IModel _connection;
        public Wrapper(string url) => _rabbitUrl = url;

        public void Start()
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(_rabbitUrl);
            var connection = factory.CreateConnection();
            _connection = connection.CreateModel();
        }


    }
}
