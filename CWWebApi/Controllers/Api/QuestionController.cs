using Core.Models;
using Microsoft.AspNetCore.Mvc;
using CWWebApi.Data;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace CWWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : BasicCRUDControllerWithPropertyAcess<InputQuestion>
    {
       
       public QuestionController(IPropertyAccessEnumerableRepository<InputQuestion> repository, ILogger<QuestionController> logger) :
            base(repository, logger)
        {
            
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("update/")]
        public override void Update([FromBody] InputQuestion entity) => base.Update(entity);


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("{id}/{property}/")]
        public override void Update(int id, string property, [FromBody] object newValue) => base.Update(id, property, newValue);




        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public override void Delete(int id) => base.Delete(id);


    }
}

