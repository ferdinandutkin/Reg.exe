
using ConsoleClient;
using Core.Classes;
using Core.Models;
using System.Collections.Generic;

namespace CW.Data
{
    public class WebUntOfWork : ILazyUnitOfWork
    {

        public ILazyRepository<InputQuestion> QuestionsRepository =>
            new QuestionRepository(ServerInteractionSigleton.Instance.QuestionControllerCallsWrapper);

        public ILazyRepository<User> UsersRepository
            => new LazyRepository<User>(ServerInteractionSigleton.Instance.UserControllerCallsWrapper);

        public ILazyRepository<TestResult> TestResulsRepository =>
               new LazyRepository<TestResult>(ServerInteractionSigleton.Instance.ResultsControllerCallsWrapper);

        public ILazyRepository<ReferenceEntry> RefenceRepository =>
            new LazyRepository<ReferenceEntry>(ServerInteractionSigleton.Instance.ReferenceEntryControllerCallsWrapper);

    }



    public class QuestionRepository : ILazyRepository<InputQuestion>
    {

        readonly ServerInteraction.APIControllerInteraction<InputQuestion> wrapper;
        public QuestionRepository(ServerInteraction.APIControllerInteraction<InputQuestion> wrapper)
        {
            this.wrapper = wrapper;
        }

        public void Add(InputQuestion entity) => wrapper.Add(entity);

        public void Delete(int id) => wrapper.Delete(id);


        public InputQuestion Get(int id) => wrapper.Get(id);


        public ILazySynchronizedCollection<InputQuestion> GetAll() =>
            new LazyNotifiableEntityCollection<InputQuestion>(wrapper.Get());

        public ISynchronizedCollection<InputQuestion> GetAllWithPropertiesIncluded() => new InputQuestionSynchronizedCollection(wrapper.GetIncludeAll());


        public void Update(InputQuestion entity) => wrapper.Update(entity);

        IEnumerable<InputQuestion> IRepository<InputQuestion>.GetAll() => wrapper.Get();

        IEnumerable<InputQuestion> IRepository<InputQuestion>.GetAllWithPropertiesIncluded() => wrapper.GetIncludeAll();

    }



    public class ReferenceEntryRepository : ILazyRepository<ReferenceEntry>
    {

        readonly ServerInteraction.APIControllerInteraction<ReferenceEntry> wrapper;
        public ReferenceEntryRepository(ServerInteraction.APIControllerInteraction<ReferenceEntry> wrapper)
        {
            this.wrapper = wrapper;
        }

        public void Add(ReferenceEntry entity) => wrapper.Add(entity);

        public void Delete(int id) => wrapper.Delete(id);


        public ReferenceEntry Get(int id) => wrapper.Get(id);


        public ILazySynchronizedCollection<ReferenceEntry> GetAll() =>
            new LazyNotifiableEntityCollection<ReferenceEntry>(wrapper.Get());

        public ISynchronizedCollection<ReferenceEntry> GetAllWithPropertiesIncluded() => new ReferenceEntrySyncronizedCollection(wrapper.GetIncludeAll());


        public void Update(ReferenceEntry entity) => wrapper.Update(entity);

        IEnumerable<ReferenceEntry> IRepository<ReferenceEntry>.GetAll() => wrapper.Get();

        IEnumerable<ReferenceEntry> IRepository<ReferenceEntry>.GetAllWithPropertiesIncluded() => wrapper.GetIncludeAll();

    }





    public class LazyRepository<T> : ILazyRepository<T> where T : class, INotifiableEntity, new()
    {

        readonly ServerInteraction.APIControllerInteraction<T> wrapper;
        public LazyRepository(ServerInteraction.APIControllerInteraction<T> wrapper)
        {
            this.wrapper = wrapper;
        }

        public void Add(T entity) => wrapper.Add(entity);

        public void Delete(int id) => wrapper.Delete(id);


        public T Get(int id) => wrapper.Get(id);


        public ILazySynchronizedCollection<T> GetAll() =>
            new LazyNotifiableEntityCollection<T>(wrapper.Get());

        public ISynchronizedCollection<T> GetAllWithPropertiesIncluded() => new SynchronizedCollection<T>(wrapper.GetIncludeAll());


        public void Update(T entity) => wrapper.Update(entity);

        IEnumerable<T> IRepository<T>.GetAll() => wrapper.Get();

        IEnumerable<T> IRepository<T>.GetAllWithPropertiesIncluded() => wrapper.GetIncludeAll();

    }




}
