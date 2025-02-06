using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;

    //DataAccess is default to Microsoft SQL Server
    public class DataAccess : DataAccessF
    {
        public DataAccess()
        {
            this.Intialize();
        }

        public DataAccess(string connection_string)
        {
            this.ConnectionString = connection_string;
            this.Intialize();
        }

        protected override void Intialize()
        {
            this.DbCon = new SqlConnection();
            SqlTransaction sql_dbtran = null;
            this.DbTran = sql_dbtran;
            this.DbComm = new SqlCommand();
            this.DbAdap = new SqlDataAdapter();

            SqlDataReader sql_dbread = null;
            this.DbRead = sql_dbread;
        }


      
    }
