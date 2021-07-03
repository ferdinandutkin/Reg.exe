using Core.Models;
using Microsoft.AspNetCore.Mvc;
using CWWebApi.Data;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

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

