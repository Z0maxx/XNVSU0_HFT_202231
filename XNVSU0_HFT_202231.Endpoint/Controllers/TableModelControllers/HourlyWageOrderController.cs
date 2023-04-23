using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using XNVSU0_HFT_202231.Endpoint.Services;
using XNVSU0_HFT_202231.Logic;
using XNVSU0_HFT_202231.Models.TableModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace XNVSU0_HFT_202231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HourlyWageOrderController : ControllerBase
    {
        readonly IHourlyWageOrderLogic logic;
        readonly IHubContext<SignalRHub> hub;
        readonly RefreshController refreshController;
        public HourlyWageOrderController(IHourlyWageOrderLogic logic, IHubContext<SignalRHub> hub, RefreshController refreshController)
        {
            this.logic = logic;
            this.hub = hub;
            this.refreshController = refreshController;
        }

        [HttpGet]
        public IEnumerable<HourlyWageOrder> ReadAll()
        {
            return logic.ReadAll();
        }
        [HttpGet("{id}")]
        public HourlyWageOrder Read(int id)
        {
            return logic.Read(id);
        }
        [HttpPost("getbulk")]
        public IEnumerable<HourlyWageOrder> ReadBulk([FromBody] IEnumerable<int> ids)
        {
            return logic.ReadBulk(ids);
        }
        [HttpPost("allforcustomer")]
        public IEnumerable<HourlyWageOrder> GetAllForCustomer([FromBody] Order order)
        {
            return logic.GetAllForCustomer(order.OrderDate, order.FirstName, order.LastName, order.EmailAddress);
        }
        [HttpPost("availableemployeesfororder")]
        public IEnumerable<HourlyWageEmployee> GetAvailableEmployeesForOrder([FromBody] Order order)
        {
            return logic.GetAvailableEmployeesForOrder(order);
        }
        [HttpPost]
        public void Create([FromBody] HourlyWageOrder value)
        {
            logic.Create(value);
            this.hub.Clients.All.SendAsync("HourlyWageOrderCreated", value.Id);
            refreshController.RefreshHourlyWageEmployee((int)value.EmployeeId);
        }
        [HttpPost("bulk")]
        public void CreateBulk([FromBody] HourlyWageOrder[] values)
        {
            logic.CreateBulk(values);
            this.hub.Clients.All.SendAsync("HourlyWageOrdersCreated", values.Select(value => value.Id));
            refreshController.RefreshHourlyWageEmployees(values.Select(o => o.EmployeeId));
        }
        [HttpPut]
        public void Update([FromBody] HourlyWageOrder value)
        {
            HourlyWageOrder updated = logic.Read((int)value.Id);
            int? oldEmployeeId = updated.EmployeeId;
            int newEmployeeId = (int)value.EmployeeId;

            logic.Update(value);
            this.hub.Clients.All.SendAsync("HourlyWageOrderUpdated", logic.Read((int)value?.Id));

            refreshController.RefreshHourlyWageEmployee(value.EmployeeId);
            refreshController.RefreshHourlyWageEmployee(newEmployeeId);
            if (newEmployeeId != oldEmployeeId)
            {
                refreshController.RefreshHourlyWageEmployee(oldEmployeeId);

                IEnumerable<int?> ids = logic.GetAllForCustomer(value.OrderDate, value.FirstName, value.LastName, value.EmailAddress).Where(o => o.Id != value.Id).Select(o => o.Id);
                refreshController.RefreshHourlyWageOrderGroup(ids, oldEmployeeId);
            }
        }
        [HttpPut("bulk")]
        public void UpdateBulk([FromBody] HourlyWageOrder[] values)
        {
            List<int> orderIds = values.Select(o => (int)o.Id).ToList();

            IEnumerable<int?> oldEmployeeIds = logic.ReadBulk(orderIds).Select(o => o.EmployeeId);
            IEnumerable<int?> newEmployeeIds = values.Select(o => o.EmployeeId);
            int? diffEmployeeId = oldEmployeeIds.Except(newEmployeeIds).FirstOrDefault();

            logic.UpdateBulk(values);
            this.hub.Clients.All.SendAsync("HourlyWageOrdersUpdated", logic.ReadBulk(values.Select(value => (int)value.Id)));

            refreshController.RefreshHourlyWageEmployees(newEmployeeIds);
            if (diffEmployeeId != 0)
            {
                refreshController.RefreshHourlyWageEmployee(diffEmployeeId);
            }
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            HourlyWageOrder deleted = logic.Read(id);
            int? employeeId = deleted.EmployeeId;

            logic.Delete(id);
            this.hub.Clients.All.SendAsync("HourlyWageOrderDeleted", deleted);

            refreshController.RefreshHourlyWageEmployee(employeeId);
        }
    }
}