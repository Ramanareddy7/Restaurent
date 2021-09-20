using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace RestaurentServices.Models
{
    public class Item
    {
        public int Id { get; set; }
        
        public string item { get; set; }
        public double price { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Boolean veg { get; set; }
        public Boolean nonveg { get; set; }
        public Boolean IsAvailable { get; set; }
        public string Description { get; set; }
        public string Ingredians { get; set; }
        public string ImageTitle { get; set; }
     


    }
}
