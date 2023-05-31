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
                            .ToListAsync());
        }

        // GET: AnimalController/Details/5
        public async Task<ActionResult> Details(Guid? animalId)
        {
            if (animalId == null) return NotFound();

            Animal animal = await _context.Animals         
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
                
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            addAnimalViewModel.AnimalTypes = await _dropDownListsHelper.GetDDLTypesAsync();
            return View(addAnimalViewModel);
        }


        // GET: AnimalController/Edit/5
        public async Task<ActionResult> EditAsync(Guid? animalId)
        {
            if (animalId == null) return NotFound();

            Animal animal = await _context.Animals.FindAsync(animalId);
            if (animal == null) return NotFound();

            EditAnimalViewModel editAnimalViewModel = new()
            {
                Description = animal.Description,
                Id = animal.Id,
                Name = animal.Name,
                Age = animal.Age,
               
            };

            return View(editAnimalViewModel);
        }

        // POST: AnimalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(Guid? Id, EditAnimalViewModel editAnimalViewModel)
        {
            if (Id != editAnimalViewModel.Id) return NotFound();

            try
            {
                Animal animal = await _context.Animals.FindAsync(editAnimalViewModel.Id);
                animal.Description = editAnimalViewModel.Description;
                animal.Name = editAnimalViewModel.Name;
                animal.Age = editAnimalViewModel.Age;
                
                _context.Update(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }

            return View(editAnimalViewModel);
        }

        // GET: AnimalController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AnimalController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(Animal animalModel)
        {
            Animal animal = await _context.Animals
                .FirstOrDefaultAsync(p => p.Id == animalModel.Id);

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();

           

            return RedirectToAction(nameof(Index));
        }
    }
}
