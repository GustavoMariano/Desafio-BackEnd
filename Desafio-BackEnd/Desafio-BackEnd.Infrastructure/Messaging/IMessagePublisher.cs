namespace Desafio_BackEnd.Infrastructure.Messaging;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T message, string queueName);
}
