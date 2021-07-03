using Core.Classes;
using Core.Models;
using CWWebApi.Data;
using CWWebApi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CWWebApi.Controllers
{
    [Route("api/[controller]")]

    [Authorize(Roles = "Anonymous,User,Admin")]
    abstract public class BasicCRUDController<T> : ControllerBase where T : class, IEntity
    {
        protected readonly IEnumerableRepository<T> repository;
        private readonly ILogger<BasicCRUDController<T>> logger;
        public BasicCRUDController(IEnumerableRepository<T> repository, ILogger<BasicCRUDController<T>> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }



        [HttpGet]
        [Route("include_all/")]
        public virtual IEnumerable<T> GetAllIIncludeAll() => repository.GetAllWithPropertiesIncluded();




        [HttpGet]
        [Route("")]
        public virtual IEnumerable<T> GetAll() => repository.GetAll();



        [HttpGet]
        [Route("{id}")]
        public virtual T GetById(int id) => repository.Get(id);


        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        [Route("")]
        public virtual int Add([FromBody] T inputQuestion)
        {
            repository.Add(inputQuestion);
            return inputQuestion.Id;
        }


        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        [Route("update/")]
        public virtual void Update([FromBody] T entity)
        {
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve

            };

            logger.LogInformation(JsonSerializer.Serialize(entity, options));
            repository.Update(entity);
        }



        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public virtual void Delete(int id) => repository.Delete(id);
    }
}

