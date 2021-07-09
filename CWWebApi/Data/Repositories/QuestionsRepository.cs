using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CWWebApi.Data
{
    public class QuestionsRepository : PropertyAccessEnumerableRepository<InputQuestion>, IPropertyAccessEnumerableRepository<InputQuestion>
    {

        public QuestionsRepository(DbContext context) : base(context)
        {

        }

        public object Get(int id, string property)
        {
            var entity = Get(id);


            switch (property)
            {
                case nameof(InputQuestion.TestCases):
                    {
                        var question = context.Find<InputQuestion>(id);
                        context.Entry(question).Collection(e => e.TestCases).Load();
                        return question.TestCases;
                    }
                default:
                    return entity?.GetType().GetProperty(property)?.GetValue(entity);

            }

        }

        public override IEnumerable<InputQuestion> GetAllWithPropertiesIncluded()
            =>
            entities.AsNoTracking().Include(e => e.TestCases).ThenInclude(t => t.Positions);



    }
}
