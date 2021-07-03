using AvailabilitySites.Models;
using System.Collections.Generic;

namespace AvailabilitySites.Data
{
    public static class BeginData
    {
        public static List<Site> GetSites()
        {
            List<Site> sites = new List<Site>
            {
                new Site(1, "yandex", "http://yandex.ru", 60),
                new Site(2, "vk", "http://vk.ru", 10),
                new Site(3, "wqwq", "http://wqwq.ru", 15),
                new Site(4, "dffgdg", "http://dffgdg.ru", 20),
                new Site(5, "google", "http://google.com", 25)
            };

            return sites;
        }
    }
}
