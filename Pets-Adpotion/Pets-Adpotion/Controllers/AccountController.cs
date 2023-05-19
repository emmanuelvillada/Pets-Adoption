using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc;
using Pets_Adpotion.DAL.Entities;
using Pets_Adpotion.DAL;
using Pets_Adpotion.Enums;
using Pets_Adpotion.Helpers;
using Pets_Adpotion.Models;
using ECommerce_MW.Models;

namespace Pets_Adpotion.Controllers
{

    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly DatabaseContext _context;
        private readonly IDropDownListsHelper _ddlHelper;
       

        public AccountController(IUserHelper userHelper, DatabaseContext context, IDropDownListsHelper dropDownListsHelper)
        {
            _userHelper = userHelper;
            _context = context;
            _ddlHelper = dropDownListsHelper;
            
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(loginViewModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos.");
            }
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Unauthorized()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            Guid emptyGuid = new Guid(); //New Guid for example: 1515fsaf-1215gas-1ga15-a41ga

            AddUserViewModel addUserViewModel = new()
            {
              
                UserType = UserType.User,
            };

            return View(addUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AddUserViewModel addUserViewModel)
        {
            if (ModelState.IsValid)
            {              

                
                addUserViewModel.CreatedDate = DateTime.Now;

                User user = await _userHelper.AddUserAsync(addUserViewModel);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Este correo ya está siendo usado.");
                    await FillDropDownListLocation(addUserViewModel);
                    return View(addUserViewModel);
                }

                //Autologeamos al nuevo usuario que se registra
                LoginViewModel loginViewModel = new()
                {
                    Password = addUserViewModel.Password,
                    RememberMe = false,
                    Username = addUserViewModel.Username
                };

                var login = await _userHelper.LoginAsync(loginViewModel);

                if (login.Succeeded) return RedirectToAction("Index", "Home");
            }

            await FillDropDownListLocation(addUserViewModel);
            return View(addUserViewModel);
        }

        private async Task FillDropDownListLocation(AddUserViewModel addUserViewModel)
        {
            addUserViewModel.Countries = await _ddlHelper.GetDDLTypesAsync();
            
        }

        public async Task<IActionResult> EditUser()
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null) return NotFound();

            EditUserViewModel editUserViewModel = new()
            {
                Address = user.Address,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Id = Guid.Parse(user.Id),
                Document = user.Document
            };

            return View(editUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel editUserViewModel)
        {
            if (ModelState.IsValid)
            {
                

                User user = await _userHelper.GetUserAsync(User.Identity.Name);

                user.FirstName = editUserViewModel.FirstName;
                user.LastName = editUserViewModel.LastName;
                user.Address = editUserViewModel.Address;
                user.PhoneNumber = editUserViewModel.PhoneNumber;
                user.Document = editUserViewModel.Document;

                await _userHelper.UpdateUserAsync(user);
                return RedirectToAction("Index", "Home");
            }

            editUserViewModel.Countries = await _ddlHelper.GetDDLTypesAsync();
           

            return View(editUserViewModel);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                if (changePasswordViewModel.OldPassword == changePasswordViewModel.NewPassword)
                {
                    ModelState.AddModelError(string.Empty, "Debes ingresar una contraseña diferente a la actual.");
                    return View(changePasswordViewModel);
                }

                User user = await _userHelper.GetUserAsync(User.Identity.Name);

                if (user != null)
                {
                    IdentityResult result = await _userHelper.ChangePasswordAsync(user, changePasswordViewModel.OldPassword, changePasswordViewModel.NewPassword);
                    if (result.Succeeded) return RedirectToAction("EditUser");
                    else ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                }
                else ModelState.AddModelError(string.Empty, "Usuario no encontrado");
            }

            return View(changePasswordViewModel);
        }

  
    }
}


