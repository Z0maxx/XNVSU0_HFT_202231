using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.RestServices
{
    class OrderLiveRestService<T, S> : LiveRestService<T> where T : Order where S : Employee
    {
        public OrderLiveRestService(string baseurl, string endpoint, string hub) : base(baseurl, endpoint, hub)
        {
        }
    }
}
