﻿using System;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace RabbitWrapper
{
    public class Wrapper
    {
        private readonly string _rabbitUrl;
        private IModel _connection;
        private const string RoutingKey = "";
        private const string Exchange = "messages";
        public Wrapper(string url) => _rabbitUrl = url;

        public void Start()
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(_rabbitUrl);
            var connection = factory.CreateConnection();
            _connection = connection.CreateModel();
        }

        public void SendHabit(Guid userId, float score, int difficulty, string title)
        {
            var message = new Message
            {
                Entity = EntityType.Habit,
                Difficulty = difficulty,
                Score = score,
                UserId = userId,
                Title = title
            };
            var serialized = SerializeObject(message);
            _connection.BasicPublish(Exchange, RoutingKey, false, null, serialized);
        }

        private byte[] SerializeObject(Message message)
        {
            var json = JsonConvert.SerializeObject(message);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}
