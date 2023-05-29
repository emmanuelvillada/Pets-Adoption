using System.ComponentModel.DataAnnotations;

namespace Pets_Adpotion.Models
{
    public class EditAnimalViewModel 
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Fecha de creación")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Fecha de modificación")]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; }

        
        [Display(Name = "Edad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Age { get; set; }

        
    }
}
