using System.ComponentModel.DataAnnotations;

namespace Pets_Adpotion.DAL.Entities
{
    public class Entity_Id
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
    }
}
