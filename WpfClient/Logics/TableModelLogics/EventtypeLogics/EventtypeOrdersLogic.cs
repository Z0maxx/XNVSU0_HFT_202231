using System.Collections.Generic;
using WpfClient.Logics.GenericLogics;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.TableModelLogics.EventtypeLogics
{
    class EventtypeOrdersLogic : InnerListLogic<EventType, FixedWageOrder>
    {
        public EventtypeOrdersLogic(ILiveRestService<EventType> liveRestService, ISingleService<FixedWageOrder> singleService) :
            base(liveRestService, singleService, new string[] { "Orders" })
        {
        }
    }
}
