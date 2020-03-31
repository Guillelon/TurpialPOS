using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class RegisterCashExit
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public bool CashEntering { get; set; }
        public virtual Register Register { get; set; }
        public int RegisterId { get; set; }

        public RegisterCashExit()
        {
            CreateDate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            Amount = 0m;
        }
    }
}
