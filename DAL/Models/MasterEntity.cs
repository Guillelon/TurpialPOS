using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MasterEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public bool IsActive { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; }

        public MasterEntity()
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
