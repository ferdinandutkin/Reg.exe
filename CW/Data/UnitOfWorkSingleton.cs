using System;

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
