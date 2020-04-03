using DAL.DTO;
using DAL.Models;
using DAL.Repositories;
using Rotativa.Options;
using System;
using System.Collections.Generic;
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
        private readonly int storeId = 1;

        public InventoryController()
        {
            _productRepository = new ProductRepository();
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
                p.Id, p.Name, p.Code, p.OrderNumber, p.CatalogNumber, p.SanitaryCode,
                p.Brand, p.Factory, p.Country, p.Description, p.Stock, Checked = false
            });
            return new JavaScriptSerializer().Serialize(products);
        }

        [HttpGet]
        public string GetProduct(int id)
        {
            var product = _productRepository.Get(id);
            var viewModel = new {
                product.Name,
                product.Code,
                product.OrderNumber,
                product.CatalogNumber,
                product.SanitaryCode,
                product.Brand,
                product.Factory,
                product.Country,
                product.Description, 
                product.Stock
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
        public ActionResult AddProduct(Product data)
        {
            data.StoreId = storeId;
            //Check if code Exists
            var possiblePreviousProduct = _productRepository.GetByCode(data.Code);
            if (data.Id > 0)
            {
                if(possiblePreviousProduct != null && possiblePreviousProduct.Id != data.Id)
                    return Json(new { success = false, responseText = "Ya existe en el inventario un producto con el código " + data.Code + "." }, JsonRequestBehavior.AllowGet);
                else
                    _productRepository.Edit(data);
            }
            else
            {
                if (possiblePreviousProduct == null)
                    _productRepository.Add(data);
                else
                {
                    return Json(new { success = false, responseText = "Ya existe en el inventario un producto con el código " + data.Code + "." }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = true, responseText = "El producto se agregó con éxito en el inventario." }, JsonRequestBehavior.AllowGet);
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
            string cusomtSwitches = string.Format("--print-media-type --allow {0} --footer-html {0}", Url.Action("ProductPrintFooter", "Inventory", new { area = "" }, "https"));
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
    }
}