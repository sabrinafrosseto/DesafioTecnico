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
            var connected = false;

            IConnection connection = null;

            Console.WriteLine(" Tentando conexão com o Rabbit...");

            while (!connected)
            {
                try
                {
                    var factory = new ConnectionFactory { HostName = "rabbit" };

                    connection = factory.CreateConnection();

                    connected = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" ERRO: " + ex.Message);

                    Console.WriteLine(" Aguardando mais 5 segundos para tentar reconectar...");

                    Thread.Sleep(5000);
                }
            }

            Console.WriteLine(" Conectado!");

            using (connection)
            {
                using var channel = connection!.CreateModel();

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

                Console.WriteLine($" Mensagem enviada: {message}");
            }
        }
    }
}
