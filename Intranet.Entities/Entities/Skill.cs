namespace Intranet.Entities;

public class Skill : BaseEntity<int>
{
    public string Name { get; set; } = default!;
    public double SkillValue { get; set; }
}
