using WpfClient.Logics.TableModelLogics.EmployeeLogics;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.TableModelLogics.FixedWageEmployeeLogics
{
    class FWEOrdersLogic : EmployeeOrdersLogic<FixedWageEmployee, FixedWageOrder>
    {
        public FWEOrdersLogic(ILiveRestService<FixedWageEmployee> liveRestService, ISingleService<FixedWageOrder> singleService) :base(liveRestService, singleService)
        {
        }
    }
}
