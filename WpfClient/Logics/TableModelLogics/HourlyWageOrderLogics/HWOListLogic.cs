using WpfClient.Logics.GenericLogics;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.TableModelLogics.HourlyWageOrderLogics
{
    class HWOListLogic : ListWithSingleLogic<HourlyWageOrder, HourlyWageEmployee>
    {
        public HWOListLogic(ILiveRestService<HourlyWageOrder> liveRestService, IUpdateService<HourlyWageOrder> updateService, ICreateService<HourlyWageOrder> createService, ISingleService<HourlyWageEmployee> singleService) :
            base(liveRestService, updateService, createService, singleService)
        {
        }
    }
}
