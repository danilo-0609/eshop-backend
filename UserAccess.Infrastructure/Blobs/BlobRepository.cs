using Azure.Storage.Blobs;
using BuildingBlocks.Application.Blobs;
using UserAccess.Application.Common;

namespace UserAccess.Infrastructure.Blobs;

internal sealed class BlobRepository : IBlobRepository
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobRepository(BlobServiceClient blobServiceClient)
    { 
        _blobServiceClient = blobServiceClient;
    }

    public Task DeleteBlobAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task<BlobObject> GetBlobAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task UploadContentBlobAsync(string content, string FileName)
    {
        throw new NotImplementedException();
    }

    public async Task<string> UploadFileBlobAsync(string filePath, string fileName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient("Photos");
        
        var blobClient = containerClient.GetBlobClient(fileName);

        await blobClient.UploadAsync(filePath);

        return blobClient.Uri.AbsoluteUri;
    }
}
