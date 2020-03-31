using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public bool Paid { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid PrivateCode { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual Store Store { get; set; }
        public int StoreId { get; set; }

        public Invoice()
        {
            CreateDate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            PrivateCode = Guid.NewGuid();
            Paid = false;
            AmountPaid = 0m;
        }
    }
}
