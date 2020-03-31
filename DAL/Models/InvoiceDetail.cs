using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class InvoiceDetail
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Detail { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public bool Paid { get; set; }
        
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }

        public InvoiceDetail()
        {
            CreateDate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            Amount = 0m;
        }
    }
}
