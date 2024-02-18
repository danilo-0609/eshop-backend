using Azure.Storage.Blobs;
using BuildingBlocks.Application.Blobs;
using UserAccess.Application.Abstractions;

namespace UserAccess.Infrastructure.Blobs;

internal sealed class BlobService : IBlobService
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public Task DeleteBlobAsync(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<BlobObject?> GetBlobAsync(string fileName)
    {
        try
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("users");

            var client = containerClient.GetBlobClient(fileName);

            if (!await client.ExistsAsync())
            {
                return null;
            }

            var content = await client.DownloadAsync();

            var blobProperties = await client.GetPropertiesAsync();
            string contentType = blobProperties.Value.ContentType;

            MemoryStream memoryStream = new MemoryStream();
            await content.Value.Content.CopyToAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new BlobObject(memoryStream, contentType);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<string> UploadFileBlobAsync(string filePath, string fileName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient("users");

        var blobClient = containerClient.GetBlobClient(fileName);

        await blobClient.UploadAsync(filePath, true);

        return fileName;
    }
}
