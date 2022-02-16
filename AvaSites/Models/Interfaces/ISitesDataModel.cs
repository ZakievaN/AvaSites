using System.Collections.Generic;
using AvailabilitySites.Data;

namespace AvailabilitySites.Models.Interfaces
{
    public interface ISitesDataModel
    {
        IEnumerable<Site> Get();

        Site Get(int id);

        int Add(Site employee);

        void Update(Site employee);

        bool Delete(int id);
       
    }
}
