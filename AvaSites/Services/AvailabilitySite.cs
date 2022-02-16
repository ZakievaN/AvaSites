using System;
using System.Net;
using AvailabilitySites.Data;

namespace AvailabilitySites.Services
{
    public static class AvailabilitySite
    {
        public static bool Check(Site site)
        {
            bool flag = true;
            Uri uri = new Uri(site.Url);
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch
            {
                flag = false;
            }
            return flag;
        }
    }
}
