using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationWithCore.Controllers.Resources;
using WebApplicationWithCore.Models;
using WebApplicationWithCore.Persistence;
namespace WebApplicationWithCore.Controllers
{
    public class FeaturesController
    {
        private readonly WebApplicationWithCoreDbContext context;
    private readonly IMapper mapper;
    public FeaturesController(WebApplicationWithCoreDbContext context, IMapper mapper)
    {
      this.mapper = mapper;
      this.context = context;
    }

    [HttpGet("/api/features")]
    public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
    {
      var features = await context.Features.ToListAsync();
      
      return mapper.Map<List<Feature>, List<KeyValuePairResource>>(features); 
    }
    }
}