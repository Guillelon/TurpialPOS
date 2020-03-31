using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace TurpialPOS.Controllers
{
    public class ClientController : Controller
    {
        private ClientRepository _clientRepository;
        private readonly int storeId = 1;

        public ClientController()
        {
            _clientRepository = new ClientRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        public string GetClients()
        {
            var clients = _clientRepository.GetAll(storeId).Select(p => new
            {
                p.Id, p.LegalId, p.Name,p.Address,p.ContactName, p.ContactPhone, p.ContactEmail
            });
            return new JavaScriptSerializer().Serialize(clients);
        }

        [HttpGet]
        public string GetClient(int id)
        {
            var client = _clientRepository.Get(id);
            var viewModel = new
            {
                client.Id,
                client.Name,
                client.Address,
                client.ContactName,
                client.ContactPhone,
                client.Notes,
                client.ContactEmail,
                client.LegalId
            };
            return new JavaScriptSerializer().Serialize(viewModel);
        }

        [HttpPost]
        public string AddClient(Client model)
        {
            model.StoreId = storeId;
            if (model.Id > 0)
                _clientRepository.Edit(model);
            else
                _clientRepository.Add(model);
            return "200";
        }

        [HttpPost]
        public string DeleteClient(Product model)
        {
            _clientRepository.Delete(model.Id);
            return "200";
        }
    }
}