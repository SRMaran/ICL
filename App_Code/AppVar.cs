using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for AppVar
/// </summary>
public class AppVar
{
	public AppVar()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string DatabaseConnectionString { get {
       return HttpContext.Current.Application["dbconnect"].ToString();
        //return "Data Source=192.168.0.111;Initial Catalog=Performance_Dashboard;User ID=sa;Password=Admin!@#app";

    } }

    public string ImportDatabaseConnectionString { get { return HttpContext.Current.Application["importconnect"].ToString(); } }

    public string WebRoot { get { return HttpContext.Current.Application["webroot"].ToString(); } }

    public string VidapayApiKey { get { return "PRE121214"; } }


    public string email { get { return HttpContext.Current.Application["email"].ToString(); } }

    public string emaildisplayname { get { return HttpContext.Current.Application["emaildisplayname"].ToString(); } }

}
