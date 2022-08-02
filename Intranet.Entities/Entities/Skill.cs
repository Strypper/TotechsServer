namespace Intranet.Entities.Entities
{
    public class Skill : BaseEntity<int>
    {
        public string Name { get; set; }
        public double SkillValue { get; set; }
    }
}
