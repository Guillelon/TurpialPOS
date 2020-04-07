﻿using DAL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductRepository
    {
        private TurpialPOSDbContext _context;

        public ProductRepository()
        {
            _context = new TurpialPOSDbContext();
        }

        public Product Add(Product product)
        {
            _context.Product.Add(product);
            _context.SaveChanges();
            return product;
        }
                
        public Product Edit(Product productEdited)
        {
            var product = _context.Product.Where(p => p.Id == productEdited.Id).FirstOrDefault();
            if(product != null){
                product.Name = productEdited.Name;
                product.Code = productEdited.Code;
                product.OrderNumber = productEdited.OrderNumber;
                product.CatalogNumber = productEdited.CatalogNumber;
                product.SanitaryCode = productEdited.SanitaryCode;
                product.Brand = productEdited.Brand;
                product.Description = productEdited.Description;
                product.Stock = productEdited.Stock;
                product.ProviderId = productEdited.ProviderId;
                _context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();                
            }
            return product;
        }

        public List<Product> GetByIdList(IList<int> ids)
        {
            return _context.Product.Where(p => ids.Contains(p.Id)).ToList();
        }

        public void Delete(int id)
        {
            var product = _context.Product.Where(p => p.Id == id).FirstOrDefault();
            if(product != null)
            {
                product.Deactivate();
                _context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public IList<Product> GetAll(int storeId)
        {
            return _context.Product.Where(p => p.StoreId == storeId
                                            && p.IsActive).ToList();
        }

        public IList<ProductType> GetTypes(int storeId)
        {
            return _context.ProductType.Where(p=> p.StoreId == storeId).ToList();
        }

        public Product Get(int id)
        {
            return _context.Product.Where(p => p.Id == id).FirstOrDefault();
        }

        public Product GetByCode(string code)
        {
            return _context.Product.Where(p => p.Code == code).FirstOrDefault();
        }
    }
}
