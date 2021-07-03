using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWWebApi.Data
{
    public class TestCaseRepository : PropertyAccessEnumerableRepository<TestCase>, IPropertyAccessEnumerableRepository<TestCase>
    {

        public TestCaseRepository(DbContext context) :base(context)
        {

        }

        public object Get(int id, string property)
        {
            var entity = Get(id);


            switch (property)
            {
                case nameof(TestCase.Positions): {
                        
                        var testCase = context.Find<TestCase>(id);
                        context.Entry(testCase).Collection(e => e.Positions).Load();
                        return testCase.Positions;
                    }
                default:
                    return entity?.GetType().GetProperty(property)?.GetValue(entity);
                  
            }
           
        }

    }
}
