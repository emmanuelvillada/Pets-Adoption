
using System.ComponentModel.DataAnnotations;

namespace Pets_Adpotion.DAL.Entities
{
    public class Adopter
    {
        [Key]
        [Required]
        public Guid Id { get; set; }    

        public User? User { get; set; }    

        public ICollection<Animal> Animals { get; set; }


    }
}
