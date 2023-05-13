using WpfClient.Logics.GenericLogics;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.TableModelLogics.FixedWageOrderLogics
{
    class FWOSingleLogic : SingleWithSingleLogic<FixedWageOrder, FixedWageEmployee>
    {
        public FWOSingleLogic(ILiveRestService<FixedWageOrder> liveRestService, IUpdateService<FixedWageOrder> updateService, ISingleService<FixedWageEmployee> singleService) :
            base(liveRestService, updateService, singleService)
        {
        }
    }
}
