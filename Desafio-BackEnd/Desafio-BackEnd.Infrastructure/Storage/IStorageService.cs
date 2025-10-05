namespace Desafio_BackEnd.Infrastructure.Storage;

public interface IStorageService
{
    Task<string> UploadAsync(string fileName, Stream fileStream);
}
