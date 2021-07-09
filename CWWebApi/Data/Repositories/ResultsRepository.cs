using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CWWebApi.Data
{
    public class ResultsRepository : PropertyAccessEnumerableRepository<TestResult>, IPropertyAccessEnumerableRepository<TestResult>
    {

        public ResultsRepository(DbContext context) : base(context)
        {

        }

        public object Get(int id, string property)
        {
            var entity = Get(id);


            switch (property)
            {
                case nameof(TestResult.User):
                    {
                        var testResult = context.Find<TestResult>(id);
                        context.Entry(testResult).Reference(e => e.User).Load();
                        return testResult.User;
                    }
                default:
                    return entity?.GetType().GetProperty(property)?.GetValue(entity);

            }

        }

    }
}
