using Core.Classes;
using Core.Models;
using CWWebApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CWWebApi.Controllers
{
    [Route("api/[controller]")]

    abstract public class BasicCRUDControllerWithPropertyAcess<T> : BasicCRUDController<T> where T : class, IEntity
    {
        private readonly IPropertyAccessEnumerableRepository<T> propAccessRepo;
        private readonly ILogger<BasicCRUDControllerWithPropertyAcess<T>> logger;
        public BasicCRUDControllerWithPropertyAcess(IPropertyAccessEnumerableRepository<T> repository, ILogger<BasicCRUDControllerWithPropertyAcess<T>> logger)
            : base(repository, logger)
        {
            propAccessRepo = repository;
            this.logger = logger;
        }



        [Route("{id}/{property}/")]
        [HttpGet]
        public virtual string Get(int id, string property)
        {
            var res = propAccessRepo.Get(id, property);

            logger.LogInformation($"id : {id}, свойство : {property}, отправлено значение: {res} ");


            ///эх вот бы были объекты анонимного типа
            ///так падождите...
            ///но я не буду переделывать потому что боюсь сломать

            var toSerialize = Property<object>.CreateInstance(res);
            return JsonSerializer.Serialize(toSerialize, toSerialize.GetType(),
                new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.Preserve });


        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        [Route("{id}/{property}/")]
        public virtual void Update(int id, string property, [FromBody] object newValue)
        {
            var propType = Property<T>.MakeGenericTypeFromPropertyName(property);

            var newVal = Property<T>.GetValue(JsonSerializer.Deserialize(newValue.ToString(), propType,
                new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.Preserve }));


            propAccessRepo.Update(id, property, newVal);

            logger.LogInformation($"id : {id}, свойство : {property}, новое значение : {newVal} ");
        }


    }
}

