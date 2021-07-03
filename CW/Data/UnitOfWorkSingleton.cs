using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW.Data
{
    class UnitOfWorkSingleton
    {
       
        private static readonly 
                Lazy<ILazyUnitOfWork> lazy
                = new(
                    () => new WebUntOfWork());
        public static ILazyUnitOfWork Instance => lazy.Value;

    }
}
