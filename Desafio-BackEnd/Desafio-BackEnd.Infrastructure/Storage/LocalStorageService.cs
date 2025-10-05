namespace Desafio_BackEnd.Infrastructure.Storage;

public class LocalStorageService : IStorageService
{
    private readonly string _basePath;

    public LocalStorageService(string basePath = "Uploads/CnhImages")
    {
        _basePath = Path.Combine(Directory.GetCurrentDirectory(), basePath);

        if (!Directory.Exists(_basePath))
            Directory.CreateDirectory(_basePath);
    }

    public async Task<string> UploadAsync(string fileName, Stream fileStream)
    {
        var filePath = Path.Combine(_basePath, fileName);

        using var file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        await fileStream.CopyToAsync(file);

        return filePath;
    }
}
