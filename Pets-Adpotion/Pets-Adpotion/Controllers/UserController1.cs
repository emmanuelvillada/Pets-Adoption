using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Pets_Adpotion.DAL;
using Pets_Adpotion.DAL.Entities;
using Pets_Adpotion.Helpers;
using Pets_Adpotion.Services;

namespace Pets_Adpotion.Controllers
{
    public class UserController1 : Controller
    {
        private IUserHelper _userHelper;
        private DatabaseContext _context;

        public UserController1(IUserHelper userHelper, DatabaseContext context) 
        {
            _userHelper = userHelper;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Animals
                .Include(t => t.Type)
                .ToListAsync());
        }

        public async Task<IActionResult> Adopt(Guid animalId)
        {
            if (animalId == null) return NotFound();

            Animal animal = await _context.Animals
                .FirstOrDefaultAsync(a => a.Id == animalId);
            if (animal == null) return NotFound();

            return View(animal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adopt(Animal animalModel)
        {

            Animal animal = await _context.Animals
               .FirstOrDefaultAsync(p => p.Id == animalModel.Id);

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();



            return RedirectToAction(nameof(Index));
        }

    }
}
