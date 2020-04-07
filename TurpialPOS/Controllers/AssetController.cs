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
    public class AssetController : Controller
    {
        private AssetRepository _assetRepository;
        private readonly int storeId = 1;

        public AssetController()
        {
            _assetRepository = new AssetRepository();
        }
        public ActionResult Index()
        {
            return View();
        }

        public string GetData()
        {
            var providers = _assetRepository.GetAll(storeId).Select(p => new
            {
                p.Id,
                p.Description,
                p.Office,
                p.Area
            });
            return new JavaScriptSerializer().Serialize(providers);
        }

        [HttpGet]
        public string Get(int id)
        {
            var asset = _assetRepository.Get(id);
            var viewModel = new
            {
                asset.Id,
                asset.Description,
                asset.Office,
                asset.Area
            };
            return new JavaScriptSerializer().Serialize(viewModel);
        }

        [HttpPost]
        public ActionResult Add(Asset model)
        {
            var validatorContext = new ValidationContext(model, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, validatorContext, results, true);
            if (isValid)
            {
                model.StoreId = storeId;
                if (model.Id > 0)
                {
                    _assetRepository.Edit(model);
                }
                else
                {
                    _assetRepository.Add(model);
                }
                return Json(new { success = true, responseText = "El activo empresarial se agregó con éxito." }, JsonRequestBehavior.AllowGet);
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
        public string Delete(Asset model)
        {
            _assetRepository.Delete(model.Id);
            return "200";
        }
    }
}