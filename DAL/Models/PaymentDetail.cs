using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PaymentDetail
    {
        public int Id { get; set; }
        public int? InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
        public int? InvoiceDetailId { get; set; }
        public virtual InvoiceDetail InvoiceDetail { get; set; }
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
