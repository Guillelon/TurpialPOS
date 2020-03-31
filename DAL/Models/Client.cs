using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string LegalId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string Notes { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public Client()
        {
            IsActive = true;
            CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
        }

        public void Deactivate()
        {
            IsActive = false;
            DeactivationDate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
        }
    }
}
