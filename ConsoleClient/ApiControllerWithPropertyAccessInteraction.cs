using Core.Classes;
using Core.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace ConsoleClient
{
    public partial class ServerInteraction
    {
        public class ApiControllerWithPropertyAccessInteraction<T> : APIControllerInteraction<T> where T : IEntity
        {
            public ApiControllerWithPropertyAccessInteraction(ServerInteraction serverInteraction, string routeToController) : base(serverInteraction, routeToController)
            {
            }

            public void UpdateProperty(int id, string propertyName, object newValue)
            {

                var prop = Property<object>.CreateInstance(newValue);
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, $"{routeToController}/{id}/{propertyName}")
                    {
                        Content = new StringContent(JsonSerializer.Serialize(prop, prop.GetType(), options), Encoding.UTF8, "application/json")
                    };
                    _ = client.SendAsync(request).Result;
                }
                catch (Exception)
                {
                    Debug.WriteLine("F");
                }


            }




            public object GetProperty(int id, string propertyName)
            {

                object result = default;


                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, $"{routeToController}/{id}/{propertyName}");

                    var response = client.SendAsync(request).Result;



                    if (response.IsSuccessStatusCode)
                    {

                        var read = response.Content.ReadAsStringAsync().Result;



                        var propType = Property<object>.MakeGenericTypeFromPropertyName(typeof(T), propertyName);




                        var res = JsonSerializer.Deserialize(read, propType, options);

                        result = propType.GetProperty(nameof(Property<object>.Value)).GetValue(res);



                    }


                }
                catch
                {
                    Debug.WriteLine("F");
                }

                return result;




            }



        }


    }
}
