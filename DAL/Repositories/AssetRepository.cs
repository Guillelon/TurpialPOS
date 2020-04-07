using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AssetRepository
    {
        private TurpialPOSDbContext _context;

        public AssetRepository()
        {
            _context = new TurpialPOSDbContext();
        }

        public IList<Asset> GetAll(int storeId)
        {
            return _context.Asset.Where(c => c.IsActive && c.StoreId == storeId).ToList();
        }

        public Asset Get(int id)
        {
            return _context.Asset.Where(c => c.Id == id).FirstOrDefault();
        }

        public void Delete(int id)
        {
            var provider = _context.Asset.Where(p => p.Id == id).FirstOrDefault();
            if (provider != null)
            {
                provider.Deactivate();
                _context.Entry(provider).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public Asset Add(Asset asset)
        {
            _context.Asset.Add(asset);
            _context.SaveChanges();
            return asset;
        }

        public Asset Edit(Asset assetEdited)
        {
            var asset = _context.Asset.Where(p => p.Id == assetEdited.Id).FirstOrDefault();
            if (asset != null)
            {
                asset.Description = assetEdited.Description;
                asset.Office = assetEdited.Office;
                asset.Area = assetEdited.Area;
                _context.Entry(asset).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
            return asset;
        }
    }
}
