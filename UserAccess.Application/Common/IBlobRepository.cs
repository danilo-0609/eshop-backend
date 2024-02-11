using BuildingBlocks.Application.Blobs;

namespace UserAccess.Application.Common;

public interface IBlobRepository
{
    public Task<BlobObject> GetBlobAsync(string name);

    public Task<string> UploadFileBlobAsync(string filePath, string fileName);

    public Task UploadContentBlobAsync(string content, string FileName);

    public Task DeleteBlobAsync(string name);
}
