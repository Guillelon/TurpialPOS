using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class PrintProductDTO
    {
        public List<int> Ids { get; set; }
        public List<Product> Products { get; set; }
        public string Date { get; set; }

        public PrintProductDTO() {
            Ids = new List<int>();
            Products = new List<Product>();
        }
    }
}
