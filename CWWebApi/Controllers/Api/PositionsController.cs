using Core.Models;
using CWWebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CWWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PositionsController : BasicCRUDControllerWithPropertyAcess<Position>
    {
        public PositionsController(IPropertyAccessEnumerableRepository<Position> repository, ILogger<PositionsController> logger) :
             base(repository, logger)
        {

        }
    }
}

