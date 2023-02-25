using Microsoft.AspNetCore.Http;

namespace Intranet.DataObject;

public class TestUploadFileDTO2
{
    public IFormFile File { get; set; }
}

public class TestUploadFileDTO3
{
    public int Id { get; set; }

    public IFormFile File { get; set; }
}