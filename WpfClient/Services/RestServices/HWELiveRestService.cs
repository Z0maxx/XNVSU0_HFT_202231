using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.RestServices
{
    class HWELiveRestService : EmployeeLiveRestService<HourlyWageEmployee>
    {
        public HWELiveRestService() : base("http://localhost:14784/api/", "hourlywageemployee", "hub")
        {
        }
    }
}