namespace Intranet.Entities;

public class RoleLevel : BaseEntity<string>
{
    public string LevelName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string LevelColor { get; set; } = default!;

    public string? RoleLevelIcon { get; set; }
    public int Level { get; set; }
}
