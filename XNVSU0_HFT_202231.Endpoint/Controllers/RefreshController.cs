using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using XNVSU0_HFT_202231.Endpoint.Services;
using XNVSU0_HFT_202231.Logic;
using XNVSU0_HFT_202231.Models.TableModels;

namespace XNVSU0_HFT_202231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RefreshController : Controller
    {
        readonly IJobLogic jobLogic;
        readonly IEventTypeLogic eventTypeLogic;
        readonly IFixedWageEmployeeLogic fixedWageEmployeeLogic;
        readonly IHourlyWageEmployeeLogic hourlyWageEmployeeLogic;
        readonly IFixedWageOrderLogic fixedWageOrderLogic;
        readonly IHourlyWageOrderLogic hourlyWageOrderLogic;
        readonly IHubContext<SignalRHub> hub;
        public RefreshController(
            IJobLogic jobLogic,
            IEventTypeLogic eventTypeLogic,
            IFixedWageEmployeeLogic fixedWageEmployeeLogic,
            IHourlyWageEmployeeLogic hourlyWageEmployeeLogic,
            IFixedWageOrderLogic fixedWageOrderLogic,
            IHourlyWageOrderLogic hourlyWageOrderLogic,
            IHubContext<SignalRHub> hub
        )
        {
            this.jobLogic = jobLogic;
            this.eventTypeLogic = eventTypeLogic;
            this.fixedWageEmployeeLogic = fixedWageEmployeeLogic;
            this.hourlyWageEmployeeLogic = hourlyWageEmployeeLogic;
            this.fixedWageOrderLogic = fixedWageOrderLogic;
            this.hourlyWageOrderLogic = hourlyWageOrderLogic;
            this.hub = hub;
        }
        [HttpPost("job")]
        public void RefreshJob([FromBody] int? id)
        {
            if (id == null) return;
            hub.Clients.All.SendAsync("JobUpdated", jobLogic.Read((int)id));
        }
        [HttpPost("eventtype")]
        public void RefreshEventType([FromBody] int? id)
        {
            if ( id == null) return;
            hub.Clients.All.SendAsync("EventTypeUpdated", eventTypeLogic.Read((int)id));
        }
        [HttpPost("fixedwageemployee")]
        public void RefreshFixedWageEmployee([FromBody] int? id)
        {
            if (id == null) return;
            hub.Clients.All.SendAsync("FixedWageEmployeeUpdated", fixedWageEmployeeLogic.Read((int)id));
        }
        [HttpPost("fixedwageemployee/bulk")]
        public void RefreshFixedWageEmployees([FromBody] IEnumerable<int?> ids)
        {
            IEnumerable<int> notNullIds = ids.Where(id => id.HasValue).Select(id => (int)id);
            if (notNullIds.Any())
            {
                hub.Clients.All.SendAsync("FixedWageEmployeesUpdated", fixedWageEmployeeLogic.ReadBulk(notNullIds));
            }
        }
        [HttpPost("hourlywageemployee")]
        public void RefreshHourlyWageEmployee([FromBody] int? id)
        {
            if (id == null) return;
            hub.Clients.All.SendAsync("HourlyWageEmployeeUpdated", hourlyWageEmployeeLogic.Read((int)id));
        }
        [HttpPost("hourlywageemployee/bulk")]
        public void RefreshHourlyWageEmployees([FromBody] IEnumerable<int?> ids)
        {
            IEnumerable<int> notNullIds = ids.Where(id => id.HasValue).Select(id => (int)id);
            if (notNullIds.Any())
            {
                hub.Clients.All.SendAsync("HourlyWageEmployeesUpdated", hourlyWageEmployeeLogic.ReadBulk(notNullIds));
            }
        }
        [HttpPost("fixedwageorder/bulk")]
        public void RefreshFixedWageOrders([FromBody] IEnumerable<int?> ids)
        {
            IEnumerable<int> notNullIds = ids.Where(id => id.HasValue).Select(id => (int)id);
            if (notNullIds.Any())
            {
                hub.Clients.All.SendAsync("FixedWageOrdersUpdated", fixedWageOrderLogic.ReadBulk(notNullIds));
            }
        }
        [HttpPost("fixedwageordergroup")]
        public void RefreshFixedWageOrderGroup([FromBody] IEnumerable<int?> ids, int? oldEmployeeId)
        {
            hub.Clients.All.SendAsync("FixedWageOrderGroupUpdated", ids, oldEmployeeId);
        }
        [HttpPost("hourlywageorder/bulk")]
        public void RefreshHourlyWageOrders([FromBody] IEnumerable<int?> ids)
        {
            IEnumerable<int> notNullIds = ids.Where(id => id.HasValue).Select(id => (int)id);
            if (notNullIds.Any())
            {
                hub.Clients.All.SendAsync("HourlyWageOrdersUpdated", hourlyWageOrderLogic.ReadBulk(notNullIds));
            }
        }
        [HttpPost("hourlywageordergroup")]
        public void RefreshHourlyWageOrderGroup([FromBody] IEnumerable<int?> ids, int? oldEmployeeId)
        {   
            hub.Clients.All.SendAsync("HourlyWageOrderGroupUpdated", ids, oldEmployeeId);
        }
    }
}
