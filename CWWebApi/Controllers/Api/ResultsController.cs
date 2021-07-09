using Core.Models;
using CWWebApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

