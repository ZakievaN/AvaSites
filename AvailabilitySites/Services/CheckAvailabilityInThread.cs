using AvailabilitySites.Models;
using AvailabilitySites.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AvailabilitySites.Services
{
    public class CheckAvailabilityInThread : ICheckAvailabilityInThread
    {
        private ISitesDataModel _sitesData;

        private readonly Dictionary<int, Timer> _timers = new Dictionary<int, Timer>();

        public CheckAvailabilityInThread(ISitesDataModel sitesData)
        {
            _sitesData = sitesData;
        }

        public void Check()
        {
            //Thread thread = new Thread(new ThreadStart(RunCheck));
            //thread.Start();
            RunCheck();
        }

        private void RunCheck()
        {
            var _sites = _sitesData.Get();

            ActualizeThreads(_sites);

            if (_sites.Count() == 0)
            {
                ThreadsOff();
                return;
            }

            foreach (var site in _sites)
            {
                Timer existTimer = FindTimer(site.PrimaryKey);
                if (existTimer != null)
                {
                    existTimer.Change(0, site.Interval * 1000);
                    continue;
                }

                Timer timer = new Timer(delegate (object objectSite)
                    {
                        ((Site)objectSite).IsAvailable = AvailabilitySite.Check(site);
                        Console.WriteLine(site.Name + " " + DateTime.Now);
                    },
                    site, 
                    0, 
                    site.Interval * 1000
                );

                _timers.Add(site.PrimaryKey, timer);

            }
        }

        private Timer FindTimer(int id)
        {
            foreach (var timer in _timers)
            {
                if (timer.Key == id)
                    return timer.Value;
            }
            return null;
        }

        private void ThreadsOff()
        {
            foreach (var thread in _timers)
            {
                thread.Value.Dispose();
            }

            _timers.Clear();
        }

        private void ActualizeThreads(IEnumerable<Site> sites)
        {
            //  Актуализация потоков. Если поток неактуальный (т.е. сайт из списка удален), то вызвать метод ThreadOff
            List<int> ids = new List<int>();
            foreach (var timer in _timers)
            {
                if (!sites.Any(site => site.PrimaryKey == timer.Key))
                {
                    timer.Value.Dispose();
                    ids.Add(timer.Key);
                }
            }

            foreach (var id in ids)
            {
                _timers.Remove(id);
            }
        }
    }
}
