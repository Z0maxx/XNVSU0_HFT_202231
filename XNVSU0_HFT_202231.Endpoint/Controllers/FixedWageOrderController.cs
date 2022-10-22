using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using XNVSU0_HFT_202231.Logic;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FixedWageOrderController : ControllerBase
    {
        readonly IFixedWageOrderLogic logic;
        public FixedWageOrderController(IFixedWageOrderLogic logic)
        {
            this.logic = logic;
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
        [HttpPost]
        public void Create([FromBody] FixedWageOrder value)
        {
            logic.Create(value);
        }
        [HttpPut]
        public void Update([FromBody] FixedWageOrder value)
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