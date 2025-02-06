using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Data.Common;
using System.Data.OleDb;
using System.Web;
using System.Collections.Generic;

	/// <summary>
	/// Utilized as Main class for accessing the database.
	/// Takes connection string from web.config file...
	/// Connect the database using SqlConnection method...
	/// Executes the passed sql string and command object and returns the number of records affected..
	/// Executes collection of command object....and if any error occured in any of the command object execution then it roll backs all the Transaction
	/// </summary>
    public abstract class DataAccessF
    {
        public LogWriter LW;

        protected int int_DbServerType = 1;
        protected string str_ConStr = "";
        protected int int_rows_affected = 0;
        protected ArrayList al_RowsAffected;
        protected string str_db_message = "";
        protected Exception DBException;
        protected int int_CommandTimeout = 100;
        protected bool bln_ManagedConnection = false;
        protected bool bln_ManagedTransaction = false;

        public DbConnection DbCon;
        public DbTransaction DbTran;
        public DbCommand DbComm;
        public DbDataAdapter DbAdap;
        public DbDataReader DbRead;

        protected bool bln_UseSymmetricKey = false;

        //Initalize method is used to intialize the appropriate database objects
        protected abstract void Intialize();

        #region Properties 
        public string ConnectionString{get{ return this.str_ConStr; } set { this.str_ConStr = value; }}
        public string DbMessage { get { return this.str_db_message; } set { this.str_db_message = value; } }
        public bool ManagedConnection{get { return this.bln_ManagedConnection; }set { this.bln_ManagedConnection = value; }}
        public bool ManagedTransaction{get { return this.bln_ManagedTransaction; }set { this.bln_ManagedTransaction = value; }}
        public int DbServerType { get { return this.int_DbServerType; } set { this.int_DbServerType = value; } }
        public int RowsAffected { get { return this.int_rows_affected; } set { this.int_rows_affected = value; } }
        public ArrayList RowsAffectedList { get { return this.al_RowsAffected; } set { this.al_RowsAffected = value; } }
        public int DbCommandTimeout { get { return this.int_CommandTimeout; } set { this.int_CommandTimeout = value; } }
        public bool UseSymmetricKey { get { return this.bln_UseSymmetricKey; } set { this.bln_UseSymmetricKey = value; } }
        #endregion

        #region Constructors

        public DataAccessF()
        {
            this.LW = new LogWriter();
             this.ConnectionString = new AppVar ().DatabaseConnectionString;
             
        }

        #endregion
        
        #region Managed Connections
        //support for managed connection and managed transaction
        /// <summary>
        /// Opens the database connection manually. if database connection opens by manual operation
        /// then manual connection close is needed.
        /// </summary>

        public void OpenDatabaseConnection()
        {
            this.DbCon.ConnectionString = this.ConnectionString;
            this.DbCon.Open();
        }
        /// <summary>
        /// Closes the database connection manually. 
        /// </summary>

        public void CloseDatabaseConnection()
        {
            this.DbCon.Close();
        }
        #endregion

        #region Manage Transactions
        public void TransactionBegin()
        {
            this.DbTran = this.DbCon.BeginTransaction();
        }

        public void TransactionRollback()
        {
            this.DbTran.Rollback();
        }

        public void TransactionCommit()
        {
            this.DbTran.Commit();
        }
        #endregion
        
        #region Write Log
        /// <summary>
        /// Writes the Command parameter into the log.
        /// </summary>
        /// <param name="Param">Parameter Collections</param>
        /// <returns>Boolean</returns>

        private void WriteCmdParameterLog(DbCommand Cmd_Obj, LogWriter.Severity LwSeverity)
        {
            DbParameterCollection Param = Cmd_Obj.Parameters;
            int i = 0;
            string str_cmdlog = "";
            for (i = 0; i < Param.Count; i++)
            {
                str_cmdlog += "Param Name : " + Param[i].ParameterName + ";Type : " + Param[i].DbType.ToString() + "; Value : " + Param[i].Value + " | ";
            }
            //this.LW.WriteLog("Param Name : " + Param[i].ParameterName + ";Type : " + Param[i].DbType.ToString() + "; Value : " + Param[i].Value, LwSeverity);
            this.LW.WriteLog(str_cmdlog, LwSeverity); 
        }

        #endregion

        #region ExecuteScalar - returns string using DataSet

        public string ExcecuteScalar(string str_TblName, string str_FldName, string str_Where, string str_Order, string str_Group)
        {
            string strRet = "";
            string str_Sql = "";
            char chr_ConcatenationOpr = '#';

            str_Sql = "select " + str_FldName + " from " + str_TblName;
            if (str_Where != "")
            {
                str_Sql += " where " + str_Where;
            }
            if (str_Group != "")
            {
                str_Sql += " group by " + str_Group;
            }
            if (str_Order != "")
            {
                str_Sql += " order by " + str_Order;
            }
            DataSet dse = this.GetDataSet(str_Sql);
            for (int i = 0; i < dse.Tables[0].Rows.Count; i++)
            {
                if (i > 0)
                {
                    strRet += chr_ConcatenationOpr;
                }
                if (dse.Tables[0].Rows[i][0] != DBNull.Value)
                {
                    strRet += dse.Tables[0].Rows[i][0].ToString().Trim();
                }
            }
            dse.Dispose();
            return strRet;
        }

        public string ExcecuteScalar(string str_TblName, string str_FldName)
        {
            return ExcecuteScalar(str_TblName, str_FldName, "", "", "").ToString();
        }

        public string ExcecuteScalar(string str_TblName, string str_FldName, string str_Where)
        {
            return ExcecuteScalar(str_TblName, str_FldName, str_Where, "", "").ToString();
        }

        public string ExcecuteScalar(string str_TblName, string str_FldName, string str_Where, string str_Order)
        {
            return ExcecuteScalar(str_TblName, str_FldName, str_Where, str_Order, "").ToString();
        }
        #endregion

        #region DataTable/DataSet - Select Statements - using Command/SQL
        /// <summary>
        /// Executes the command object passed as parameter 
        /// and returns Data Table
        /// </summary>
        /// <param name="sco_CmdObj">DbCommand object</param>
        /// <returns>Data Table</returns>

        public DataTable GetDataTable(DbCommand Cmd_Obj)
        {
            this.DBException = null;
            this.int_rows_affected = 0;
            this.str_db_message = "";

            DataTable dt = null;
            this.DbComm = Cmd_Obj;
            this.DbComm.CommandTimeout = 0;
            //this.DbComm.CommandTimeout = this.DbCommandTimeout;
            this.DbComm.Connection = this.DbCon;

            if (this.UseSymmetricKey == true)
            {
                this.DbComm.CommandText = "OPEN SYMMETRIC KEY EncryptKey DECRYPTION BY CERTIFICATE EncryptCert; "
                    + this.DbComm.CommandText
                    + "; CLOSE SYMMETRIC KEY EncryptKey ";
            }

            try
            {
                this.LW.WriteLog("Sql : " + this.DbComm.CommandText, LogWriter.Severity.Information);
                this.OpenDatabaseConnection();
                this.DbRead = this.DbComm.ExecuteReader(CommandBehavior.CloseConnection);
                dt = new DataTable();
                dt.Load(this.DbRead);
                this.int_rows_affected = dt.Rows.Count;
                this.DbRead.Close();  
            }
            catch (Exception ex)
            {
                this.DBException = ex;
                this.str_db_message = ex.ToString();
                this.WriteCmdParameterLog(this.DbComm, LogWriter.Severity.Error);
                this.LW.WriteLog(ex.ToString(), LogWriter.Severity.Error);
            }
            finally
            {
                if (this.DbCon.State == ConnectionState.Open) this.DbCon.Close();
            }
            if (this.DBException != null) throw this.DBException;
            return dt; 
        }

        public DataTable GetDataTable(string str_sql)
        {
            this.DbComm.CommandText = str_sql;
            return this.GetDataTable(this.DbComm);  
        }

        public DataSet GetDataSet(string str_sql)
        {
            this.DbComm.CommandText = str_sql;
            return this.GetDataSet(this.DbComm);
        }
        public DataSet GetDataSet(DbCommand Cmd_Obj)
        {
            return this.SqlSelect(Cmd_Obj); 
        }
        #endregion

        #region SQLSelect

        protected DataSet SqlSelect(DbCommand Cmd_Obj)
        {
            DataSet dse_Obj = new DataSet();

            this.DBException = null;
            this.int_rows_affected = 0;
            this.str_db_message = "";


            this.DbComm = Cmd_Obj;
            this.DbComm.CommandTimeout = this.int_CommandTimeout;
            this.DbComm.Connection = this.DbCon;
            this.DbAdap.SelectCommand = this.DbComm;
            this.DbCon.ConnectionString = this.ConnectionString;
            this.LW.WriteLog("Sql : " + this.DbComm.CommandText, LogWriter.Severity.Information);

            try
            {
                this.OpenDatabaseConnection();
                this.DbAdap.Fill(dse_Obj, "tbl");
                this.int_rows_affected = dse_Obj.Tables[0].Rows.Count;
            }
            catch (Exception ex)
            {
                this.WriteCmdParameterLog(this.DbComm, LogWriter.Severity.Error);  
                this.DBException = ex;
                this.str_db_message = ex.ToString();
                this.LW.WriteLog(ex.ToString(), LogWriter.Severity.Error);
            }
            finally
            {
                this.CloseDatabaseConnection(); 
            }

            if (this.DBException != null)  throw this.DBException;
            return dse_Obj ;
        }
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// Executes command objects in the array list one by one
        /// If any error occur in execution it will roll back the execution and returns false
        /// </summary>
        /// <param name="arr_Cmd">ArrayList</param>
        /// <returns>Boolean</returns>
        public bool ExecuteNonQuery(ArrayList arr_CmdObj)
        {
            bool bln_Status = true;
            ArrayList arr_ResObj = new ArrayList();
            arr_ResObj = this.SqlUpdate(arr_CmdObj);
            if (arr_ResObj.Count != arr_CmdObj.Count)
                bln_Status = false;
            return bln_Status;
        }
        public bool ExecuteNonQuery(string str_Sql)
        {
            SqlCommand sco_CmdObj = new SqlCommand(str_Sql);
            ArrayList arr_CmdObj = new ArrayList();
            arr_CmdObj.Add(sco_CmdObj);
            return ExecuteNonQuery(arr_CmdObj);
        }
        public bool ExecuteNonQuery(SqlCommand sco_CmdObj)
        {
            ArrayList arr_CmdObj = new ArrayList();
            arr_CmdObj.Add(sco_CmdObj);
            return ExecuteNonQuery(arr_CmdObj);
        }

        #endregion


        public bool IsTableExists(string str_DatabaseName, string str_TableName, string str_Type)
        {
            string str_Sql="select 1 from " + str_DatabaseName + "..sysobjects where name='" + str_TableName + "' and type='" + str_Type + "'";
            DataTable dt= this.GetDataTable(str_Sql);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        
        #region SqlUpdate
        /// <summary>
        /// Executes the array of command object passed as parameter 
        /// If Executes returns no of records
        /// </summary>
        /// <param name="sco_CmdObj">Command object</param>
        /// <returns>Integer</returns>
        public ArrayList SqlUpdate(ArrayList arr_CmdObj)
        {
            ArrayList arr_Result = new ArrayList();
            this.DBException = null;
            this.int_rows_affected = 0;
            this.str_db_message = "";

            if (!this.ManagedConnection) this.OpenDatabaseConnection();     
            if (!this.ManagedTransaction) this.TransactionBegin();

            DbCommand db_OpenSymmetric = new SqlCommand("OPEN SYMMETRIC KEY EncryptKey DECRYPTION BY CERTIFICATE EncryptCert");
            DbCommand db_CloseSymmetric = new SqlCommand(" CLOSE SYMMETRIC KEY EncryptKey ");


            try
            {

                if (this.UseSymmetricKey == true)
                {
                    db_OpenSymmetric.Connection = this.DbCon;
                    db_OpenSymmetric.Transaction = this.DbTran;
                    db_OpenSymmetric.ExecuteNonQuery();
                }
                
                for (int i = 0; i < arr_CmdObj.Count; i++)
                {
                    if (arr_CmdObj[i].ToString() == "System.Data.SqlClient.SqlCommand")
                        this.DbComm = (System.Data.SqlClient.SqlCommand)arr_CmdObj[i];
                    else
                        this.DbComm = (DbCommand)arr_CmdObj[i];

                    this.DbComm.CommandTimeout = this.int_CommandTimeout;
                    this.DbComm.Connection = this.DbCon;
                    this.DbComm.Transaction = this.DbTran;

                    this.LW.WriteLog("Sql : " + this.DbComm.CommandText, LogWriter.Severity.Information);
                    arr_Result.Add(this.DbComm.ExecuteNonQuery());
                    this.LW.WriteLog("Affected Records : " + arr_Result[i].ToString(), LogWriter.Severity.Information);
                }

                if (this.UseSymmetricKey == true)
                {
                    db_CloseSymmetric.Connection = this.DbCon;
                    db_CloseSymmetric.Transaction = this.DbTran;
                    db_CloseSymmetric.ExecuteNonQuery();
                }

                if (!this.ManagedTransaction) this.TransactionCommit();
                LW.WriteLog("Transaction Commited", LogWriter.Severity.Information);
            }
            catch (Exception ex)
            {
                this.WriteCmdParameterLog(this.DbComm, LogWriter.Severity.Error);
                this.DBException = ex;
                this.str_db_message = ex.ToString();
                this.LW.WriteLog(ex.ToString(), LogWriter.Severity.Error);
                if (!this.ManagedTransaction) this.TransactionRollback();
            }
            finally
            {
                if (!this.ManagedConnection) this.CloseDatabaseConnection(); 
                this.DbComm.Dispose();

            }
            if (this.DBException != null)throw this.DBException;
            this.int_rows_affected = (int) arr_Result[0];
            this.al_RowsAffected = new ArrayList();
            this.al_RowsAffected = arr_Result;
            return arr_Result;
        }
        #endregion

    }     
