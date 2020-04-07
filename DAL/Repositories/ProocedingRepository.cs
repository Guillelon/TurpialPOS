using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProceedingRepository
    {
        private TurpialPOSDbContext _context;

        public ProceedingRepository()
        {
            _context = new TurpialPOSDbContext();
        }

        public IList<Proceeding> GetAll(int storeId)
        {
            return _context.Proceeding.Where(c => c.IsActive && c.StoreId == storeId).ToList();
        }

        public Proceeding Get(int id)
        {
            return _context.Proceeding.Where(c => c.Id == id).FirstOrDefault();
        }

        public void Delete(int id)
        {
            var provider = _context.Proceeding.Where(p => p.Id == id).FirstOrDefault();
            if (provider != null)
            {
                provider.Deactivate();
                _context.Entry(provider).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public Proceeding Add(Proceeding Proceeding)
        {
            _context.Proceeding.Add(Proceeding);
            _context.SaveChanges();
            return Proceeding;
        }

        public Proceeding Edit(Proceeding ProceedingEdited)
        {
            var Proceeding = _context.Proceeding.Where(p => p.Id == ProceedingEdited.Id).FirstOrDefault();
            if (Proceeding != null)
            {
                Proceeding.OrderNumber = ProceedingEdited.OrderNumber;
                Proceeding.InstitutionName = ProceedingEdited.InstitutionName;
                Proceeding.ProductDescription = ProceedingEdited.ProductDescription;
                Proceeding.ProductSanitaryCode = ProceedingEdited.ProductSanitaryCode;
                Proceeding.Paid = ProceedingEdited.Paid;
                Proceeding.Notes = ProceedingEdited.Notes;
                _context.Entry(Proceeding).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
            return Proceeding;
        }
    }
}
