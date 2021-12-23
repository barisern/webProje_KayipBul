using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projeKayıpBul.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<LostItem> LostCats { get; set; }
        public IEnumerable<LostItem> LostDogs { get; set; }
        public IEnumerable<LostItem> LostButFound { get; set; }
    }
}
