using AvailabilitySites.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace AvailabilitySites.Services.Interfaces
{
    public interface ISitesDataModel: INotifyPropertyChanged
    {
        IEnumerable<Site> Get();

        Site Get(int id);

        int Add(Site employee);

        void Update(Site employee);

        bool Delete(int id);
       
    }
}
