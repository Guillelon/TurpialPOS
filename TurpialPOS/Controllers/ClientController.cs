﻿using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public ActionResult AddClient(Client model)
        {
            var validatorContext = new ValidationContext(model, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, validatorContext, results, true);
            if (isValid)
            {
                model.StoreId = storeId;
                var possiblePreviousClient = _clientRepository.GetByLegalId(model.LegalId);
                if (model.Id > 0)
                {
                    if (possiblePreviousClient != null && possiblePreviousClient.Id != model.Id)
                        return Json(new { success = false, responseText = "Ya existe un cliente con el RUC: " + model.LegalId + "." }, JsonRequestBehavior.AllowGet);
                    else
                        _clientRepository.Edit(model);
                }
                else
                {
                    if (possiblePreviousClient == null)
                        _clientRepository.Add(model);
                    else
                    {
                        return Json(new { success = false, responseText = "Ya existe un cliente con el RUC:  " + model.LegalId + "." }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { success = true, responseText = "El cliente se agregó con éxito." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = results.FirstOrDefault();
                var message = "Ha ocurrido un problema, estamos trabajando para resolverlo";
                if (result != null)
                    message = result.ErrorMessage;
                return Json(new { success = false, responseText = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public string DeleteClient(Client model)
        {
            _clientRepository.Delete(model.Id);
            return "200";
        }
    }
}