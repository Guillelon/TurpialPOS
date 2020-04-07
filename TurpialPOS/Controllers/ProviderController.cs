using DAL.Models;
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
    public class ProviderController : Controller
    {
        private ProviderRepository _providerRepository;
        private readonly int storeId = 1;

        public ProviderController()
        {
            _providerRepository = new ProviderRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        public string GetData()
        {
            var providers = _providerRepository.GetAll(storeId).Select(p => new
            {
                p.Id,
                p.LegalId,
                p.Name,
                p.Country,
                p.ContactName,
                p.ContactPhone,
                p.ContactEmail
            });
            return new JavaScriptSerializer().Serialize(providers);
        }

        [HttpGet]
        public string Get(int id)
        {
            var client = _providerRepository.Get(id);
            var viewModel = new
            {
                client.Id,
                client.Name,
                client.Country,
                client.ContactName,
                client.ContactPhone,
                client.Notes,
                client.ContactEmail,
                client.LegalId
            };
            return new JavaScriptSerializer().Serialize(viewModel);
        }

        [HttpPost]
        public ActionResult Add(Provider model)
        {
            var validatorContext = new ValidationContext(model, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, validatorContext, results, true);
            if (isValid)
            {
                model.StoreId = storeId;
                var possiblePreviousClient = _providerRepository.GetByLegalId(model.LegalId);
                if (model.Id > 0)
                {
                    if (possiblePreviousClient != null && possiblePreviousClient.Id != model.Id)
                        return Json(new { success = false, responseText = "Ya existe un proveedor con el RUC: " + model.LegalId + "." }, JsonRequestBehavior.AllowGet);
                    else
                        _providerRepository.Edit(model);
                }
                else
                {
                    if (possiblePreviousClient == null)
                        _providerRepository.Add(model);
                    else
                    {
                        return Json(new { success = false, responseText = "Ya existe un proveedor con el RUC:  " + model.LegalId + "." }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { success = true, responseText = "El proveedor se agregó con éxito." }, JsonRequestBehavior.AllowGet);
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
        public string Delete(Client model)
        {
            _providerRepository.Delete(model.Id);
            return "200";
        }
    }
}