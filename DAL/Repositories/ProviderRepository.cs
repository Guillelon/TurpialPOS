using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProviderRepository
    {
        private TurpialPOSDbContext _context;

        public ProviderRepository()
        {
            _context = new TurpialPOSDbContext();
        }

        public IList<Provider> GetAll(int storeId)
        {
            return _context.Provider.Where(c => c.IsActive && c.StoreId == storeId).ToList();
        }

        public Provider Get(int id)
        {
            return _context.Provider.Where(c => c.Id == id).FirstOrDefault();
        }

        public Provider GetByLegalId(string legalId)
        {
            return _context.Provider.Where(c => c.LegalId == legalId).FirstOrDefault();
        }

        public void Delete(int id)
        {
            var provider = _context.Provider.Where(p => p.Id == id).FirstOrDefault();
            if (provider != null)
            {
                provider.Deactivate();
                _context.Entry(provider).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public Provider Add(Provider provider)
        {
            _context.Provider.Add(provider);
            _context.SaveChanges();
            return provider;
        }

        public Provider Edit(Provider providerEdited)
        {
            var provider = _context.Provider.Where(p => p.Id == providerEdited.Id).FirstOrDefault();
            if (provider != null)
            {
                provider.Name = providerEdited.Name;
                provider.Country = providerEdited.Country;
                provider.ContactName = providerEdited.ContactName;
                provider.ContactPhone = providerEdited.ContactPhone;
                provider.ContactEmail = providerEdited.ContactEmail;
                provider.LegalId = providerEdited.LegalId;
                provider.Notes = providerEdited.Notes;
                _context.Entry(provider).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
            return provider;
        }
    }
}
