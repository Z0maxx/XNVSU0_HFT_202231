using WpfClient.Logics.GenericLogics;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.TableModelLogics.FixedWageEmployeeLogics
{
    class FWEListLogic : ListWithInnerListLogic<FixedWageEmployee>
    {
        public FWEListLogic(ILiveRestService<FixedWageEmployee> liveRestService, IUpdateService<FixedWageEmployee> updateService, IInnerListService<FixedWageEmployee> innerListService, ICreateService<FixedWageEmployee> createService) :
            base(liveRestService, updateService, innerListService, createService)
        {
        }
    }
}
