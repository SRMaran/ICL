using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for sqlconnection
/// </summary>
public class sqlconnection
{
    public sqlconnection()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private static string ConnectionString = @"Data Source = 192.168.0.117 ; Initial Catalog = Info_warehouse; User ID = ajview; Password = aj$%^World@123";
    // private static string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    public static SqlConnection GetConnection()
    {
        var con = new SqlConnection(ConnectionString);
        con.Open();

        return con;
    }
    
}