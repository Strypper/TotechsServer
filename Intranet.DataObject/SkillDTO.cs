namespace Intranet.DataObject;

public class SkillDTO : BaseDTO<int>
{
    public string Name { get; set; } = default!;
    public double SkillValue { get; set; }
}
