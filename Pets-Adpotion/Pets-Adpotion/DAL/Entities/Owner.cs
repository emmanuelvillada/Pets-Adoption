using System.ComponentModel.DataAnnotations;

namespace Pets_Adpotion.DAL.Entities
{
    public class Owner
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [StringLength(70)]
        public string Name { get; set; }


        [Display(Name= "Pets")]
        public ICollection<Animal> Animals { get; set; }
    }
}
