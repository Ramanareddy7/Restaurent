using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurentServices.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        [ForeignKey("MenuId")]
        public int MenuId { get; set; }

        public List<Item> Items { get; set; }   
        
       
    }
}
