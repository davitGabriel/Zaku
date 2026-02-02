using System.ComponentModel.DataAnnotations;

namespace Zaku.Domain.Entities
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
