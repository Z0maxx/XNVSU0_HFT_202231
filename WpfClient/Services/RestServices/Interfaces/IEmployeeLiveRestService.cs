using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.RestServices.Interfaces
{
    interface IEmployeeLiveRestService<T> : ILiveRestService<T> where T : Employee
    {
        RestCollection<T>? AvailableEmployees { get; }
        void SetupAvailable(DateTime orderDate, int jobId);
    }
}
