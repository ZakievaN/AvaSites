using System;
using System.Collections.Generic;
using System.Linq;
using AvailabilitySites.Data;
using AvailabilitySites.Models.Interfaces;

namespace AvailabilitySites.Models
{
    public class SitesDataModel : ISitesDataModel
    {
        private readonly AvailabilitySitesDbContext _dbContext;

        public SitesDataModel(AvailabilitySitesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Site site)
        {
            if (site is null)
            {
                throw new ArgumentNullException(nameof(site));
            }

            _dbContext.Sites.Add(site);
            _dbContext.SaveChanges();

            return site.Id;
        }

        public bool Delete(int id)
        {
            var site = Get(id);

            if (site is null)
            {
                return false;
            }

            _dbContext.Sites.Remove(site);
            _dbContext.SaveChanges();

            return true;
        }

        public IEnumerable<Site> Get()
        {
            return _dbContext.Sites.ToList();
        }

        public Site Get(int id)
        {
            return _dbContext.Sites.FirstOrDefault(e => e.Id == id);
        }

        public void Update(Site site)
        {
            if (site is null)
            {
                throw new ArgumentNullException(nameof(site));
            }

            var dbItem = Get(site.Id);

            if (dbItem is null)
            {
                return;
            }

            dbItem.Name = site.Name;
            dbItem.Url = site.Url;
            dbItem.Interval = site.Interval;

            _dbContext.SaveChanges();
        }
    }
}
