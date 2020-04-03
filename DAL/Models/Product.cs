using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CodeBar { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public decimal Tax { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DeactivationDate { get; set; }

        //iFarmaLabs needs
        public string Code { get; set; }
        public string OrderNumber { get; set; }
        public string CatalogNumber { get; set; }
        public string SanitaryCode { get; set; }
        public string Brand { get; set; }
        public string Factory { get; set; }
        public string Country { get; set; }
        public int Stock { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; }

        public Product()
        {
            IsActive = true;
            CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
        }

        public void Deactivate() {
            IsActive = false;
            DeactivationDate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
        }
    }
}
