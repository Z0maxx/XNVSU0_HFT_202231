using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using XNVSU0_HFT_202231.Endpoint.Services;
using XNVSU0_HFT_202231.Logic;
using XNVSU0_HFT_202231.Models.TableModels;

namespace XNVSU0_HFT_202231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        readonly IJobLogic logic;
        readonly IHubContext<SignalRHub> hub;
        readonly RefreshController refreshController;
        public JobController(IJobLogic logic, IHubContext<SignalRHub> hub, RefreshController refreshController)
        {
            this.logic = logic;
            this.hub = hub;
            this.refreshController = refreshController;
        }

        [HttpGet]
        public IEnumerable<Job> ReadAll()
        {
            return logic.ReadAll();
        }
        [HttpGet("{id}")]
        public Job Read(int id)
        {
            return logic.Read(id);
        }
        [HttpPost]
        public void Create([FromBody] Job value)
        {
            logic.Create(value);
            hub.Clients.All.SendAsync("JobCreated", value.Id);
        }
        [HttpPut]
        public void Update([FromBody] Job value)
        {
            Job updated = logic.Read((int)value.Id);
            IEnumerable<int?> fixedWageEmployeeIds = updated.FixedWageEmployees.Select(e => e.Id);
            IEnumerable<int?> hourlyWageEmployeeIds = updated.HourlyWageEmployees.Select(e => e.Id);

            logic.Update(value);
            hub.Clients.All.SendAsync("JobUpdated", logic.Read((int)value.Id));

            refreshController.RefreshFixedWageEmployees(fixedWageEmployeeIds);
            refreshController.RefreshHourlyWageEmployees(hourlyWageEmployeeIds);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Job deleted = logic.Read(id);
            IEnumerable<int?> fixedWageEmployeeIds = deleted.FixedWageEmployees.Select(e => e.Id);
            IEnumerable<int?> hourlyWageEmployeeIds = deleted.HourlyWageEmployees.Select(e => e.Id);

            logic.Delete(id);
            hub.Clients.All.SendAsync("JobDeleted", deleted);

            refreshController.RefreshFixedWageEmployees(fixedWageEmployeeIds);
            refreshController.RefreshHourlyWageEmployees(hourlyWageEmployeeIds);
        }
    }
}