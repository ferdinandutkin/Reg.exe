using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public partial class ServerInteraction
    {
        public class BasicApiControllerInteraction
        {
            public ServerInteraction Parent { get; private set; }
            protected readonly HttpClient client;
            protected readonly string routeToController;

            protected readonly JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault

            };


            protected async ValueTask<HttpResponseMessage> BasicDeleteAsync(params object[] additionalParams)
            {
                return await client.DeleteAsync(BuildParams(additionalParams));
            }

                protected string BuildParams(params object[] additionalParams)
            {
                var ret = string.Join('/', additionalParams.Prepend(routeToController));
                return ret;
            }

            public BasicApiControllerInteraction(ServerInteraction serverInteraction, string routeToController)
            {
                this.Parent = serverInteraction;
                this.client = serverInteraction.client;
                this.routeToController = routeToController;
            }


            protected async ValueTask<U> BasicGetAsync<U>(params object[] additionalParams)
            {
               

                return await client.GetFromJsonAsync<U>(BuildParams(additionalParams), options).ConfigureAwait(false);
               

            }

            protected async ValueTask<IEnumerable<U>> BasicGetCollectionAsync<U>(params object[] additionalParams)
            {
                return await BasicGetAsync<List<U>>(additionalParams).ConfigureAwait(false);
            }



            protected U BasicGet<U>(params object[] additionalParams)
            {
                return BasicGetAsync<U>(additionalParams).Result;

            }


            protected IEnumerable<U> BasicGetCollection<U>(params object[] additionalParams)
            {
                return BasicGetCollectionAsync<U>(additionalParams).Result;
            }



        }


    }
}
