using WpfClient.Logics.GenericLogics;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.TableModelLogics.HourlyWageEmployeeLogics
{
    class HWEListLogic : ListWithInnerListLogic<HourlyWageEmployee>
    {
        public HWEListLogic(ILiveRestService<HourlyWageEmployee> liveRestService, IUpdateService<HourlyWageEmployee> updateService, IInnerListService<HourlyWageEmployee> innerListService, ICreateService<HourlyWageEmployee> createService) :
            base(liveRestService, updateService, innerListService, createService)
        {
        }
    }
}
