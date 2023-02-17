using Intranet.Entities;

namespace Intranet.DataObject;

public class MediaAssetsDTO : BaseDTO<int>
{
    public string? Title { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public MediaAssestType MediaAssestType { get; set; }
}

public class CreateMediaAssetsDTO
{
    public string? Title { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public MediaAssestType MediaAssestType { get; set; }
}
