using System.Data.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pets_Adpotion.DAL;
using Pets_Adpotion.Helpers;

namespace Pets_Adpotion.Services
{
    public class DropDownListsHelper : IDropDownListsHelper
    {
        private readonly DatabaseContext _context;

        public DropDownListsHelper(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetDDLTypesAsync()
        {
            List<SelectListItem> listTypes = await _context.Types
                .Select(c => new SelectListItem
                {
                    Text = c.Name, //Col
                    Value = c.Id.ToString(), //Guid                    
                })
                .OrderBy(c => c.Text)
                .ToListAsync();

            listTypes.Insert(0, new SelectListItem
            {
                Text = "Seleccione una categoría...",
                Value = Guid.Empty.ToString(), //Cambio el 0 por Guid.Empty ya que debo manejar el mismo tipo de dato en todo el DDL
                Selected = true //Le coloco esta propiedad para que me salga seleccionada por defecto desde la UI
            });

            return listTypes;
        }

        

       
    }
}
