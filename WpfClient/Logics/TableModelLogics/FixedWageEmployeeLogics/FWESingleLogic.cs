using WpfClient.Logics.GenericLogics;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.TableModelLogics.FixedWageEmployeeLogics
{
    class FWESingleLogic : SingleWithInnerListLogic<FixedWageEmployee>
    {
        public FWESingleLogic(ILiveRestService<FixedWageEmployee> liveRestService, IUpdateService<FixedWageEmployee> updateService, IInnerListService<FixedWageEmployee> innerListService) :
            base(liveRestService, updateService, innerListService)
        {
        }
    }
}
