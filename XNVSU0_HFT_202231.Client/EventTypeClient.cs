using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Client
{
    class EventTypeClient : Client<EventType>
    {
        public EventTypeClient(RestService rest, string[] args)
            :base(rest, args, new string[] { "Id", "Name" })
        {
        }
    }
}