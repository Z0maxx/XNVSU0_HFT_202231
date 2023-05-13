using System.Collections.Generic;
using WpfClient.Logics.GenericLogics;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.TableModelLogics.EmployeeLogics
{
    abstract class EmployeeOrdersLogic<T, S> : InnerListLogic<T, S> where T : Employee where S : Order
    {
        public EmployeeOrdersLogic(ILiveRestService<T> liveRestService, ISingleService<S> singleService) :
            base(liveRestService, singleService, new string[] { "Orders" })
        {
        }
    }
}
