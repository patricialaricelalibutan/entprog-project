﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data;
using Microsoft.Data.SqlClient;

namespace FINALTEST1.Models
{
    class DonatorsDataAccessLayer
    {
        private const string ConnectionString = "Server=tcp:entprog-db-server.database.windows.net,1433;Initial Catalog=Funds4Safety_db;Persist Security Info=False;User ID=libutanp;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private SqlConnection cn = new SqlConnection(ConnectionString);
        private SqlCommand cmd;

        public void Open()
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }          
        }

        public void Close()
        {
            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
        }

        public void SetSql(string CommandText)
        {
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = CommandText;
        }

        public void AddParameter (string ParamName, object ParamValue)
        {
            cmd.Parameters.AddWithValue(ParamName, ParamValue);
        }

        public void ClearParameters()
        {
            cmd.Parameters.Clear();
        }

        public void Execute()
        {
            cmd.ExecuteNonQuery();
        }

        public SqlDataReader GetReader()
        {
            return cmd.ExecuteReader();
        }

        public DataTable GetData()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }
    }
}
