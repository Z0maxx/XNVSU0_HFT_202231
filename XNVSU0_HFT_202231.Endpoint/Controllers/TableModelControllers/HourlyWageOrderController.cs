using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using XNVSU0_HFT_202231.Logic;
using XNVSU0_HFT_202231.Models.TableModels;

namespace XNVSU0_HFT_202231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HourlyWageOrderController : ControllerBase
    {
        readonly IHourlyWageOrderLogic logic;
        public HourlyWageOrderController(IHourlyWageOrderLogic logic)
        {
            this.logic = logic;
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
        [HttpPost]
        public void Create([FromBody] HourlyWageOrder value)
        {
            logic.Create(value);
        }
        [HttpPut]
        public void Update([FromBody] HourlyWageOrder value)
        {
            logic.Update(value);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logic.Delete(id);
        }
    }
}