using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SQLLearning.Data
{
    public static class DataReaderHelpers
    {
        public static T GetFieldValue<T>(this SqlDataReader rdr, string name)
        {
            T ret = default;

            if (!rdr[name].Equals(DBNull.Value))
            {
                ret = (T)rdr[name];
            }
            return ret;
        }
    }
}
