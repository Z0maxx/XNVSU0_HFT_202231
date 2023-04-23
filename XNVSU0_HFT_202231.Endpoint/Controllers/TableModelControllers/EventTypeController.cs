using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using XNVSU0_HFT_202231.Endpoint.Services;
using XNVSU0_HFT_202231.Logic;
using XNVSU0_HFT_202231.Models.TableModels;
using System;
using System.Linq;

namespace XNVSU0_HFT_202231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventTypeController : ControllerBase
    {
        readonly IEventTypeLogic logic;
        readonly IHubContext<SignalRHub> hub;
        readonly RefreshController refreshController;
        public EventTypeController(IEventTypeLogic logic, IHubContext<SignalRHub> hub, RefreshController refreshController)
        {
            this.logic = logic;
            this.hub = hub;
            this.refreshController = refreshController;
        }

        [HttpGet]
        public IEnumerable<EventType> ReadAll()
        {
            return logic.ReadAll();
        }
        [HttpGet("{id}")]
        public EventType Read(int id)
        {
            return logic.Read(id);
        }
        [HttpPost]
        public void Create([FromBody] EventType value)
        {
            logic.Create(value);
            hub.Clients.All.SendAsync("EventTypeCreated", value.Id);
        }
        [HttpPut]
        public void Update([FromBody] EventType value)
        {
            EventType updated = logic.Read((int)value.Id);
            IEnumerable<int?> orderIds = updated.Orders.Select(o => o.Id); 

            logic.Update(value);
            hub.Clients.All.SendAsync("EventTypeUpdated", logic.Read((int)value.Id));
            refreshController.RefreshFixedWageOrders(orderIds);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            EventType deleted = logic.Read((int)id);
            IEnumerable<int?> orderIds = deleted.Orders.Select(o => o.Id);

            logic.Delete(id);
            hub.Clients.All.SendAsync("EventTypeDeleted", deleted);
            refreshController.RefreshFixedWageOrders(orderIds);
        }
    }
}