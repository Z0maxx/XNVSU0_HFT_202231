using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using XNVSU0_HFT_202231.Logic;
using XNVSU0_HFT_202231.Models.TableModels;

namespace XNVSU0_HFT_202231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FixedWageEmployeeController : ControllerBase
    {
        readonly IFixedWageEmployeeLogic logic;
        public FixedWageEmployeeController(IFixedWageEmployeeLogic logic)
        {
            this.logic = logic;
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
        [HttpPost]
        public void Create([FromBody] FixedWageEmployee value)
        {
            logic.Create(value);
        }
        [HttpPut]
        public void Update([FromBody] FixedWageEmployee value)
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