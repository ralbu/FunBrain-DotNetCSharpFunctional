using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Functional;
//using Functional;
using Microsoft.AspNetCore.Mvc;
//using static Functional.F;

namespace FunBrainApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
//            var list = Enumerable.Range(1, 5);
//
//            var multiplier = list.Map(x => x + id);

            return Ok();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    }

}
