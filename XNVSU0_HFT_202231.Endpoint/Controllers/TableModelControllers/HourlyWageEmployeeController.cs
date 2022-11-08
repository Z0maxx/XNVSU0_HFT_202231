using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using XNVSU0_HFT_202231.Logic;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HourlyWageEmployeeController : ControllerBase
    {
        readonly IHourlyWageEmployeeLogic logic;
        public HourlyWageEmployeeController(IHourlyWageEmployeeLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<HourlyWageEmployee> ReadAll()
        {
            return logic.ReadAll();
        }
        [HttpGet("{id}")]
        public HourlyWageEmployee Read(int id)
        {
            return logic.Read(id);
        }
        [HttpPost]
        public void Create([FromBody] HourlyWageEmployee value)
        {
            logic.Create(value);
        }
        [HttpPut]
        public void Update([FromBody] HourlyWageEmployee value)
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