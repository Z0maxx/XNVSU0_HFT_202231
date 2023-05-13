using WpfClient.Logics.GenericLogics;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.TableModelLogics.FixedWageOrderLogics
{
    class FWOListLogic : ListWithSingleLogic<FixedWageOrder, FixedWageEmployee>
    {
        public FWOListLogic(ILiveRestService<FixedWageOrder> liveRestService, IUpdateService<FixedWageOrder> updateService, ICreateService<FixedWageOrder> createService, ISingleService<FixedWageEmployee> singleService) :
            base(liveRestService, updateService, createService, singleService)
        {
        }
    }
}
