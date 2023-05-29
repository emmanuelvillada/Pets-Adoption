using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Pets_Adpotion.Models
{
    public class AddAnimalViewModel : EditAnimalViewModel
    {
        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Guid AnimalTypeId { get; set; }

        public IEnumerable<SelectListItem> AnimalTypes { get; set; }

        [Display(Name = "Foto")]
        public IFormFile? ImageFile { get; set; }
    }
}
