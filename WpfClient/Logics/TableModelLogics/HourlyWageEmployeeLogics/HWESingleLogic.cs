using WpfClient.Logics.GenericLogics;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.TableModelLogics.HourlyWageEmployeeLogics
{
    class HWESingleLogic : SingleWithInnerListLogic<HourlyWageEmployee>
    {
        public HWESingleLogic(ILiveRestService<HourlyWageEmployee> liveRestService, IUpdateService<HourlyWageEmployee> updateService, IInnerListService<HourlyWageEmployee> innerListService) :
            base(liveRestService, updateService, innerListService)
        {
        }
    }
}
