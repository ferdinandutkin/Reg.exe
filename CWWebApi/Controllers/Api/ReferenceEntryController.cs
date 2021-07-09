using Core.Models;
using CWWebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CWWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReferenceEntryController : BasicCRUDControllerWithPropertyAcess<ReferenceEntry>
    {
        public ReferenceEntryController(IPropertyAccessEnumerableRepository<ReferenceEntry> repository, ILogger<ReferenceEntryController> logger) :
             base(repository, logger)
        {

        }
    }
}

