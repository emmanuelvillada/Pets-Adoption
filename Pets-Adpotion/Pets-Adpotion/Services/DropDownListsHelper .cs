﻿using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IEnumerable<SelectListItem>> GetDDLCategoriesAsync()
        {
            List<SelectListItem> listCategories = await _context.Categories
                .Select(c => new SelectListItem
                {
                    Text = c.Name, //Col
                    Value = c.Id.ToString(), //Guid                    
                })
                .OrderBy(c => c.Text)
                .ToListAsync();

            listCategories.Insert(0, new SelectListItem
            {
                Text = "Seleccione una categoría...",
                Value = Guid.Empty.ToString(), //Cambio el 0 por Guid.Empty ya que debo manejar el mismo tipo de dato en todo el DDL
                Selected = true //Le coloco esta propiedad para que me salga seleccionada por defecto desde la UI
            });

            return listCategories;
        }

        public async Task<IEnumerable<SelectListItem>> GetDDLCategoriesAsync(IEnumerable<Category> filterCategories)
        {
            List<Category> categories = await _context.Categories.ToListAsync(); //me traigo TODAS las categorías que tengo guardadas en BD
            List<Category> categoriesFiltered = new(); //aquí declaro una lista vacía que es la que tendrá los filtros

            foreach (Category category in categories)
                if (!filterCategories.Any(c => c.Id == category.Id))
                    categoriesFiltered.Add(category);

            List<SelectListItem> listCategories = categoriesFiltered
                .Select(c => new SelectListItem
                {
                    Text = c.Name, //Col
                    Value = c.Id.ToString(), //Guid                    
                })
                .OrderBy(c => c.Text)
                .ToList();

            listCategories.Insert(0, new SelectListItem
            {
                Text = "Seleccione una categoría...",
                Value = Guid.Empty.ToString(),
                Selected = true
            });

            return listCategories;
        }

        public async Task<IEnumerable<SelectListItem>> GetDDLCountriesAsync()
        {
            List<SelectListItem> listCountries = await _context.Countries
                .Select(c => new SelectListItem
                {
                    Text = c.Name, //Col
                    Value = c.Id.ToString(), //Guid                    
                })
                .OrderBy(c => c.Text)
                .ToListAsync();

            listCountries.Insert(0, new SelectListItem
            {
                Text = "Seleccione un país...",
                Value = Guid.Empty.ToString(),
                Selected = true
            });

            return listCountries;
        }

        public async Task<IEnumerable<SelectListItem>> GetDDLStatesAsync(Guid countryId)
        {
            List<SelectListItem> listStatesByCountryId = await _context.States
                .Where(s => s.Country.Id == countryId)
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString(),
                })
                .OrderBy(s => s.Text)
                .ToListAsync();

            listStatesByCountryId.Insert(0, new SelectListItem
            {
                Text = "Seleccione un estado...",
                Value = Guid.Empty.ToString(),
                Selected = true
            });

            return listStatesByCountryId;
        }

        public async Task<IEnumerable<SelectListItem>> GetDDLCitiesAsync(Guid stateId)
        {
            List<SelectListItem> listCitiesByStateId = await _context.Cities
                .Where(c => c.State.Id == stateId)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                })
                .OrderBy(c => c.Text)
                .ToListAsync();

            listCitiesByStateId.Insert(0, new SelectListItem
            {
                Text = "Seleccione una ciudad...",
                Value = Guid.Empty.ToString(),
                Selected = true //Le coloco esta propiedad para que me salga seleccionada por defecto desde la UI
            });

            return listCitiesByStateId;
        }
    }
}
