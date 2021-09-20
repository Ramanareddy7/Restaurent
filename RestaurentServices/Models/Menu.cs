using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurentServices.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        [ForeignKey("RestaurentId")]
       
        public int RestaurentId { get; set; }
       
        public List<Category> categories { get; set; }
        

    }
}
