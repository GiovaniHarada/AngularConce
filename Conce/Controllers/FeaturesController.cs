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
    public class FeaturesController : Controller
    {
        private readonly ConceContext ctx;
        private readonly IMapper mapper;

        public FeaturesController(ConceContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<Feature>> Get()
        {
            var features = await ctx.Features.ToListAsync();
            return features;
        }

    }
}
