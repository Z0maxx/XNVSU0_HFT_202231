using System;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.RestServices
{
    class EmployeeLiveRestService<T> : LiveRestService<T>, IEmployeeLiveRestService<T> where T : Employee
    {
        public RestCollection<T>? AvailableEmployees { get; private set; }
        public EmployeeLiveRestService(string baseurl, string endpoint, string hub) : base(baseurl, endpoint, hub)
        {
        }
        public void SetupAvailable(DateTime orderDate, int jobId)
        {
            AvailableEmployees = new(baseurl, $"{endpoint}/available/{orderDate}/{jobId}");
        }
    }
}
