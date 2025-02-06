using System;
using System.Collections.Generic;
using System.Net;
using System.Data;
using System.Xml;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for PhTemplate
/// </summary>
public class PhTemplate
{
    string str_Root = "";
    string str_Culture = "en-US";

    DataSet ds_Data = null;
    System.Globalization.CultureInfo culture;
    AppVar AV;

    string str_XmlData = "";
    string str_LogoutPage = "";

	public PhTemplate()
	{
        this.AV = new AppVar();
        this.str_Root = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "/DivTemplate/";


        //if (this.str_Culture == "") throw new Exception("Culture not defined in Application Variable");
       
        this.culture = new System.Globalization.CultureInfo(str_Culture);
     
	}
    public PhTemplate(string str_language)
    {
        this.AV = new AppVar();
    }

    public System.Globalization.CultureInfo getCulture()
    {
        return new System.Globalization.CultureInfo(this.str_Culture);
    }

    //public string getSessionToken() {

    //    if (this.OverwriteSessionToken == true)
    //    {
    //        if (this.GuestToken == "") throw new Exception("GuestToken must be assigned if OverwriteSessionToken is true");
    //        return this.GuestToken;
    //    }

    //    if (HttpContext.Current.Session["token"] == null) this.RedirectToLoginPage();
    //    return HttpContext.Current.Session["token"].ToString();
    //}

    public void RedirectToLoginPage()
    {
        HttpContext.Current.Response.Redirect("~/" + this.str_LogoutPage);
    }
    public bool OverwriteSessionToken { get; set; }
    public string GuestToken { get; set; }
    public DataSet getDataSet { get { return this.ds_Data; } set { this.ds_Data = value; } }
    public string getXmlData { get { return this.str_XmlData; } set { this.str_XmlData = value; } }
    public bool SkipToken { get; set; }

    public string ReadFileToString(string str_TemplateFileName)
    {
        string str_Filepath = this.str_Root + str_TemplateFileName;
        StreamReader streamReader = new StreamReader(str_Filepath);
        string str_Div = streamReader.ReadToEnd();
        streamReader.Close();

        return str_Div;
    }

