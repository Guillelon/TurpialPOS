using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ClientRepository
    {
        private TurpialPOSDbContext _context;

        public ClientRepository()
        {
            _context = new TurpialPOSDbContext();
        }

        public Client Add(Client client)
        {
            _context.Client.Add(client);
            _context.SaveChanges();
            return client;  
        }

        public Client Edit(Client clientEdited)
        {
            var client = _context.Client.Where(p => p.Id == clientEdited.Id).FirstOrDefault();
            if (client != null)
            {
                client.Name = clientEdited.Name;
                client.Address = clientEdited.Address;
                client.ContactName = clientEdited.ContactName;
                client.ContactPhone = clientEdited.ContactPhone;
                client.ContactEmail = clientEdited.ContactEmail;
                client.LegalId = clientEdited.LegalId; 
                client.Notes = clientEdited.Notes;   
                _context.Entry(client).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
            return client;
        }

        public IList<Client> GetAll(int storeId)
        {
            return _context.Client.Where(c => c.IsActive && c.StoreId == storeId).ToList();
        }

        public Client Get(int id)
        {
            return _context.Client.Where(c => c.Id == id).FirstOrDefault();
        }

        public void Delete(int id)
        {
            var client = _context.Client.Where(p => p.Id == id).FirstOrDefault();
            if (client != null)
            {
                client.Deactivate();
                _context.Entry(client).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public Client GetByLegalId(string legalId)
        {
            return _context.Client.Where(c => c.LegalId == legalId).FirstOrDefault();
        }
    }
}
