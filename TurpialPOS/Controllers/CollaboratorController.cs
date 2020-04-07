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
    public class CollaboratorController : Controller
    {
        private CollabortatorRepository _collabortatorRepository;
        private readonly int storeId = 1;

        public CollaboratorController()
        {
            _collabortatorRepository = new CollabortatorRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        public string GetData()
        {
            var providers = _collabortatorRepository.GetAll(storeId).Select(p => new
            {
                p.Id,
                p.LegalId,
                p.Name,
                p.Address,
                p.Phone,
                p.WorkArea,
            });
            return new JavaScriptSerializer().Serialize(providers);
        }

        [HttpGet]
        public string Get(int id)
        {
            var collaborator = _collabortatorRepository.Get(id);
            var viewModel = new
            {
                collaborator.Id,
                collaborator.Name,
                collaborator.LegalId,
                collaborator.Address,
                collaborator.Phone,
                collaborator.WorkArea
            };
            return new JavaScriptSerializer().Serialize(viewModel);
        }

        [HttpPost]
        public ActionResult Add(Collaborator model)
        {
            var validatorContext = new ValidationContext(model, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, validatorContext, results, true);
            if (isValid)
            {
                model.StoreId = storeId;
                var possiblePreviousClient = _collabortatorRepository.GetByLegalId(model.LegalId);
                if (model.Id > 0)
                {
                    if (possiblePreviousClient != null && possiblePreviousClient.Id != model.Id)
                        return Json(new { success = false, responseText = "Ya existe un colaborador con la cédula: " + model.LegalId + "." }, JsonRequestBehavior.AllowGet);
                    else
                        _collabortatorRepository.Edit(model);
                }
                else
                {
                    if (possiblePreviousClient == null)
                        _collabortatorRepository.Add(model);
                    else
                    {
                        return Json(new { success = false, responseText = "Ya existe un colaborador con la cédula:  " + model.LegalId + "." }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { success = true, responseText = "El colaborador se agregó con éxito." }, JsonRequestBehavior.AllowGet);
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
            _collabortatorRepository.Delete(model.Id);
            return "200";
        }
    }
}