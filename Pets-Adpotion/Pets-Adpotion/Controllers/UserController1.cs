using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Pets_Adpotion.DAL;
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
            return View(await _context.Users
                .Include(a => a.Animals)
                .ToListAsync());
        }
    }
}
