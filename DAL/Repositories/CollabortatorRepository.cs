using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CollabortatorRepository
    {
        private TurpialPOSDbContext _context;

        public CollabortatorRepository()
        {
            _context = new TurpialPOSDbContext();
        }

        public IList<Collaborator> GetAll(int storeId)
        {
            return _context.Collaborator.Where(c => c.IsActive && c.StoreId == storeId).ToList();
        }

        public Collaborator Get(int id)
        {
            return _context.Collaborator.Where(c => c.Id == id).FirstOrDefault();
        }

        public Collaborator GetByLegalId(string legalId)
        {
            return _context.Collaborator.Where(c => c.LegalId == legalId).FirstOrDefault();
        }

        public void Delete(int id)
        {
            var provider = _context.Collaborator.Where(p => p.Id == id).FirstOrDefault();
            if (provider != null)
            {
                provider.Deactivate();
                _context.Entry(provider).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public Collaborator Add(Collaborator collaborator)
        {
            _context.Collaborator.Add(collaborator);
            _context.SaveChanges();
            return collaborator;
        }

        public Collaborator Edit(Collaborator collaboratorEdited)
        {
            var collaborator = _context.Collaborator.Where(p => p.Id == collaboratorEdited.Id).FirstOrDefault();
            if (collaborator != null)
            {
                collaborator.Name = collaboratorEdited.Name;
                collaborator.Address = collaboratorEdited.Address;
                collaborator.Phone = collaboratorEdited.Phone;
                collaborator.LegalId = collaboratorEdited.LegalId;
                collaborator.WorkArea = collaboratorEdited.WorkArea;
                _context.Entry(collaborator).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
            return collaborator;
        }
    }
}
