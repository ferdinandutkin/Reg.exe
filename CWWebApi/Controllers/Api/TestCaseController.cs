using Core.Models;
using CWWebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CWWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestCaseController : BasicCRUDControllerWithPropertyAcess<TestCase>
    {



        public TestCaseController(IPropertyAccessEnumerableRepository<TestCase> repository, ILogger<TestCaseController> logger) :
             base(repository, logger)
        {

        }



    }
}

