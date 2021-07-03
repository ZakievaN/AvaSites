using AvailabilitySites.Data;
using AvailabilitySites.Models;
using AvailabilitySites.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AvailabilitySites.Services
{
    public class SitesDataModel : ISitesDataModel
    {
        private readonly List<Site> _sites = new List<Site>();

        private int _maxId;

        public SitesDataModel()
        {
            InitializeData();
            _maxId = _sites.DefaultIfEmpty().Max(e => e?.PrimaryKey ?? 1);
        }

        /// <summary>
        /// Событие срабатывает, когда изменяется список сайтов по количеству или если обновляется информация по сайту из списка
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        private void InitializeData()
        {
            var list = BeginData.GetSites();
            foreach (var item in list)
            {
                item.IsAvailable = AvailabilitySite.Check(item);
                _sites.Add(item);
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int Add(Site site)
        {
            if (site is null) throw new ArgumentNullException(nameof(site));

            site.PrimaryKey = ++_maxId;
            _sites.Add(site);

            NotifyPropertyChanged();

            return site.PrimaryKey;
        }

        public bool Delete(int id)
        {
            var site = Get(id);

            if (site is null) return false;

            bool flag = _sites.Remove(site);

            NotifyPropertyChanged();

            return flag;
        }

        public IEnumerable<Site> Get()
        {
            return _sites;
        }

        public Site Get(int id)
        {
            return _sites.FirstOrDefault(e => e.PrimaryKey == id);
        }

        public void Update(Site site)
        {
            if (site is null) throw new ArgumentNullException(nameof(site));

            var db_item = Get(site.PrimaryKey);

            if (db_item is null) return;

            db_item.Name = site.Name;
            db_item.Url = site.Url;
            db_item.Interval = site.Interval;

            NotifyPropertyChanged();
        }
    }
}
