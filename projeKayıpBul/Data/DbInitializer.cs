using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using projeKayıpBul.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projeKayıpBul.Data
{
    public class DbInitializer : IDbInitializer
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, 
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async void Initialize()
        {
            try
            {
                if(_db.Database.GetPendingMigrations().Count()>0)
                {
                    _db.Database.Migrate();
                }
            }
            catch(Exception ex) { }

            if (_db.Roles.Any(r => r.Name == "Admin")) return;

            _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole("User")).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "b191210085@sakarya.edu.tr",
                Email = "b191210085@sakarya.edu.tr",
                Name = "Ömer Barış",
                Surname = "Eren",
                AboutMe = "KayıpBul Uygulaması Admin Kullanıcısıyım!",
                EmailConfirmed = true,
                PhoneNumber = "5554443322"
            }, "123").GetAwaiter().GetResult();
                
            _userManager.AddToRoleAsync(_db.Users.FirstOrDefaultAsync(u => u.Email == "b191210085@sakarya.edu.tr").GetAwaiter().GetResult(), "Admin").GetAwaiter().GetResult();
        }
    }
}
