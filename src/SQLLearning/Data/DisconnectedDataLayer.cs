using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SQLLearning.Data
{
    public class DisconnectedDataLayer
    {
        private SqlConnection connection;
        private SqlDataAdapter dataAdapter;
        private SqlCommandBuilder commandBuilder;

        public DisconnectedDataLayer(SqlConnection connection, SqlDataAdapter dataAdapter)
        {
            this.connection = connection;
            this.dataAdapter = dataAdapter;
        }
        public DataSet QueryData(string queryString, string tableName)
        {
            dataAdapter = new SqlDataAdapter(queryString, connection);
            commandBuilder = new SqlCommandBuilder(dataAdapter);

            DataSet dt = new DataSet();
            dataAdapter.Fill(dt, tableName);

            return dt;
        }

        public DataSet QueryData(string queryString, SqlParameter parameter, string tableName)
        {

            var cmd = new SqlCommand(queryString, connection);
            if(parameter != null)
            {
                cmd.Parameters.Add(parameter);
            }

            dataAdapter = new SqlDataAdapter(cmd);
            commandBuilder = new SqlCommandBuilder(dataAdapter);

            DataSet dt = new DataSet();
            dataAdapter.Fill(dt, tableName);

            return dt;
        }

        public DataSet InsertData(DataSet dataSet, string tableName)
        {
            var insertCommand = commandBuilder.GetInsertCommand();
            dataAdapter.Update(dataSet, tableName);
            return dataSet;
        }

        public DataSet DeleteData(DataSet dataSet, List<DataRow> dataRows, string tableName)
        {
            foreach (var row in dataRows)
            {
                row.Delete();
            }
            dataAdapter.Update(dataSet, tableName);
            return dataSet;
        }
    }
}
