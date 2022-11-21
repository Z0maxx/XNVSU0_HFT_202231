using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using XNVSU0_HFT_202231.Logic;
using XNVSU0_HFT_202231.Models.TableModels;

namespace XNVSU0_HFT_202231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        readonly IJobLogic logic;
        public JobController(IJobLogic logic)
        {
            this.logic = logic;
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
        }
        [HttpPut]
        public void Update([FromBody] Job value)
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