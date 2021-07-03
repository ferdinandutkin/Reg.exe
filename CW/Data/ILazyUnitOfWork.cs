using Core.Classes;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
