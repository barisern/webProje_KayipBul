using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projeKayıpBul.Data;
using projeKayıpBul.Models;
using projeKayıpBul.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace projeKayıpBul.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel HomeVM = new HomeViewModel()
            {
                LostCats = await _db.LostItem.Where(x => x.Category.Name == "Kedi").ToListAsync(),
                LostDogs = await _db.LostItem.Where(x => x.Category.Name == "Köpek").ToListAsync(),
                LostButFound = await _db.LostItem.Where(x => x.Status == Status.Found).ToListAsync(),
                Category = await _db.Category.ToListAsync()
            };
            return View(HomeVM);
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
