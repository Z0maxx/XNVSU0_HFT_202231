using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.RestServices
{
    class EventTypeLiveRestService : LiveRestService<EventType>
    {
        public EventTypeLiveRestService() : base("http://localhost:14784/api/", "eventtype", "hub")
        {
        }
    }
}
