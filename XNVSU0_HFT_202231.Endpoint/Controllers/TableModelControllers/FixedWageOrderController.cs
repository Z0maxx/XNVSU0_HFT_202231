using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
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
    public class FixedWageOrderController : ControllerBase
    {
        readonly IFixedWageOrderLogic logic;
        readonly IHubContext<SignalRHub> hub;
        readonly RefreshController refreshController;
        
        public FixedWageOrderController(IFixedWageOrderLogic logic, IHubContext<SignalRHub> hub, RefreshController refreshController)
        {
            this.logic = logic;
            this.hub = hub;
            this.refreshController = refreshController;
        }

        [HttpGet]
        public IEnumerable<FixedWageOrder> ReadAll()
        {
            return logic.ReadAll();
        }
        [HttpGet("{id}")]
        public FixedWageOrder Read(int id)
        {
            return logic.Read(id);
        }
        [HttpPost("getbulk")]
        public IEnumerable<FixedWageOrder> ReadBulk([FromBody] IEnumerable<int> ids)
        {
            return logic.ReadBulk(ids);
        }
        [HttpPost("allforcustomer")]
        public IEnumerable<FixedWageOrder> GetAllForCustomer([FromBody] Order order)
        {
            return logic.GetAllForCustomer(order.OrderDate, order.FirstName, order.LastName, order.EmailAddress);
        }
        [HttpPost("availableemployeesfororder")]
        public IEnumerable<FixedWageEmployee> GetAvailableEmployeesForOrder([FromBody] Order order)
        {
            return logic.GetAvailableEmployeesForOrder(order);
        }
        [HttpPost]
        public void Create([FromBody] FixedWageOrder value)
        {
            logic.Create(value);
            hub.Clients.All.SendAsync("FixedWageOrderCreated", value.Id);
            refreshController.RefreshEventType((int)value.EventTypeId);
            refreshController.RefreshFixedWageEmployee((int)value.EmployeeId);
        }
        [HttpPost("bulk")]
        public void CreateBulk([FromBody] FixedWageOrder[] values)
        {
            logic.CreateBulk(values);
            hub.Clients.All.SendAsync("FixedWageOrdersCreated", values.Select(value => value.Id));
            refreshController.RefreshEventType((int)values[0].EventTypeId);
            refreshController.RefreshFixedWageEmployees(values.Select(o => o.EmployeeId));
        }
        [HttpPut]
        public void Update([FromBody] FixedWageOrder value)
        {
            FixedWageOrder updated = logic.Read((int)value.Id);
            int? oldEventTypeId = updated.EventTypeId;
            int? oldEmployeeId = updated.EmployeeId;
            int? newEmployeeId = value.EmployeeId;

            logic.Update(value);
            hub.Clients.All.SendAsync("FixedWageOrderUpdated", logic.Read((int)value?.Id));

            refreshController.RefreshEventType(value.EventTypeId);
            if (value.EventTypeId != oldEventTypeId)
            {
                refreshController.RefreshEventType(oldEventTypeId);
            }

            refreshController.RefreshFixedWageEmployee(newEmployeeId);
            if (newEmployeeId != oldEmployeeId)
            {
                refreshController.RefreshFixedWageEmployee(oldEmployeeId);

                IEnumerable<int?> ids = logic.GetAllForCustomer(value.OrderDate, value.FirstName, value.LastName, value.EmailAddress).Where(o => o.Id != value.Id).Select(o => o.Id);
                refreshController.RefreshFixedWageOrderGroup(ids, oldEmployeeId);
            }
        }
        [HttpPut("bulk")]
        public void UpdateBulk([FromBody] FixedWageOrder[] values)
        {
            List<int> orderIds = values.Select(o => (int)o.Id).ToList();

            IEnumerable<int?> oldEmployeeIds = logic.ReadBulk(orderIds).Select(o => o.EmployeeId);
            IEnumerable<int?> newEmployeeIds = values.Select(o => o.EmployeeId);
            int? diffEmployeeId = oldEmployeeIds.Except(newEmployeeIds).FirstOrDefault();

            int? oldEventTypeId = logic.Read(orderIds[0]).EventTypeId;
            int newEventTypeId = (int)values.ToList()[0].EventTypeId;

            logic.UpdateBulk(values);
            hub.Clients.All.SendAsync("FixedWageOrdersUpdated", logic.ReadBulk(values.Select(value => (int)value.Id)));

            refreshController.RefreshEventType(newEventTypeId);
            if (newEventTypeId != oldEventTypeId)
            {
                refreshController.RefreshEventType(oldEventTypeId);
            }

            refreshController.RefreshFixedWageEmployees(newEmployeeIds);
            if (diffEmployeeId != 0)
            {
                refreshController.RefreshFixedWageEmployee(diffEmployeeId);
            }
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            FixedWageOrder deleted = logic.Read(id);
            int? eventTypeId = deleted.EventTypeId;
            int? employeeId = deleted.EmployeeId;
            hub.Clients.All.SendAsync("FixedWageOrderDeleted", deleted);
            logic.Delete(id);

            refreshController.RefreshEventType(eventTypeId);
            refreshController.RefreshFixedWageEmployee(employeeId);
        }
    }
}