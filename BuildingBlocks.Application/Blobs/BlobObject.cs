namespace BuildingBlocks.Application.Blobs;

public sealed record BlobObject(Stream? Content, string? ContentType);
