using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Consumer
{
    public class Receive
    {
        public void ReceiveMessage(Action<string> onMessageReceivedAction)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(" Tentando conexão com o Rabbit...");

                    var factory = new ConnectionFactory { HostName = "rabbit" };
                    using var connection = factory.CreateConnection();
                    using var channel = connection.CreateModel();

                    Console.WriteLine(" Conectado!");

                    channel.QueueDeclare(queue: "protocols",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    Console.WriteLine(" Aguardando mensagens...");

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($" Mensagem Recebida: {message}");

                        onMessageReceivedAction.Invoke(message);
                    };
                    channel.BasicConsume(queue: "protocols",
                                         autoAck: true,
                                         consumer: consumer);

                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" ERRO: " + ex.Message);

                    Console.WriteLine(" Aguardando mais 5 segundos para tentar reconectar...");

                    Thread.Sleep(5000);
                }
            }
        }
    }
}
