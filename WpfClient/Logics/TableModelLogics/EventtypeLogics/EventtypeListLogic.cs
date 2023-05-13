using WpfClient.Logics.GenericLogics;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.TableModelLogics.EventtypeLogics
{
    class EventtypeListLogic : ListWithInnerListLogic<EventType>
    {
        public EventtypeListLogic(ILiveRestService<EventType> liveRestService, IUpdateService<EventType> updateService, IInnerListService<EventType> innerListService, ICreateService<EventType> createService) :
            base(liveRestService, updateService, innerListService, createService)
        {
        }
    }
}
