using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Welcome to Vamsi Ticker Processing System!");

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest",
    VirtualHost = "/"
};
var connection = factory.CreateConnection();
using var chaneel = connection.CreateModel();
chaneel.QueueDeclare("bookings", durable:true, exclusive:false);

var consumer = new EventingBasicConsumer(chaneel);
consumer.Received += (model, eventArgs) =>
{
    // getting my byte[]
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"new ticket Processing is initiated - {message} ");
};
chaneel.BasicConsume("bookings", true, consumer);
Console.ReadKey();