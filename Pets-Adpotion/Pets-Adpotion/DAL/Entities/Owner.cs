using System.ComponentModel.DataAnnotations;

namespace Pets_Adpotion.DAL.Entities
{
    public class Owner : Entity_Id
    {
        

        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [StringLength(70)]
        public string Name { get; set; }


        [Display(Name= "Pets")]
        public ICollection<Animal> Animals { get; set; }
    }
}
