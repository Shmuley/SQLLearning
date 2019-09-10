﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SQLLearning.Data
{
    public class DisconnectedDataLayer
    {
        private SqlConnection connection;
        private SqlDataAdapter dataAdapter;
        private SqlCommandBuilder commandBuilder;

        public DisconnectedDataLayer(SqlConnection connection)
        {
            this.connection = connection;
        }

        public DataSet QueryData(string queryString, string tableName)
        {
            dataAdapter = new SqlDataAdapter(queryString, connection);
            commandBuilder = new SqlCommandBuilder(dataAdapter);

            DataSet dt = new DataSet();
            dataAdapter.Fill(dt, tableName);
            return dt;
        }

        public DataSet InsertData(DataSet dataSet, string tableName)
        {
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