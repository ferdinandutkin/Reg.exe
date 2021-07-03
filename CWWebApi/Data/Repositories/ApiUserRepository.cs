using CWWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CWWebApi.Data
{
    class ApiUserRepository : PropertyAccessEnumerableRepository<ApiUser>
   {
        
        public ApiUserRepository(DbContext context) : base(context)
        {

        }


        public object Get(int id, string property)
        {
            var entity = Get(id);


            switch (property)
            {
                case nameof(ApiUser.User):
                    {

                        var apiUser = context.Find<ApiUser>(id);
                        context.Entry(apiUser).Reference(apiUser => apiUser.User).Load();
                        return apiUser.User;
                    }
                default:
                    return entity?.GetType().GetProperty(property)?.GetValue(entity);

            }

        }

    }
}