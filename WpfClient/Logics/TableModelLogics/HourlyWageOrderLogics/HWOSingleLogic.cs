using WpfClient.Logics.GenericLogics;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.TableModelLogics.HourlyWageOrderLogics
{
    class HWOSingleLogic : SingleWithSingleLogic<HourlyWageOrder, HourlyWageEmployee>
    {
        public HWOSingleLogic(ILiveRestService<HourlyWageOrder> liveRestService, IUpdateService<HourlyWageOrder> updateService, ISingleService<HourlyWageEmployee> singleService) :
            base(liveRestService, updateService, singleService)
        {
        }
    }
}
