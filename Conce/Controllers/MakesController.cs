using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Conce.Data;
using Conce.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Conce.Controllers.Resources;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Conce.Controllers
{
    [Route("api/[controller]")]
    public class MakesController : Controller
    {
        private readonly ConceContext ctx;
        private readonly IMapper mapper;

        public MakesController(ConceContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }
        // GET: api/makes
        [HttpGet]
        public async Task<IEnumerable<MakeResource>> Get()
        {
            var makes = await ctx.Makes.Include(m => m.Models).ToListAsync();
            return mapper.Map<List<Make>, List<MakeResource>>(makes);
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
