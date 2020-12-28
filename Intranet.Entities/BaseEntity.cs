using System.ComponentModel.DataAnnotations;

namespace Intranet.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
