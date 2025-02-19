using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Kozubenko.Database
{
    public class SqliteDb : IDisposable
    {
        
        //public SQLiteConnection Connection { get; private set; }

        public SqliteDb()
        {

        }

        public static SqliteDb LockDb()
        {
            return new SqliteDb();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
