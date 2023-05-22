using Pets_Adpotion.DAL.Entities;
using Pets_Adpotion.Enums;
using Pets_Adpotion.Helpers;
using System.Data.Entity;
using System.Diagnostics.Metrics;
using static System.Net.Mime.MediaTypeNames;
using Type = Pets_Adpotion.DAL.Entities.Type;

namespace Pets_Adpotion.DAL
{
    public class SeederDb
    {
        private readonly DatabaseContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IAzureBlobHelper _azureBlobHelper;

        public SeederDb(DatabaseContext context, IUserHelper userHelper, IAzureBlobHelper azureBlobHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _azureBlobHelper = azureBlobHelper;
        }

        public async Task SeederAsync()
        {
            await _context.Database.EnsureCreatedAsync(); // me reemplaza el comando update-database    
            await PopulateRolesAsync();
            await PopulateUserAsync("Steve", "Jobs", "steve_jobs_admin@yopmail.com", "3002323232", "Street Apple", "102030", "SteveJobs.png", UserType.Admin);
            await PopulateUserAsync("Bill", "Gates", "bill_gates_admin@yopmail.com", "4005656656", "Street Microsoft", "405060", "BillGates.png", UserType.User);
            await PopulateAnimalsAsync();
            await _context.SaveChangesAsync();
        }

        
        

        private async Task PopulateRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task PopulateUserAsync(
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            string document,
            string image,
            UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);

            if (user == null)
            {
                Guid imageId = await _azureBlobHelper.UploadAzureBlobAsync
                    ($"{Environment.CurrentDirectory}\\wwwroot\\images\\users\\{image}", "users");

                user = new User
                {
                    CreatedDate = DateTime.Now,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    
                    UserType = userType,
                    
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }
        }

        private async Task PopulateAnimalsAsync()
        {
            if (!_context.Animals.Any())
            {
                await AddAnimalAsync("Pitbull 100 kilos", "Princesa", 5, new Type {Name = "Perro" },"Princesa.png") ;
               
            }
        }

        private async Task AddAnimalAsync(string description, string name,int age, Type type
            , string image)
        {
            Animal animal = new()
            {
                Description = description,
                Name = name,
                Age = age,
               
                Type = type,
                
                ImageId = await _azureBlobHelper.UploadAzureBlobAsync($"{Environment.CurrentDirectory}\\wwwroot\\images\\animals\\{image}", "products")
            };

            


           
        }

    }
}
