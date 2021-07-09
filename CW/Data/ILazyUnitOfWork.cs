using Core.Models;

namespace CW.Data
{

    public interface ILazyUnitOfWork
    {
        ILazyRepository<InputQuestion> QuestionsRepository { get; }


        ILazyRepository<User> UsersRepository { get; }

        ILazyRepository<TestResult> TestResulsRepository { get; }

        ILazyRepository<ReferenceEntry> RefenceRepository { get; }

    }


}
