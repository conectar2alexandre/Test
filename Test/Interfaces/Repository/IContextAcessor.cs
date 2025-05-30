using System.Data;

namespace Test.Interfaces.Repository
{

        public interface IContextAcessor
        {
            IDbConnection Connection { get; set; }
            IDbTransaction? Transaction { get; set; }
        }

}
