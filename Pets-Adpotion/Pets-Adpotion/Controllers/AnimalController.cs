using System.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Pets_Adpotion.DAL;
using Pets_Adpotion.DAL.Entities;
using Pets_Adpotion.Helpers;
using Pets_Adpotion.Models;

namespace Pets_Adpotion.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AnimalController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IDropDownListsHelper _dropDownListsHelper;
        private readonly IAzureBlobHelper _azureBlobHelper;

        public AnimalController(DatabaseContext context, IDropDownListsHelper dropDownListsHelper, IAzureBlobHelper azureBlobHelper)
        {
            _context = context;
            _dropDownListsHelper = dropDownListsHelper;
            _azureBlobHelper = azureBlobHelper;
        }

        // GET: AnimalController
        public async Task<ActionResult> Index()
        {

            return View(await _context.Animals
                .Include(a => a.AnimalTypes)
                .ToListAsync());
        }

        // GET: AnimalController/Details/5
        public async Task<ActionResult> Details(Guid? animalId)
        {
            if (animalId == null) return NotFound();

            Animal animal = await _context.Animals
                .Include(a => a.AnimalTypes)              
                .FirstOrDefaultAsync(a => a.Id == animalId);
            if (animal == null) return NotFound();

            return View(animal);
        }

        // GET: AnimalController/Create
        public async  Task<ActionResult> Create()
        {
            AddAnimalViewModel addAnimalViewModel = new()
            {
                AnimalTypes = await _dropDownListsHelper.GetDDLTypesAsync(),
            };

            return View(addAnimalViewModel);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddAnimalViewModel addAnimalViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Guid imageId = Guid.Empty;

                    if (addAnimalViewModel.ImageFile != null)
                        imageId = await _azureBlobHelper.UploadAzureBlobAsync(addAnimalViewModel.ImageFile, "products");

                    Animal animal = new()
                    {
                        Description = addAnimalViewModel.Description,
                        Name = addAnimalViewModel.Name,
                        Age = addAnimalViewModel.Age,
                        ImageId = imageId   
                    };

                    

                    

                    _context.Add(animal);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                //catch (DbUpdateException dbUpdateException)
                //{
                //    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                //    {
                //        ModelState.AddModelError(string.Empty, "Ya existe un animal con el mismo nombre.");
                //    }
                //    else
                //    {
                //        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                //    }
                //}
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            addAnimalViewModel.AnimalTypes = await _dropDownListsHelper.GetDDLTypesAsync();
            return View(addAnimalViewModel);
        }


        // GET: AnimalController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AnimalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AnimalController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AnimalController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
