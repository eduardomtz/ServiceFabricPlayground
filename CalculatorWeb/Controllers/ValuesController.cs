using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using CalculatorService.Interfaces;

namespace CalculatorWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public string Add(int a, int b)
        {
            var calculatorClient = ServiceProxy.Create<ICalculatorService>(
                new Uri("fabric:/CalculatorApplication/CalculatorService"));
            return calculatorClient.Add(1, 2).Result;
        }

        [HttpGet]
        public string Subtract(int a, int b)
        {
            var calculatorClient = ServiceProxy.Create<ICalculatorService>(
                new Uri("fabric:/CalculatorApplication/CalculatorService"));
            return calculatorClient.Subtract(1, 2).Result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}