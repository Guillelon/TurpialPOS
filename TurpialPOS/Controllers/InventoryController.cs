using DAL.Models;
using DAL.Repositories;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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
                p.Name,p.Price,p.Cost,p.Description,p.CodeBar,
                ProductType = p.ProductType.Name, p.Id, p.Tax
            });
            return new JavaScriptSerializer().Serialize(products);
        }

        [HttpGet]
        public string GetProduct(int id)
        {
            var product = _productRepository.Get(id);
            var viewModel = new {
                product.Name,
                product.Price,
                product.Cost,
                product.Description,
                product.CodeBar,
                ProductType = product.ProductType.Id,
                product.Id
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
        public string AddProduct(Product model)
        {
            if (model.Id > 0)
                _productRepository.Edit(model);
            else
                _productRepository.Add(model);
            return "200";
        }

        [HttpPost]
        public string DeleteProduct(Product model)
        {
            _productRepository.Delete(model.Id);
            return "200";
        }
    }
}