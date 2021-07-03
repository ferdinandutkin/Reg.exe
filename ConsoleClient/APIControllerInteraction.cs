using Core.Classes;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public partial class ServerInteraction
    {
        public class APIControllerInteraction<T> : BasicApiControllerInteraction where T : IEntity
        {




            public APIControllerInteraction(ServerInteraction serverInteraction, string routeToController) : base(serverInteraction, routeToController)
            {

            }



            public IEnumerable<T> GetIncludeAll()
            {
                return BasicGetCollection<T>("include_all");
            }




            public void Delete(int id) => _ = BasicDeleteAsync(id).Result;
            private async ValueTask<HttpResponseMessage> BasicPostAsync<U>(U value, params object[] additionalParams)
                => await client.PostAsJsonAsync(BuildParams(additionalParams), value, options).ConfigureAwait(false);

            public async ValueTask<int> AddAsync(T entity)
            {

                var responce = await BasicPostAsync(entity).ConfigureAwait(false);
                if (responce.IsSuccessStatusCode)
                {
                    return await responce.Content.ReadFromJsonAsync<int>(options).ConfigureAwait(false);
                }

                return 0;

            }

            public int Add(T entity)
            {
                return AddAsync(entity).Result;
            }

            public async ValueTask UpdateAsync(T entity)
            {
                _ = await BasicPostAsync(entity, "update").ConfigureAwait(false);
                return;
            }
            public void Update(T entity)
            {
                UpdateAsync(entity).ConfigureAwait(false).GetAwaiter().GetResult();


            }
            public ValueTask<IEnumerable<T>> GetAsync() => BasicGetCollectionAsync<T>();

            public IEnumerable<T> Get() => BasicGetCollection<T>();

            public T Get(int id)
            {

                return BasicGet<T>(id);
            }



        }


    }
}