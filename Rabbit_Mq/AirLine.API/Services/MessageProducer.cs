using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace AirLine.API.Services
{
    public class MessageProducer : IMessageProducer
    {
        public void SendingMessage<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };
            var connection = factory.CreateConnection();
            using var chaneel = connection.CreateModel();
            chaneel.QueueDeclare("bookings", durable: true, exclusive: true);
            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);
            chaneel.BasicPublish("", "bookings",body:body);
        }
    }
}
