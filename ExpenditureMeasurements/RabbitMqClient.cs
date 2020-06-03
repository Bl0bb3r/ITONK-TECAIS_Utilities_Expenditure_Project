using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;


namespace ExpenditureMeasurements
{
    public class RabbitMqClient
    {
        private readonly string _name;
        private readonly ConnectionFactory _connectionFactory;

        public RabbitMqClient(string name)
        {
            _name = name;
            var rabbitHostName = Environment.GetEnvironmentVariable("RABBITMQ_SERVICE_HOST");
            Console.WriteLine($"SERVICE_HOST: {rabbitHostName}");
            _connectionFactory = new ConnectionFactory { HostName = rabbitHostName ?? "localhost" };
        }

        public void SendMessage(object message, string routingKey)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: _name, type: "direct");

                    var messagePayloadJson = JsonConvert.SerializeObject(message);
                    var payload = Encoding.UTF8.GetBytes(messagePayloadJson);

                    channel.BasicPublish(exchange: _name, routingKey: routingKey, basicProperties: null,
                        body: payload);

                    Console.WriteLine($"Published message: {messagePayloadJson} with routing key {routingKey}");

                }
            }
        }
    }
}
