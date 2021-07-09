using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CWWebApi.Data
{
    public class UserRepository : PropertyAccessEnumerableRepository<User>, IPropertyAccessEnumerableRepository<User>
    {

        public UserRepository(DbContext context) : base(context)
        {

        }

        public object Get(int id, string property)
        {
            var entity = Get(id);


            switch (property)
            {
                case nameof(User.Results):
                    {

                        var user = context.Find<User>(id);
                        context.Entry(user).Collection(e => e.Results).Load();
                        return user.Results;
                    }
                default:
                    return entity?.GetType().GetProperty(property)?.GetValue(entity);

            }

        }

    }
}
