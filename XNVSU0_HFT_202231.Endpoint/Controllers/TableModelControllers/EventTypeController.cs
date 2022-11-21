using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using XNVSU0_HFT_202231.Logic;
using XNVSU0_HFT_202231.Models.TableModels;

namespace XNVSU0_HFT_202231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventTypeController : ControllerBase
    {
        readonly IEventTypeLogic logic;
        public EventTypeController(IEventTypeLogic logic)
        {
            this.logic = logic;
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
        }
        [HttpPut]
        public void Update([FromBody] EventType value)
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