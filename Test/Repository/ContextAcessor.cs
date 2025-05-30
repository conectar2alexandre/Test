
using Test.Interfaces.Repository;
using System.Data;
using Test.Interfaces.Repository;

namespace src.Repository
{
    public class ContextAcessor : IContextAcessor
    {
        public IDbConnection Connection { get; set; }

        public IDbTransaction? Transaction { get; set; }
    }
}

