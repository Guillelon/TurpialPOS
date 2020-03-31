using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PaymentType
    {
        public int Id { get; set; }
        public string SystemName { get; set; }
        public string PublicName { get; set; }
    }
}
