using Domain.Entidades;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Publisher
{
    public class Send
    {
        public void SendMessage(Protocol protocol)
        {
            {
                var factory = new ConnectionFactory { HostName = "localhost" };

                using var connection = factory.CreateConnection();

                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: "protocols",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

                var message = JsonSerializer.Serialize(protocol); 

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: string.Empty,
                                     routingKey: "protocols",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine($" [x] Sent {message}");
            }
        }
    }
}
