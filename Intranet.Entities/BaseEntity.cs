using System.ComponentModel.DataAnnotations;

namespace Intranet.Entities
{
    public class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; } = default!;
    }
}
