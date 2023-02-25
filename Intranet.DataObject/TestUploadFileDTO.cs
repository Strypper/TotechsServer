using Microsoft.AspNetCore.Http;

namespace Intranet.DataObject;

public class TestUploadFileDTO
{
    public int Id { get; set; }
    public IFormFile Photos { get; set; }
}
