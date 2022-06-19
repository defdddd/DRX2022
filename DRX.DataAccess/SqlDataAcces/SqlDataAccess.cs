using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.DataAccess.SqlDataAcces
{
    public class SqlDataAccess : ISqlDataAccess
    {
        public string Connection { get; private set; }
        public SqlDataAccess(string connection)
        {
            Connection = connection;
        }
    }
}
