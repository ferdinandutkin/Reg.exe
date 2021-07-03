using Core.Models;
using Microsoft.AspNetCore.Mvc;
using CWWebApi.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace CWWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : BasicCRUDControllerWithPropertyAcess<TestResult>
    {
        public ResultsController(IPropertyAccessEnumerableRepository<TestResult> repository, ILogger<ResultsController> logger) :
             base(repository, logger)
        {
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        [Route("")]
        public override int Add([FromBody] TestResult inputQuestion)
        {
            repository.Add(inputQuestion);
            return inputQuestion.Id;
        }




    
    }
}

