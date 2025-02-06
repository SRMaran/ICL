using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
/// <summary>
/// <summary>
/// Summary description for SessionCustom
/// </summary>
public class SessionCustom
{
    DataTable dt_Company = null;
    DataTable dt_Menu = null;
    DataTable dt_QuickAccess = null;
    DataTable dt_Provider = null;
    DataTable dt_Empmanager = null;


    public SessionCustom()
    {

    }

    public SessionCustom(bool bln_SessionCheckRequired)
    {
        if (HttpContext.Current.Session["userid"] == null)
            this.NavigateToLoginPage();
    }

    public string GetUserid()
    {
        return Userid;
    }


    public string BaseUrl()
    {
        return HttpContext.Current.Session["httproot"].ToString();
    }

    public void NavigateToLoginPage()
    {
        HttpContext.Current.Response.Redirect("~/Web/Default.aspx");
    }

    public string Userid
    {
        get { return HttpContext.Current.Session["userid"].ToString(); }
        set { HttpContext.Current.Session["userid"] = value; }
    }
    public string Userdesg
    {
        get { return HttpContext.Current.Session["Destination"].ToString(); }
        set { HttpContext.Current.Session["Destination"] = value; }
    }
    public string AccKey
    {
        get { return HttpContext.Current.Session["acckey"].ToString(); }
        set { HttpContext.Current.Session["acckey"] = value; }
    }
    public string LogKey
    {
        get { return HttpContext.Current.Session["logkey"].ToString(); }
        set { HttpContext.Current.Session["logkey"] = value; }
    }
    public string username
    {
        get { return HttpContext.Current.Session["Username"].ToString(); }
        set { HttpContext.Current.Session["Username"] = value; }
    }
    public string UserImage
    {
        get { return HttpContext.Current.Session["UserImage"].ToString(); }
        set { HttpContext.Current.Session["UserImage"] = value; }
    }

    public DataRow UserRecord
    {
        get { return (DataRow)HttpContext.Current.Session["userrecord"]; }
        set { HttpContext.Current.Session["userrecord"] = value; }
    }
    public DataTable UserRecordTable
    {
        get { return (DataTable)HttpContext.Current.Session["userrecordtable"]; }
        set { HttpContext.Current.Session["userrecordtable"] = value; }
    }

    public string UserRole
    {
        get { return HttpContext.Current.Session["Roles"].ToString(); }
        set { HttpContext.Current.Session["Roles"] = value; }
    }

    public string Orgname
    {
        get { return HttpContext.Current.Session["Orgnames"].ToString(); }
        set { HttpContext.Current.Session["Orgnames"] = value; }
    } public string Org_names
    {
        get { return HttpContext.Current.Session["org_names"].ToString(); }
        set { HttpContext.Current.Session["org_names"] = value; }
    }
    public string Orgnamecode
    {
        get { return HttpContext.Current.Session["Orgnamecode"].ToString(); }
        set { HttpContext.Current.Session["Orgnamecode"] = value; }
    }
    public string CompanyID
    {
        get
        {
            if (HttpContext.Current.Session["Companyid"] != null)
                return HttpContext.Current.Session["Companyid"].ToString();
            else return string.Empty;
        }
        set { HttpContext.Current.Session["Companyid"] = value; }
    }

    public string GroupId
    {
        get { return HttpContext.Current.Session["NGL_GroupName"].ToString(); }
        set { HttpContext.Current.Session["NGL_GroupName"] = value; }
    }
    public string lablename
    {
        get { return HttpContext.Current.Session["lablename"].ToString(); }
        set { HttpContext.Current.Session["lablename"] = value; }
    }
    public string headername
    {
        get { return HttpContext.Current.Session["headername"].ToString(); }
        set { HttpContext.Current.Session["headername"] = value; }
    }
    public string Name
    {
        get { return HttpContext.Current.Session["Name"].ToString(); }
        set { HttpContext.Current.Session["Name"] = value; }
    }

    public bool SessionExists()
    {
        if (HttpContext.Current.Session["userid"] == null || HttpContext.Current.Session["userid"] == "")
            return false;
        else
            return true;
    }
}

