using System.Data;
using Test.Interfaces.Repository;

namespace Test.Repository
{
    public abstract class RepositoryBase : IRepositoryBase
    {

        private readonly IContextAcessor _contextAccessor;

        protected RepositoryBase(IContextAcessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        protected IDbConnection Connection => _contextAccessor.Connection;
        protected IDbTransaction? Transaction => _contextAccessor.Transaction;
    }
}
