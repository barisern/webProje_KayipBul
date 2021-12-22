using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace projeKayıpBul.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public string Comment { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public string LostItemId { get; set; }
        [ForeignKey("LostItemId")]
        public LostItem LostItem { get; set; }
    }
}
