using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.RestServices
{
    class HWOLiveRestService : OrderLiveRestService<HourlyWageOrder, HourlyWageEmployee>
    {
        public HWOLiveRestService() : base("http://localhost:14784/api/", "hourlywageorder", "hub")
        {
        }
    }
}
