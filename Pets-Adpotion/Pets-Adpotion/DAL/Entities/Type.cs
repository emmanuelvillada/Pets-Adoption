using System.ComponentModel.DataAnnotations;

namespace Pets_Adpotion.DAL.Entities
{
    public class Type : Entity_Id
    {
        [Display(Name ="Tipo")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        public string? Description { get; set; }

        public ICollection<Animal> Animals { get; set; }
    }
}
