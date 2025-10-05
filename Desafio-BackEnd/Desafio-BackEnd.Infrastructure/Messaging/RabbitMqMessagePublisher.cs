using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Desafio_BackEnd.Infrastructure.Messaging;

public class RabbitMqMessagePublisher : IMessagePublisher
{
    private readonly string _hostname;
    private readonly string _username;
    private readonly string _password;

    public RabbitMqMessagePublisher(string hostname, string username, string password)
    {
        _hostname = hostname;
        _username = username;
        _password = password;
    }

    public Task PublishAsync<T>(T message, string queueName)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _hostname,
            UserName = _username,
            Password = _password
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        channel.BasicPublish(exchange: "",
                             routingKey: queueName,
                             basicProperties: null,
                             body: body);

        return Task.CompletedTask;
    }
}
