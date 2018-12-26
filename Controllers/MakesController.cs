using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplicationWithCore.Models;
using WebApplicationWithCore.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AutoMapper;
using WebApplicationWithCore.Controllers.Resources;
namespace WebApplicationWithCore.Controllers
{

    public class MakesController : Controller
    {
        private readonly WebApplicationWithCoreDbContext context;
        private readonly IMapper mapper;

        public MakesController(WebApplicationWithCoreDbContext context , IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }
        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes =  await context.Makes.Include(m => m.Models).ToListAsync();
            return mapper.Map<List<Make>,List<MakeResource>>(makes);
        } 
    }
}