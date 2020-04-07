using DAL.DTO;
using DAL.Models;
using DAL.Repositories;
using Rotativa.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace TurpialPOS.Controllers
{
    public class InventoryController : Controller
    {
        private ProductRepository _productRepository;
        private ProviderRepository _providerRepository;
        private readonly int storeId = 1;

        public InventoryController()
        {
            _productRepository = new ProductRepository();
            _providerRepository = new ProviderRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string GetProducts()
        {
            var products = _productRepository.GetAll(storeId).Select(p => new
            {
                p.Id,
                p.Name,
                p.Code,
                p.OrderNumber,
                p.CatalogNumber,
                p.SanitaryCode,
                p.Brand,
                p.Description,
                p.Stock,
                ProviderName = p.Provider.Name,
                ProviderCountry = p.Provider.Country,
                Checked = false
            });
            return new JavaScriptSerializer().Serialize(products);
        }
        
        [HttpGet]
        public string GetProduct(int id)
        {
            var product = _productRepository.Get(id);
            var viewModel = new
            {
                product.Name,
                product.Code,
                product.OrderNumber,
                product.CatalogNumber,
                product.SanitaryCode,
                product.Brand,
                product.Description,
                product.Stock,
                product.ProviderId
            };
            return new JavaScriptSerializer().Serialize(viewModel);
        }

        [HttpGet]
        public string GetProductTypes()
        {
            var productTypes = _productRepository.GetTypes(storeId).Select(p => new { p.Id, p.Name });
            return new JavaScriptSerializer().Serialize(productTypes);
        }

        [HttpPost]
        public ActionResult AddProduct(Product model)
        {
            var validatorContext = new ValidationContext(model, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, validatorContext, results, true);
            if (isValid)
            {
                model.StoreId = storeId;
                //Check if code Exists
                var possiblePreviousProduct = _productRepository.GetByCode(model.Code);
                if (model.Id > 0)
                {
                    if (possiblePreviousProduct != null && possiblePreviousProduct.Id != model.Id)
                        return Json(new { success = false, responseText = "Ya existe en el inventario un producto con el código " + model.Code + "." }, JsonRequestBehavior.AllowGet);
                    else
                        _productRepository.Edit(model);
                }
                else
                {
                    if (possiblePreviousProduct == null)
                        _productRepository.Add(model);
                    else
                    {
                        return Json(new { success = false, responseText = "Ya existe en el inventario un producto con el código " + model.Code + "." }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { success = true, responseText = "El producto se agregó con éxito en el inventario." }, JsonRequestBehavior.AllowGet);
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
        public string DeleteProduct(Product model)
        {
            _productRepository.Delete(model.Id);
            return "200";
        }

        [HttpPost]
        public ActionResult SetProductPrint(PrintProductDTO data)
        {
            var now = DateTime.Now;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            data.Date = now.ToLongDateString();
            data.Products = _productRepository.GetByIdList(data.Ids);
            TempData["ProductsToPrint"] = data;
            return null;
        }

        [HttpGet]
        public ActionResult ProductPrint()
        {
            var data = (PrintProductDTO)TempData["ProductsToPrint"];
            TempData["ProductsToPrint"] = null;
            string cusomtSwitches = string.Format("--print-media-type --allow {0} --footer-html {0} --header-html {1}", Url.Action("ProductPrintFooter", "Inventory", new { area = "" }, "https"), Url.Action("ProductPrintHeader", "Inventory", new { area = "" }, "https"));
            return new Rotativa.ViewAsPdf("ProductPrint", data)
            {
                FileName = "PDF_Output.pdf",
                CustomSwitches = cusomtSwitches
            };
        }

        [AllowAnonymous]
        public ActionResult ProductPrintFooter()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ProductPrintHeader()
        {
            return View();
        }
    }
}