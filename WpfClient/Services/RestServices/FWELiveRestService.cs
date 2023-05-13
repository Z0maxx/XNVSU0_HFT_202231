using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.RestServices
{
    class FWELiveRestService : EmployeeLiveRestService<FixedWageEmployee>
    {
        public FWELiveRestService() : base("http://localhost:14784/api/", "fixedwageemployee", "hub")
        {
        }
    }
}
