using System.ComponentModel.DataAnnotations;

namespace Pets_Adpotion.DAL.Entities
{
    public class Animal_Type 
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Display(Name ="Tipo")]
        public string Name { get; set; }

        public ICollection<Animal> Animals { get; set; }
    }
}
