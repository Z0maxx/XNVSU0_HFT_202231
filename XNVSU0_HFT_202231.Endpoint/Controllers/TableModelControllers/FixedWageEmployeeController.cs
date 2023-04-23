using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using XNVSU0_HFT_202231.Endpoint.Services;
using XNVSU0_HFT_202231.Logic;
using XNVSU0_HFT_202231.Models.TableModels;

namespace XNVSU0_HFT_202231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FixedWageEmployeeController : ControllerBase
    {
        readonly IFixedWageEmployeeLogic logic;
        readonly IHubContext<SignalRHub> hub;
        readonly RefreshController refreshController;
        public FixedWageEmployeeController(IFixedWageEmployeeLogic logic, IHubContext<SignalRHub> hub, RefreshController refreshController)
        {
            this.logic = logic;
            this.hub = hub;
            this.refreshController = refreshController;
        }

        [HttpGet]
        public IEnumerable<FixedWageEmployee> ReadAll()
        {
            return logic.ReadAll();
        }
        [HttpGet("{id}")]
        public FixedWageEmployee Read(int id)
        {
            return logic.Read(id);
        }
        [HttpGet("available/{date}/{jobId}")]
        public IEnumerable<FixedWageEmployee> GetAvailable(DateTime date, int jobId)
        {
            return logic.GetAvailable(date, jobId);
        }
        [HttpPost]
        public void Create([FromBody] FixedWageEmployee value)
        {
            logic.Create(value);
            hub.Clients.All.SendAsync("FixedWageEmployeeCreated", value.Id);
            refreshController.RefreshJob(value.JobId);
        }
        [HttpPut]
        public void Update([FromBody] FixedWageEmployee value)
        {
            FixedWageEmployee updated = logic.Read((int)value.Id);
            int? oldJobId = value.JobId;
            IEnumerable<int?> orderIds = updated.Orders.Select(o => o.Id);

            logic.Update(value);
            hub.Clients.All.SendAsync("FixedWageEmployeeUpdated", logic.Read((int)value?.Id));

            refreshController.RefreshJob(value.JobId);
            if (oldJobId != value.JobId)
            {
                refreshController.RefreshJob(oldJobId);
            }

            refreshController.RefreshFixedWageOrders(orderIds);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            FixedWageEmployee deleted = logic.Read(id);
            int? jobId = deleted.JobId;
            IEnumerable<int?> orderIds = deleted.Orders.Select(o => o.Id); 

            logic.Delete(id);
            hub.Clients.All.SendAsync("FixedWageEmployeeDeleted", deleted);

            refreshController.RefreshJob(jobId);
            refreshController.RefreshFixedWageOrders(orderIds);
        }
    }
}