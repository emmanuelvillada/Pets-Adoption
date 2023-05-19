using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pets_Adpotion.Helpers
{
    public interface IDropDownListsHelper
    {
        Task<IEnumerable<SelectListItem>> GetDDLTypesAsync();
        

        
    }
}
