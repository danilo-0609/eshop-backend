using API.Controllers;
using API.Modules.UserAccess.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAccess.Application.Common;

namespace API.Modules.UserAccess.Endpoints;

[Route("api/[controller]")]
[ApiController]
public sealed class BlobController : ApiController
{
    private readonly IBlobRepository _blobRepository;

    public BlobController(IBlobRepository blobRepository)
    {
        _blobRepository = blobRepository;
    }

    [HttpPost("UploadBlob")]
    public async Task<IActionResult> UploadBlobFile([FromBody] BlobContentRequest model)
    {
        var result = await _blobRepository.UploadFileBlobAsync(model.FilePath, model.FileName);

        return Ok(result); 
    }
}   
