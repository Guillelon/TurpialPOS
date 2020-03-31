using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Username { get; set; }
        public decimal Amount { get; set; }
        public string Reference { get; set; }

        public virtual IList<PaymentDetail> PaymentDetails { get; set; }

        public int PaymentTypeId { get; set; }
        public virtual PaymentType PaymentType { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; }

        public Payment()
        {
            CreateDate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            Amount = 0m;
            Reference = string.Empty;
        }
    }
}
