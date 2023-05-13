using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.RestServices
{
    class FWOLiveRestService : OrderLiveRestService<FixedWageOrder, FixedWageEmployee>
    {
        public FWOLiveRestService() : base("http://localhost:14784/api/", "fixedwageorder", "hub")
        {
        }
    }
}
