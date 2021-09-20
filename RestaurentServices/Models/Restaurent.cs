using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurentServices.Models
{
    public class Restaurent
    {
        public int RestaurentId { get; set; }
        public string RestaurentName { get; set; }
        public string Address { get; set; }
        public string RestaurentImage {  get; set; }
        public List<Menu> menu{ get; set; }   

    }
}
