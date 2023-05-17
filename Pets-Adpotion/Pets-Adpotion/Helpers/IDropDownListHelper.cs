﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pets_Adpotion.Helpers
{
    public interface IDropDownListsHelper
    {
        Task<IEnumerable<SelectListItem>> GetDDLCategoriesAsync();
        Task<IEnumerable<SelectListItem>> GetDDLCategoriesAsync(IEnumerable<Category> filterCategories);

        Task<IEnumerable<SelectListItem>> GetDDLCountriesAsync();

        Task<IEnumerable<SelectListItem>> GetDDLStatesAsync(Guid countryId);

        Task<IEnumerable<SelectListItem>> GetDDLCitiesAsync(Guid stateId);
    }
}
