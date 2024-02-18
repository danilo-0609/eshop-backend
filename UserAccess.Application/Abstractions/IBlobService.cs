using BuildingBlocks.Application.Blobs;

namespace UserAccess.Application.Abstractions;

public interface IBlobService
{
    public Task<BlobObject?> GetBlobAsync(string name);

    public Task<string> UploadFileBlobAsync(string filePath, string fileName);

    public Task DeleteBlobAsync(string name);
}
