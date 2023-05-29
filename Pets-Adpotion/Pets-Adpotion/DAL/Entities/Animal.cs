using System.ComponentModel.DataAnnotations;

namespace Pets_Adpotion.DAL.Entities
{
    public class Animal
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe ser de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Edad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Age { get; set; }

        [Display(Name = "Tipo de Mascota")]       
        public Animal_Type Type { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe ser de {1} caracteres.")]
        public string Description { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        [Display(Name = "adoptador")]
        public Adopter? Owner { get; set; }

        [Display(Name = "Foto Mascota")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7048/images/Image_not_available.png"
            : $"https://sales2023.blob.core.windows.net/users/{ImageId}";

        
        
    }
}