    public string ReplaceVariableWithValueForEmail(string str_name, string str_subject,string str_contents,string image,string footer,string str_TemplateTextForEmail)
    {

        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Header%%", str_name);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Header%%", str_name);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Header%%", str_name);

        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%MailSubject%%", str_subject);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%MailSubject%%", str_subject);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%MailSubject%%", str_subject);

        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Content%%", str_contents);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Content%%", str_contents);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Content%%", str_contents);

        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Image%%", image);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Image%%", image);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Image%%", image);

        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Footer%%", footer);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Footer%%", footer);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Footer%%", footer);

        return str_TemplateTextForEmail;
    }
    public string ReplaceVariableWithValueForSendEmails(string str_name, string str_subject, string str_contents, string str_td, string str_TemplateTextForEmail, string str_Projectcatcategory, string str_Admianname)
    {



        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Username%%", str_subject);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Username%%", str_subject);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Username%%", str_subject);

        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Projectname%%", str_contents);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Projectname%%", str_contents);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Projectname%%", str_contents);

        //str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Summary%%", str_tasks);
        //str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Summary%%", str_tasks);
        //str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Summary%%", str_tasks);


        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%taskname%%", str_td);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%taskname%%", str_td);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%taskname%%", str_td);

        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Admianname%%", str_Admianname);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Admianname%%", str_Admianname);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Admianname%%", str_Admianname);

        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Projectcatcategory%%", str_Projectcatcategory);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Projectcatcategory%%", str_Projectcatcategory);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%Projectcatcategory%%", str_Projectcatcategory);


        //str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%attachmentfile%%", str_attachment);
        //str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%attachmentfile%%", str_attachment);
        //str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%attachmentfile%%", str_attachment);


        return str_TemplateTextForEmail;
    }

    public string mailtemplate(string str_username, string str_projectname,string str_category, string str_mailtemplate)
    {

        str_mailtemplate = str_mailtemplate.Replace("%%username%%", str_username);
        str_mailtemplate = str_mailtemplate.Replace("%%username%%", str_username);
        str_mailtemplate = str_mailtemplate.Replace("%%username%%", str_username);

        str_mailtemplate = str_mailtemplate.Replace("%%projectname%%", str_projectname);
        str_mailtemplate = str_mailtemplate.Replace("%%projectname%%", str_projectname);
        str_mailtemplate = str_mailtemplate.Replace("%%projectname%%", str_projectname);

        str_mailtemplate = str_mailtemplate.Replace("%%category%%", str_category);
        str_mailtemplate = str_mailtemplate.Replace("%%category%%", str_category);
        str_mailtemplate = str_mailtemplate.Replace("%%category%%", str_category);

        return str_mailtemplate;
    }

    public string Replacevariableforaddmembers(string str_name, string str_userid, string str_email, string str_TemplateTextForEmail)
    {

        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%name%%", str_name);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%name%%", str_name);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%name%%", str_name);

        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%link%%", str_userid);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%link%%", str_userid);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%link%%", str_userid);

        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%email%%", str_email);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%email%%", str_email);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%email%%", str_email);


        return str_TemplateTextForEmail;
    }


    //public string DownloadDataFromUrl(string str_WebUrl)
    //{
    //    string str_OriginalUrl = str_WebUrl;
    //    WebClient client = new WebClient();
    //    client.Encoding = System.Text.Encoding.UTF8;
    //    str_WebUrl += "&token=" + this.getSessionToken();
    //    string xmlData = client.DownloadString(str_WebUrl);

    //    if (!SkipToken && xmlData.ToString().Contains("authstatus") && xmlData.ToString().Contains("authmessage"))
    //    {
    //        // as token expired, call to get token if session still exists
    //        if (HttpContext.Current.Session["accountrow"] != null)
    //        {
    //            DataRow dr = (DataRow)HttpContext.Current.Session["accountrow"];
    //            HttpContext.Current.Session["token"] = this.GetUserToken(dr["UserName"].ToString(), dr["Password"].ToString());
    //        }
    //        return this.DownloadDataFromUrl(str_OriginalUrl);
    //    }

    //    return xmlData;
    //}

    //public string GetUserToken(string str_UserName, string str_UserPassword)
    //{
    //    //get token key
    //    string str_Token = "";
    //    string str_TokenURL = this.AV.WebRoot + "sys/formtoken.ashx";
    //    SkipToken = true;
    //    string str_ResultXml = this.PostData(str_TokenURL, "uid=" + str_UserName + "&pkey=" + str_UserPassword);
    //    DataSet dsToken = new Cxml().XmlStringToDataSet(str_ResultXml);
    //    if (dsToken.Tables.Count > 0 || dsToken.Tables[0].Rows.Count > 0)
    //    {
    //        str_Token = dsToken.Tables[0].Rows[0]["AuthToken"].ToString();
    //    }

    //    if (str_Token == "") this.RedirectToLoginPage();

    //    return str_Token;
    //}

    //public DataSet GetDataSetFromUrl(string str_WebUrl)
    //{
    //    string xmlData = this.DownloadDataFromUrl(str_WebUrl);
    //    DataSet ds = new Cxml().XmlStringToDataSet(xmlData);
    //    return ds;
    //}

    //public void LoadGridItem(string str_WebUrl, PlaceHolder ph_Grid, string str_ItemTemplateFile)
    //{
    //    this.LoadGridItem(str_WebUrl, ph_Grid, str_ItemTemplateFile, "");
    //}

    //public void LoadGridItem(string str_WebUrl, PlaceHolder ph_Grid, string str_ItemTemplateFile, string str_TableName)
    //{
    //    string xmlData = this.DownloadDataFromUrl(str_WebUrl);
    //    this.str_XmlData = xmlData;
    //    DataSet ds = new Cxml().XmlStringToDataSet(xmlData);
    //    this.LoadGridItem(ds, ph_Grid, str_ItemTemplateFile, str_TableName);
    //}

    public void LoadGridItem(DataSet ds, PlaceHolder ph_Grid, string str_ItemTemplateFile, string str_TableName)
    {
        this.ds_Data = ds;
        string str_Template = new PhTemplate().ReadFileToString(str_ItemTemplateFile);
        string str_TemplateUpdated = "";
        string str_ColumnValue = "";
        Literal li = new Literal();

        if (ds.Tables.Count == 0) return; // throw new Exception("Records not found, Grid load failed");

        DataTable dt_Data;
        if (str_TableName == "") dt_Data = ds.Tables[0];
        else dt_Data = ds.Tables[str_TableName];

        int intRow = 0;
        foreach (DataRow dr in dt_Data.Rows)
        {
            intRow++;
            str_TemplateUpdated = str_Template;

            foreach (DataColumn dc in dt_Data.Columns)
            {
                str_ColumnValue = dr[dc.ColumnName].ToString();
                if (dc.ColumnName.ToLower().EndsWith("url")) str_ColumnValue = this.ConvertToDisplayDate(str_ColumnValue);
                else str_ColumnValue = this.FormatValue(dc,str_ColumnValue);

                str_TemplateUpdated = str_TemplateUpdated.Replace("%%" + dc.ColumnName + "%%", str_ColumnValue);
                str_TemplateUpdated = str_TemplateUpdated.Replace("%%" + dc.ColumnName + "_full%%", str_ColumnValue.Replace("thumb_", ""));
            }
            str_TemplateUpdated = str_TemplateUpdated.Replace("%%autonumber%%", intRow.ToString());

            li = new Literal();
            li.Text = str_TemplateUpdated;
            ph_Grid.Controls.Add(li);
        }
    }
    public void LoadGridItemvideo(DataSet ds, ListView tbl_Grid, string str_ItemTemplateFile, string str_TableName)
    {
        this.ds_Data = ds;
        string str_Template = new PhTemplate().ReadFileToString(str_ItemTemplateFile);
        string str_TemplateUpdated = "";
        string str_ColumnValue = "";
        Label li = new Label();

        if (ds.Tables.Count == 0) return; // throw new Exception("Records not found, Grid load failed");

        DataTable dt_Data;
        if (str_TableName == "") dt_Data = ds.Tables[0];
        else dt_Data = ds.Tables[str_TableName];

        int intRow = 0;
        foreach (DataRow dr in dt_Data.Rows)
        {
            intRow++;
            str_TemplateUpdated = str_Template;

            foreach (DataColumn dc in dt_Data.Columns)
            {
                str_ColumnValue = dr[dc.ColumnName].ToString();
                if (dc.ColumnName.ToLower().EndsWith("Val")) str_ColumnValue = this.ConvertToDisplayDate(str_ColumnValue);
                else if (dc.ColumnName.ToLower().EndsWith("Url")) str_ColumnValue = this.getpath(str_ColumnValue);
                else str_ColumnValue = this.FormatValue(dc, str_ColumnValue);

                str_TemplateUpdated = str_TemplateUpdated.Replace("%%" + dc.ColumnName + "%%", str_ColumnValue);
                str_TemplateUpdated = str_TemplateUpdated.Replace("%%" + dc.ColumnName + "_full%%", str_ColumnValue.Replace("thumb_", ""));
            }
            str_TemplateUpdated = str_TemplateUpdated.Replace("%%autonumber%%", intRow.ToString());

            li = new Label();
            li.Text = str_TemplateUpdated;
            tbl_Grid.Controls.Add(li);
        }
    }

    public string getpath(string str_ColumnValue)
    {
        try
        {
            if (str_ColumnValue != "")
                str_ColumnValue = "" + str_ColumnValue;
            else
                str_ColumnValue = "../Images/no_image-postjob.jpg";

            return str_ColumnValue;
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public void LoadGridItemUsers(DataSet ds, PlaceHolder ph_Grid, string str_ItemTemplateFile, string str_TableName)
    {
        this.ds_Data = ds;
        string str_Template = new PhTemplate().ReadFileToString(str_ItemTemplateFile);
        string str_TemplateUpdated = "";
        string str_ColumnValue = "";
        Literal li = new Literal();

        if (ds.Tables.Count == 0) return; // throw new Exception("Records not found, Grid load failed");

        DataTable dt_Data;
        if (str_TableName == "") dt_Data = ds.Tables[0];
        else dt_Data = ds.Tables[str_TableName];

        int intRow = 0;
        foreach (DataRow dr in dt_Data.Rows)
        {
            intRow++;
            str_TemplateUpdated = str_Template;

            foreach (DataColumn dc in dt_Data.Columns)
            {
                str_ColumnValue = dr[dc.ColumnName].ToString();
                if (dc.ColumnName.ToLower().EndsWith("date")) str_ColumnValue = this.ConvertToDisplayDate(str_ColumnValue);
                else str_ColumnValue = this.FormatValue(dc, str_ColumnValue);

                str_TemplateUpdated = str_TemplateUpdated.Replace("%%" + dc.ColumnName + "%%", str_ColumnValue);
                str_TemplateUpdated = str_TemplateUpdated.Replace("%%" + dc.ColumnName + "_full%%", str_ColumnValue.Replace("thumb_", ""));
            }
            str_TemplateUpdated = str_TemplateUpdated.Replace("%%autonumber%%", intRow.ToString());

            if (dr["Active"].ToString().ToLower() == "true")
                str_TemplateUpdated = str_TemplateUpdated.Replace("%%Active%%", "<span class='label label-sm label-success'>Active</span>");
            else if (dr["Active"].ToString().ToLower() == "false")
                str_TemplateUpdated = str_TemplateUpdated.Replace("%%Active%%", "<span class='label label-sm label-warning'>In Active</span>");
            

            li = new Literal();
            li.Text = str_TemplateUpdated;
            ph_Grid.Controls.Add(li);
        }
    }
    public string ReplaceVariableWithValue(DataRow dr, string str_TemplateText)
    {
        foreach (DataColumn dc in dr.Table.Columns)
        {
            str_TemplateText = str_TemplateText.Replace("%%" + dc.ColumnName + "%%", dr[dc].ToString());
            str_TemplateText = str_TemplateText.Replace("%%" + dc.ColumnName.ToLower() + "%%", dr[dc].ToString());
            str_TemplateText = str_TemplateText.Replace("%%" + dc.ColumnName.ToUpper() + "%%", dr[dc].ToString());
        }

        return str_TemplateText;
    }

    public string FormatValue(DataColumn dc, string str_ColumnValue)
    {
        if (SetCurrencyColumn == null || SetCurrencyColumn.Length == 0 || str_ColumnValue == "") return str_ColumnValue;
        //decimal dec_Value = 0;
       

        foreach (string str in this.SetCurrencyColumn)
        {
            if (dc.ColumnName.ToLower() == str.ToLower())
            {
                try
                {
                    //dec_Value = Convert.ToDecimal(str_ColumnValue);
                    //return "<p align='right'>" + this.AV.CurrencySymbol + " " + string.Format(this.culture, "{0:C}", dec_Value).Replace(this.culture.NumberFormat.CurrencySymbol, "") + "</p>";
                    return this.FormatValue(str_ColumnValue);

                }
                catch (Exception ex)
                {
                    return str_ColumnValue;
                }
            }
        }
        return str_ColumnValue;
    }

    public string FormatValue(string str_ColumnValue)
    {
        decimal dec_Value = 0;

        try
        {
            dec_Value = Convert.ToDecimal(str_ColumnValue);
            return "<p align='right'>" + " " + string.Format(this.culture, "{0:C}", dec_Value).Replace(this.culture.NumberFormat.CurrencySymbol, "") + "</p>";

        }
        catch (Exception ex)
        {
            return str_ColumnValue;
        }
        
    }

    public string[] SetCurrencyColumn { get; set; }

    
    protected string ConvertToDisplayDate(string str_ColumnValue)
    {
        try
        {
            str_ColumnValue = Convert.ToDateTime(str_ColumnValue).ToString(this.culture);
            return str_ColumnValue;
        }
        catch (Exception ex) {
            return "";
        }
    }

    public void FormatDataSetAsPerSchmea (DataSet ds_Data, DataSet ds_Schema)
    {
        foreach (DataTable dt_Data in ds_Data.Tables)
        {
            DataTable dt_Schema = ds_Schema.Tables[dt_Data.TableName];

            foreach (DataColumn dc_Schema in dt_Schema.Columns)
            {
                if (!dt_Data.Columns.Contains(dc_Schema.ColumnName))
                {
                    dt_Data.Columns.Add(dc_Schema.ColumnName);
                }
            }

        }
    }

    //public string PostData(string str_PostDataUrl, string str_PostData)
    //{
    //    string str_OriginalUrl = str_PostDataUrl;
    //    if (!SkipToken) str_PostDataUrl += "?token=" + this.getSessionToken();

    //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(str_PostDataUrl);
    //    request.Method = "POST";
    //    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(str_PostData);
    //    request.ContentType = "application/x-www-form-urlencoded";
    //    request.ContentLength = byteArray.Length;
    //    System.IO.Stream dataStream = request.GetRequestStream();
    //    dataStream.Write(byteArray, 0, byteArray.Length);
    //    dataStream.Close();
    //    WebResponse response = request.GetResponse();
    //    dataStream = response.GetResponseStream();
    //    System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
    //    string responseFromServer = HttpUtility.UrlDecode(reader.ReadToEnd());
    //    reader.Close();
    //    dataStream.Close();
    //    response.Close();

    //    string xmlData = responseFromServer;
    //    if (!SkipToken && xmlData.ToString().Contains("authstatus") && xmlData.ToString().Contains("authmessage"))
    //    {
    //        // as token expired, call to get token if session still exists
    //        if (HttpContext.Current.Session["accountrow"] != null)
    //        {
    //            DataRow dr = (DataRow)HttpContext.Current.Session["accountrow"];
    //            HttpContext.Current.Session["token"] = this.GetUserToken(dr["UserName"].ToString(), dr["Password"].ToString());
    //        }
    //        return this.DownloadDataFromUrl(str_OriginalUrl);
    //    }

    //    return responseFromServer;
    //}
    //public string PostData(string str_PostDataUrl, string str_PostData)
    //{
    //    string str_OriginalUrl = str_PostDataUrl;
    //    if (!SkipToken) str_PostDataUrl += "?token=" + this.getSessionToken();

    //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(str_PostDataUrl);
    //    request.Method = "POST";
    //    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(str_PostData);
    //    request.ContentType = "application/x-www-form-urlencoded";
    //    request.ContentLength = byteArray.Length;
    //    System.IO.Stream dataStream = request.GetRequestStream();
    //    dataStream.Write(byteArray, 0, byteArray.Length);
    //    dataStream.Close();
    //    WebResponse response = request.GetResponse();
    //    dataStream = response.GetResponseStream();
    //    System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
    //    string responseFromServer = HttpUtility.UrlDecode(reader.ReadToEnd());
    //    reader.Close();
    //    dataStream.Close();
    //    response.Close();

    //    string xmlData = responseFromServer;
    //    if (!SkipToken && xmlData.ToString().Contains("authstatus") && xmlData.ToString().Contains("authmessage"))
    //    {
    //        // as token expired, call to get token if session still exists
    //        if (HttpContext.Current.Session["accountrow"] != null)
    //        {
    //            DataRow dr = (DataRow)HttpContext.Current.Session["accountrow"];
    //            HttpContext.Current.Session["token"] = this.GetUserToken(dr["UserName"].ToString(), dr["Password"].ToString());
    //        }
    //        return this.DownloadDataFromUrl(str_OriginalUrl);
    //    }

    //    return responseFromServer;
    //}

    //public void AssignLabelText(string str_WebUrl, Panel pn_Body)
    //{
    //    string xmlData = this.DownloadDataFromUrl(str_WebUrl);

    //    DataSet ds = new Cxml().XmlStringToDataSet(xmlData);
    //    this.ds_Data = ds;
    //    this.str_XmlData = xmlData;

    //    string str_ControlName = "";
    //    Control ctrl;
    //    Literal li = new Literal();
    //    foreach (DataRow dr in ds.Tables[0].Rows)  //mostly one row
    //    {
    //        foreach (DataColumn dc in ds.Tables[0].Columns)
    //        {
    //            string str_ColumnValue = dr[dc.ColumnName].ToString();
    //            if (dc.ColumnName.ToLower().EndsWith("date")) str_ColumnValue = this.ConvertToDisplayDate(str_ColumnValue);
    //            else str_ColumnValue = this.FormatValue(dc, str_ColumnValue);
    //            str_ControlName = "label_" + dc.ColumnName;
    //            ctrl = pn_Body.FindControl(str_ControlName);

    //            if (ctrl!=null && ctrl.ToString() == "System.Web.UI.WebControls.Label")
    //            {
    //                Label lbl = (Label)ctrl;
    //                lbl.Text = this.FormatValue(dc, str_ColumnValue); //  dr[dc.ColumnName].ToString();
    //            }
    //            else if (ctrl != null && ctrl.ToString() == "System.Web.UI.WebControls.Image")
    //            {
    //                Image lbl = (Image)ctrl;
    //                lbl.ImageUrl = str_ColumnValue;// dr[dc.ColumnName].ToString();
    //            }


    //            //for full image
    //            str_ControlName = "label_" + dc.ColumnName + "_full";
    //            ctrl = pn_Body.FindControl(str_ControlName);
    //            if (ctrl != null && ctrl.ToString() == "System.Web.UI.WebControls.Image")
    //            {
    //                Image lbl = (Image)ctrl;
    //                lbl.ImageUrl = str_ColumnValue.Replace("thumb_", "");
    //            }
    //        }
    //    }
    //}
    internal string SupportEmailsend(string str_name, string str_shiment, string str_support, string str_TemplateTextForEmail)
    {
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%name%%", str_name);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%name%%", str_name);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%name%%", str_name);

        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%link%%", str_support);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%link%%", str_support);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%link%%", str_support);

        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%studentname%%", str_shiment);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%studentname%%", str_shiment);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%studentname%%", str_shiment);


        return str_TemplateTextForEmail;
    }

    internal string ReplaceVariableWithValueForEmailsend(string str_name, string str_userid, string str_TemplateTextForEmail)
    {
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%name%%", str_name);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%name%%", str_name);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%name%%", str_name);

        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%link%%", str_userid);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%link%%", str_userid);
        str_TemplateTextForEmail = str_TemplateTextForEmail.Replace("%%link%%", str_userid);

        return str_TemplateTextForEmail;
    }
}