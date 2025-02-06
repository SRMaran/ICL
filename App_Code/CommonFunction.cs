using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net.Mime;
using System.Activities;
using System.Web.Script.Serialization;
using System.Net;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Activities.Expressions;


/// <summary>
/// Summary description for CommonFunction
/// </summary>
public class CommonFunction
{
    DataAccess DA;
    PhTemplate Ph;
    public CommonFunction()
    {
        this.DA = new DataAccess();
        this.Ph = new PhTemplate();
    }

    public string PostData(string str_PostDataUrl, string str_PostData)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:4646/Forex/sys/formmail.ashx");
        request.Method = "POST";
        byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(str_PostData);
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = byteArray.Length;
        System.IO.Stream dataStream = request.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();
        WebResponse response = request.GetResponse();
        dataStream = response.GetResponseStream();
        System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
        string responseFromServer = HttpUtility.UrlDecode(reader.ReadToEnd());
        reader.Close();
        dataStream.Close();
        response.Close();
        return responseFromServer;
    }
    public DateTime GetIndianDateTime()
    {
        TimeZoneInfo Indian_Zone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        DateTime dt_IndianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Indian_Zone);
        return dt_IndianTime;
    }


    public string GetIPAddress()
    {
        try
        {
            string externalIP;
            externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
            externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                         .Matches(externalIP)[0].ToString();
            return externalIP;
        }
        catch { return null; }

    }
    public string SendMailToEmployee(string str_UserName, string str_Password, string str_FirstName)
    {
        string str_Response = "0";
        try
        {
            AppVar AV = new AppVar();
            string str_LoginURL = AV.WebRoot + "Web/Default.aspx";
            string str_EmailStatus = this.PostData(AV.WebRoot + "sys/formmail.ashx", "mailkey=3CC81FCE-D6B4-452E-A8F9-912AB6EDEFC7&email=" + str_UserName + "&p1=" + HttpContext.Current.Server.UrlEncode(HttpUtility.HtmlEncode(str_FirstName)) + "&p2=" + HttpContext.Current.Server.UrlEncode(HttpUtility.HtmlEncode(str_UserName)) + "&p3=" + HttpContext.Current.Server.UrlEncode(HttpUtility.HtmlEncode(str_Password)) + "&p4=" + HttpContext.Current.Server.UrlEncode(HttpUtility.HtmlEncode(str_LoginURL)));
            if (str_EmailStatus == "1") str_Response = "1";
            else str_Response = "0";

            return str_Response;
        }
        catch (Exception ex)
        {
            return str_Response;
        }

    }

    public void LoadMonthToDropdown(System.Web.UI.WebControls.DropDownList ddl_month)
    {
        ddl_month.Items.Add(new System.Web.UI.WebControls.ListItem("January", "1"));
        ddl_month.Items.Add(new System.Web.UI.WebControls.ListItem("February", "2"));
        ddl_month.Items.Add(new System.Web.UI.WebControls.ListItem("March", "3"));
        ddl_month.Items.Add(new System.Web.UI.WebControls.ListItem("April", "4"));
        ddl_month.Items.Add(new System.Web.UI.WebControls.ListItem("May", "5"));
        ddl_month.Items.Add(new System.Web.UI.WebControls.ListItem("June", "6"));
        ddl_month.Items.Add(new System.Web.UI.WebControls.ListItem("July", "7"));
        ddl_month.Items.Add(new System.Web.UI.WebControls.ListItem("August", "8"));
        ddl_month.Items.Add(new System.Web.UI.WebControls.ListItem("September", "9"));
        ddl_month.Items.Add(new System.Web.UI.WebControls.ListItem("October", "10"));
        ddl_month.Items.Add(new System.Web.UI.WebControls.ListItem("November", "11"));
        ddl_month.Items.Add(new System.Web.UI.WebControls.ListItem("December", "12"));
    }
    public void LoadDateToDropdown(System.Web.UI.WebControls.DropDownList ddl_date)
    {
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("1", "1"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("2", "2"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("3", "3"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("4", "4"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("5", "5"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("6", "6"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("7", "7"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("8", "8"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("8", "9"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("10", "10"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("11", "11"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("12", "12"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("13", "13"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("14", "14 "));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("15", "15"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("16", "16"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("17", "17"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("18", "18"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("20", "20"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("21", "21"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("22", "22"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("23", "23"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("24", "24"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("25", "25"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("26", "26"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("27", "27"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("28", "28"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("29", "29"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("30", "30"));
        ddl_date.Items.Add(new System.Web.UI.WebControls.ListItem("31", "31"));
    }

    public void LoadYearToDropdown(System.Web.UI.WebControls.DropDownList ddl_year)
    {

        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1975", "1975"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1976", "1976"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1977", "1977"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1978", "1978"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1979", "1979"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1980", "1980"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1981", "1981"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1982", "1982"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1983", "1983"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1984", "1984"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1985", "1985"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1986", "1986"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1987", "1987"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1988", "1988"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1989", "1989"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1990", "1990"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1991", "1991"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1992", "1992"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1993", "1993"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1994", "1994"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1995", "1995"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1996", "1996"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1997", "1997"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1998", "1998"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("1999", "1999"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("2000", "2000"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("2001", "2001"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("2002", "2002"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("2003", "2003"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("2004", "2004"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("2005", "2005"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("2006", "2006"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("2007", "2007"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("2008", "2008"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("2009", "2009"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("2010", "2010"));
        //ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("2011", "2011"));
        ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("2018", "2018"));
        ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("2019", "2019"));
        ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("2020", "2020"));
        ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("2021", "2021"));
        ddl_year.Items.Add(new System.Web.UI.WebControls.ListItem("2022", "2012"));
    }

    public SqlCommand CreateLogKey(string str_UserKey)
    {
        DateTime now = DateTime.UtcNow;
        var timenow = now;
        string str_Sql = "insert into  WD_Logdetail(WD_Createdon,WD_createdby) values (@CreatedOn,@CreatedBy)";
        SqlCommand sc = new SqlCommand(str_Sql);
        sc.Parameters.AddWithValue("@CreatedBy", str_UserKey);
        sc.Parameters.AddWithValue("@CreatedOn", timenow.ToString("yyyy-MM-dd HH:mm:ss"));
        return sc;
    }


    public DateTime currentdatetime(string str_utc)
    {


        DateTime dt_utc = Convert.ToDateTime(str_utc);

        CultureInfo ci = new CultureInfo("en-NZ");
        string date = dt_utc.ToString("R", ci);
        DateTime convertedDate = DateTime.Parse(date);
        var Request = HttpContext.Current.Request;
        string final = Request.ToString();
        // string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //if (string.IsNullOrEmpty(ipAddress))
        //{
        //    ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        //}
        //ipAddress = "13.126.196.138";
        //string APIKey = "327ae8f9e5a9e8340bbeebcbaa5637777cf712a69c74a817fc15dfd5c6285dbe";
        //string url = string.Format("http://api.ipinfodb.com/v3/ip-city/?key={0}&ip={1}&format=json", APIKey, ipAddress);
        //using (WebClient client = new WebClient())
        //{
        //    string json = client.DownloadString(url);
        //    Location location = new JavaScriptSerializer().Deserialize<Location>(json);
        //    List<Location> locations = new List<Location>();
        //    locations.Add(location);
        //    string str_timezone = location.TimeZone;
        //  //  DateTimeOffset result = DateTimeOffset.Parse(str_timezone, CultureInfo.InvariantCulture);
        //  //  DateTimeOffset result =DateTimeOffset.TryParse(str_timezone, out result)
        //  //  var offset = DateTimeOffset.ParseExact(offsetString);
        //    DateTimeOffset offset = dt_utc;
        //    if (!DateTimeOffset.TryParse(str_timezone, out offset))
        //    {
        //        offset = DateTimeOffset.Now;
        //    }

        //   // TimeSpan ts = dt_utc - offset;
        //    DateTime dt_final = offset.DateTime;
        return convertedDate;

    }
    public class Location
    {
        public string IPAddress { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string TimeZone { get; set; }
    }

    public class Menu
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
    }
    public class SubMenu
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string SubMenuName { get; set; }
        public string SubMenuUrl { get; set; }
    }
    public class ChildSubMenu
    {
        public int SubParentId { get; set; }
        public string ChildSubMenuName { get; set; }
        public string ChildSubMenuUrl { get; set; }
    }

    public string Supportemail(string str_email, string type, string str_subject, string str_shipment, string str_name, string str_support)
    {
        string str_msg = "success";
        string str_template = "";
        if (type == "ShipSupportMail")
        {
            str_template = "ShipSupportMail.txt";
        }
        if (type == "ContainerSupportMail")
        {
            str_template = "ContainerSupportMail.txt";
        }
        if (type == "BookingSupportMail")
        {
            str_template = "BookingSupportMail.txt";
        }
        if (type == "QuotesSupportMail")
        {
            str_template = "QuotesSupportMail.txt";
        }
        if (type == "CustomSupportMail")
        {
            str_template = "CustomSupportMail.txt";
        }
        try
        {

            SmtpClient smtpClient = new SmtpClient();
            SmtpClient smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = "smtp.office365.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential("iclhub@iclgo.com", "r0PXu5Fg");
                smtp.Timeout = 20000;
            }
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("iclhub@iclgo.com", "Olivia Coleman");
            mail.To.Add(new MailAddress(str_email));
            //mail.CC.Add(new MailAddress(str_email));
            string subject = str_subject;
            string body = Ph.ReadFileToString(str_template);
            body = Ph.SupportEmailsend(str_name, str_shipment, str_support, body);
            mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html));
            mail.IsBodyHtml = true;
            mail.Subject = str_subject;
            smtp.Send(mail);
            str_msg = "success";
        }
        catch (Exception ex)
        {
            str_msg = ex.Message;
        }
        return str_msg;
    }
    public string PasswordRecovery(string str_email, string type, string str_subject, string str_link, string str_firstname)
    {
        string str_msg = "success";
        string str_template = "";

        if (type == "password")
        {

            str_template = "Passwordrecover.txt";
        }
        if (type == "Createpassword")
        {

            str_template = "CreatePassword.txt";
        }

        SmtpClient smtpClient = new SmtpClient();

        var smtp = new System.Net.Mail.SmtpClient();
        {
            
            //smtp.Host = "smtp.gmail.com";
            smtp.Host = "smtp.office365.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential("iclhub@iclgo.com", "r0PXu5Fg");
            smtp.Timeout = 20000;
        }
        MailMessage mail = new MailMessage();

        //Setting From , To and CC
        mail.From = new MailAddress("iclhub@iclgo.com", "Olivia Coleman");
        mail.To.Add(new MailAddress(str_email));
        string subject = str_subject;
        string body = this.Ph.ReadFileToString(str_template);
        body = this.Ph.ReplaceVariableWithValueForEmailsend(str_firstname, str_link, body);
        mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html));
        mail.IsBodyHtml = true;
        mail.Subject = subject;

        smtp.Send(mail);

        return str_msg;
    }
    public string getMonthscgl(int crrMonth, int crrYear, string columnname)
    {
        string currentYearFilter = "";
        string pastYearFilter = "";
        string filter_month = "";
        if (crrMonth > 0)
        {

            for (int i = 0; i < 12; i++)
            {
                string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames;
                string month_text = monthNames[i];
                int month_val = i + 1;
                int year_value = 0;
                if (month_val != crrMonth)
                {
                    if (month_val > crrMonth)
                    {
                        year_value = crrYear - 1;
                        pastYearFilter += "SUM(CASE WHEN YEAR(" + columnname + ")= " + year_value + " and MONTH(" + columnname + ") = " + month_val + " THEN 1 ELSE 0 END) AS '" + month_text + " " + year_value + "',";
                    }
                    else
                    {
                        year_value = crrYear;
                        currentYearFilter += "SUM(CASE WHEN YEAR(" + columnname + ")= " + year_value + " and MONTH(" + columnname + ") = " + month_val + " THEN 1 ELSE 0 END) AS '" + month_text + " " + year_value + "',";
                    }
                }

            }
        }
        filter_month = pastYearFilter + currentYearFilter;
        filter_month = filter_month.Length == 0 ? "SUM(CASE WHEN MONTH(" + columnname + ") = 1 THEN 1 ELSE 0 END) AS Jan," : filter_month;
        return filter_month;
    }

    public string getMonthsname(int crrMonth, int crrYear = 0)
    {
        string currentYearFilter = "";
        string pastYearFilter = "";
        string filter_month = "";
        if (crrMonth > 0)
        {
            for (int i = 11; i >= 0; i--)
            {


                string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames;
                string month_text = monthNames[i];
                int month_val = i + 1;
                int year_value = 0;

                if (month_val > crrMonth)
                {
                    year_value = crrYear - 1;
                    pastYearFilter += "'" + month_text + " " + year_value + "',";
                }
                else
                {
                    year_value = crrYear;
                    currentYearFilter += "'" + month_text + " " + year_value + "',";
                }


            }
        }
        filter_month = currentYearFilter + pastYearFilter;
         filter_month = filter_month.TrimEnd(',');

        string[] months = filter_month.Split(',');

        Array.Reverse(months);

        filter_month = string.Join(",", months);


        return filter_month;
    }
    public string getMonths(int crrMonth, int crrYear = 0)
    {
        string currentYearFilter = "";
        string pastYearFilter = "";
        string filter_month = "";
        if (crrMonth > 0)
        {
            for (int i = 11; i >= 0; i--)
            {


                string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames;
                string month_text = monthNames[i];
                int month_val = i + 1;
                int year_value = 0;

                if (month_val > crrMonth)
                {
                    year_value = crrYear - 1;
                    pastYearFilter += "SUM(CASE WHEN YEAR(a.spr_originetd)= " + year_value + " and MONTH(a.spr_originetd) = " + month_val + " THEN 1 ELSE 0 END) AS '" + month_text + " " + year_value + "',";
                }
                else
                {
                    year_value = crrYear;
                    currentYearFilter += "SUM(CASE WHEN YEAR(a.spr_originetd)= " + year_value + " and MONTH(a.spr_originetd) = " + month_val + " THEN 1 ELSE 0 END) AS '" + month_text + " " + year_value + "',";
                }


            }
        }
        filter_month = currentYearFilter + pastYearFilter;
        filter_month = filter_month.Length == 0 ? "SUM(CASE WHEN MONTH(a.spr_originetd) = 1 THEN 1 ELSE 0 END) AS Jan," : filter_month;
        filter_month = filter_month.TrimEnd(',');
        return filter_month;
    }
    public string getTotal(int crrMonth, int crrYear = 0)
    {
        string pastYearFilter = "";
        string Totalvalues = "";
        DateTime currentDate = DateTime.Now;
        DateTime elevenMonthsAgo = currentDate.AddMonths(-11);
        int PrevMonth = elevenMonthsAgo.Month;
        int PrevYear = elevenMonthsAgo.Year;
        int CurMonth = currentDate.Month;
        int CurYear = currentDate.Year;
        if (PrevMonth > CurMonth)
        {
            pastYearFilter = "SUM(CASE WHEN (YEAR(ShipmentDate) = " + PrevYear + " AND MONTH(ShipmentDate) >= " + PrevMonth + ") OR (YEAR(ShipmentDate) = " + CurYear + " AND MONTH(ShipmentDate) <= " + CurMonth + ") THEN 1 ELSE 0 END) AS Total";
        }
        Totalvalues = pastYearFilter;
        Totalvalues = Totalvalues.Length == 0 ? " SUM(CASE WHEN (YEAR(ShipmentDate) = " + PrevYear + " AND MONTH(ShipmentDate) >= " + PrevMonth + ") OR (YEAR(ShipmentDate) = " + CurYear + " AND MONTH(ShipmentDate) <= " + CurMonth + ") THEN 1 ELSE 0 END) AS Total" : Totalvalues;
        return Totalvalues;
    }
    public string getDESC(int crrMonth, int crrYear = 0)
    {
        string pastYearFilter = "";
        string Totalvalues = "";
        DateTime currentDate = DateTime.Now;
        DateTime elevenMonthsAgo = currentDate.AddMonths(-11);
        int PrevMonth = elevenMonthsAgo.Month;
        int PrevYear = elevenMonthsAgo.Year;
        int CurMonth = currentDate.Month;
        int CurYear = currentDate.Year;
        if (PrevMonth > CurMonth)
        {
            pastYearFilter = "SUM(CASE WHEN (YEAR(ShipmentDate) = " + PrevYear + " AND MONTH(ShipmentDate) >= " + PrevMonth + ") OR (YEAR(ShipmentDate) = " + CurYear + " AND MONTH(ShipmentDate) <= " + CurMonth + ") THEN 1 ELSE 0 END)";
        }
        Totalvalues = pastYearFilter;
        Totalvalues = Totalvalues.Length == 0 ? " SUM(CASE WHEN (YEAR(ShipmentDate) = " + PrevYear + " AND MONTH(ShipmentDate) >= " + PrevMonth + ") OR (YEAR(ShipmentDate) = " + CurYear + " AND MONTH(ShipmentDate) <= " + CurMonth + ") THEN 1 ELSE 0 END)" : Totalvalues;
        return Totalvalues;
    }
    public string getMonthName(int crrMonth)
    {
        string filter_month = "";
        if (crrMonth > 0)
        {
            crrMonth -= 1;
            string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames;
            filter_month = monthNames[crrMonth];
        }
        return filter_month;
    }
    public string getMonth(int crrMonth, int actualMonth)
    {
        DateTime currentDate = DateTime.Now;
        int crrYear = currentDate.Year;

        string filter_month = "";
        if (actualMonth > 0)
        {
            string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames;

            for (int i = 0; i < actualMonth; i++)
            {
                string month_text = monthNames[i];
                int month_val = i + 1;
                if ((actualMonth != crrMonth) || (month_val != actualMonth))
                {
                    filter_month += "SUM(CASE WHEN YEAR(ShipmentDate)= " + crrYear + " and MONTH(ShipmentDate) = " + month_val + " THEN 1 ELSE 0 END) AS '" + month_text + " " + crrYear + "',";
                }
                else
                {
                    filter_month += "SUM(CASE WHEN YEAR(ShipmentDate)= " + crrYear + " and MONTH(ShipmentDate) = " + month_val + " THEN 1 ELSE 0 END) AS '" + month_text + " " + crrYear + "',";
                }
            }
        }
        filter_month = filter_month.Length == 0 ? "SUM(CASE WHEN MONTH(ShipmentDate) = 1 THEN 1 ELSE 0 END) AS Jan," : filter_month;
        return filter_month;
    }
    public string getMonthall(int crrMonth, int crrYear, string pyear)
    {
        string currentYearFilter = "";
        string pastYearFilter = "";
        string filter_month = "";

        int yearParsed = int.Parse(pyear);


        for (int i = 0; i < 12; i++)
        {
            int[] monthNumbers = Enumerable.Range(1, 12).ToArray();
            string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames;
            string month_text = monthNames[i];
            int month_val = monthNumbers[i];
            pastYearFilter += "SUM(CASE WHEN YEAR(ShipmentDate)= " + yearParsed + " and MONTH(ShipmentDate) = " + month_val + " THEN 1 ELSE 0 END) AS '" + month_text + " " + yearParsed + "',";

        }
        filter_month += pastYearFilter;
        filter_month = filter_month.Length == 0 ? "SUM(CASE WHEN MONTH(ShipmentDate) = 1 THEN 1 ELSE 0 END) AS Jan," : filter_month;
        return filter_month;
    }
    public string GetPrevYearMonth(int crrMonth, int crrYear = 0)
    {
        string pastYearFilter = "";

        string Totalvalues = "";

        DateTime currentDate = DateTime.Now;
        DateTime elevenMonthsAgo = currentDate.AddMonths(-11);
        int PrevMonth = elevenMonthsAgo.Month;
        int PrevYear = elevenMonthsAgo.Year;

        int CurMonth = currentDate.Month;
        int CurYear = currentDate.Year;


        if (PrevMonth > CurMonth)
        {
            pastYearFilter = " OR (YEAR(ShipmentDate) = " + PrevYear + " AND MONTH(ShipmentDate) >= " + PrevMonth + ")";
        }



        Totalvalues = pastYearFilter;
        Totalvalues = Totalvalues.Length == 0 ? " OR (YEAR(ShipmentDate) = " + PrevYear + " AND MONTH(ShipmentDate) >= " + PrevMonth + ")" : Totalvalues;


        return Totalvalues;
    }
    public string getCurrentMonthWeekFiter(int day, string monthName)
    {
        string week = "";
        if (day > 0 && monthName != "")
        {
            if (day > 24 && day <= 31)
            {
                week += "SUM(CASE WHEN YEAR(ShipmentDate) = YEAR(GETDATE()) AND MONTH(ShipmentDate) = MONTH(GETDATE()) AND(DATEPART(DAY, ShipmentDate) - 1) / 7 + 1 = 5 THEN 1 ELSE 0 END) AS '" + monthName + " 5th Week',";
                //week += " SUM((CASE WHEN MONTH(ShipmentDate) = MONTH(GETDATE()) AND ABS(DATEDIFF(DAY, ShipmentDate, GETDATE())) > 24 AND ABS(DATEDIFF(DAY, ShipmentDate, GETDATE())) <= 31 THEN 1 else 0 END))  AS '" + monthName + " 5th week',";

            }
            if (day > 18 && day <= 24 || 24 < day)
            {
                week += "SUM(CASE WHEN YEAR(ShipmentDate) = YEAR(GETDATE()) AND MONTH(ShipmentDate) = MONTH(GETDATE()) AND(DATEPART(DAY, ShipmentDate) - 1) / 7 + 1 = 4 THEN 1 ELSE 0 END) AS '" + monthName + " 4th Week',";
                //week += " SUM((CASE WHEN MONTH(ShipmentDate) = MONTH(GETDATE()) AND ABS(DATEDIFF(DAY, ShipmentDate, GETDATE())) > 18 AND ABS(DATEDIFF(DAY, ShipmentDate, GETDATE())) <= 24 THEN 1 else 0 END))  AS '" + monthName + " 4th week',";

            }
            if (day > 12 && day <= 18 || 18 < day)
            {
                week += "SUM(CASE WHEN YEAR(ShipmentDate) = YEAR(GETDATE()) AND MONTH(ShipmentDate) = MONTH(GETDATE()) AND(DATEPART(DAY, ShipmentDate) - 1) / 7 + 1 = 3 THEN 1 ELSE 0 END) AS '" + monthName + " 3rd Week',";
                // week += " SUM((CASE WHEN MONTH(ShipmentDate) = MONTH(GETDATE()) AND ABS(DATEDIFF(DAY, ShipmentDate, GETDATE())) > 12 AND ABS(DATEDIFF(DAY, ShipmentDate, GETDATE())) <= 18 THEN 1 else 0 END))  AS '" + monthName + " 3rd Week',";

            }
            if (day > 6 && day <= 12 || 12 < day)
            {
                week += "SUM(CASE WHEN YEAR(ShipmentDate) = YEAR(GETDATE()) AND MONTH(ShipmentDate) = MONTH(GETDATE()) AND(DATEPART(DAY, ShipmentDate) - 1) / 7 + 1 = 2 THEN 1 ELSE 0 END) AS '" + monthName + " 2nd Week',";
                //week += " SUM((CASE WHEN MONTH(ShipmentDate) = MONTH(GETDATE()) AND ABS(DATEDIFF(DAY, ShipmentDate, GETDATE())) > 6 AND ABS(DATEDIFF(DAY, ShipmentDate, GETDATE())) <= 12 THEN 1 else 0 END))  AS '" + monthName + " 2nd Week',";

            }
            if (day <= 6 || 6 < day)
            {
                week += "SUM(CASE WHEN YEAR(ShipmentDate) = YEAR(GETDATE()) AND MONTH(ShipmentDate) = MONTH(GETDATE()) AND(DATEPART(DAY, ShipmentDate) - 1) / 7 + 1 = 1 THEN 1 ELSE 0 END) AS '" + monthName + " 1st Week',";
                //week += " SUM(CASE WHEN YEAR(ShipmentDate) = YEAR(GETDATE()) AND MONTH(ShipmentDate) = MONTH(GETDATE()) AND(DATEPART(DAY, ShipmentDate) - 1) / 7 + 1 <= 6 THEN 1 ELSE 0 END) AS '" + monthName +  " 1st Week',";
            }
        }
        return week;
    }
    public string getMonthNamecgl(int crrMonth)
    {
        string filter_month = "";
        if (crrMonth > 0)
        {
            crrMonth -= 1;
            string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames;
            filter_month = monthNames[crrMonth];
        }
        return filter_month;
    }
    public string getMonthallcgl(int crrMonth, int crrYear, string pyear)
    {
        string currentYearFilter = "";
        string pastYearFilter = "";
        string filter_month = "";

        int yearParsed = int.Parse(pyear);


        for (int i = 0; i < 12; i++)
        {
            int[] monthNumbers = Enumerable.Range(1, 12).ToArray();
            string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames;
            string month_text = monthNames[i];
            int month_val = monthNumbers[i];
            pastYearFilter += "SUM(CASE WHEN YEAR(JobDate)= " + yearParsed + " and MONTH(JobDate) = " + month_val + " THEN 1 ELSE 0 END) AS '" + month_text + " " + yearParsed + "',";

        }
        filter_month += pastYearFilter;
        filter_month = filter_month.Length == 0 ? "SUM(CASE WHEN MONTH(JobDate) = 1 THEN 1 ELSE 0 END) AS Jan," : filter_month;
        return filter_month;



    }
    public string getCurrentMonthWeekFitercgl(int day, string monthName)
    {
        string week = "";
        if (day > 0 && monthName != "")
        {
            if (day <= 6 || 6 < day)
            {
                week += "SUM(CASE WHEN YEAR(ShipmentDate) = YEAR(GETDATE()) AND MONTH(ShipmentDate) = MONTH(GETDATE()) AND(DATEPART(DAY, ShipmentDate) - 1) / 7 + 1 = 1 THEN 1 ELSE 0 END) AS '" + monthName + " 1st Week',";
                //week += "SUM((CASE WHEN MONTH(JobDate) = MONTH(GETDATE()) AND ABS(DATEDIFF(DAY, JobDate, GETDATE())) <= 6 THEN 1 else 0 END))  AS '" + monthName + " 1st Week',";
            }
            if (day > 6 && day <= 12 || 12 < day)
            {
                week += "SUM(CASE WHEN YEAR(ShipmentDate) = YEAR(GETDATE()) AND MONTH(ShipmentDate) = MONTH(GETDATE()) AND(DATEPART(DAY, ShipmentDate) - 1) / 7 + 1 = 2 THEN 1 ELSE 0 END) AS '" + monthName + " 2nd Week',";
                //week += " SUM((CASE WHEN MONTH(JobDate) = MONTH(GETDATE()) AND ABS(DATEDIFF(DAY, JobDate, GETDATE())) > 6 AND ABS(DATEDIFF(DAY, JobDate, GETDATE())) <= 12 THEN 1 else 0 END))  AS '" + monthName + " 2nd Week',";

            }
            if (day > 12 && day <= 18 || 18 < day)
            {
                week += "SUM(CASE WHEN YEAR(ShipmentDate) = YEAR(GETDATE()) AND MONTH(ShipmentDate) = MONTH(GETDATE()) AND(DATEPART(DAY, ShipmentDate) - 1) / 7 + 1 = 3 THEN 1 ELSE 0 END) AS '" + monthName + " 3rd Week',";
                //week += " SUM((CASE WHEN MONTH(JobDate) = MONTH(GETDATE()) AND ABS(DATEDIFF(DAY, JobDate, GETDATE())) > 12 AND ABS(DATEDIFF(DAY, JobDate, GETDATE())) <= 18 THEN 1 else 0 END))  AS '" + monthName + " 3rd Week',";

            }
            if (day > 18 && day <= 24 || 24 < day)
            {
                week += "SUM(CASE WHEN YEAR(ShipmentDate) = YEAR(GETDATE()) AND MONTH(ShipmentDate) = MONTH(GETDATE()) AND(DATEPART(DAY, ShipmentDate) - 1) / 7 + 1 = 4 THEN 1 ELSE 0 END) AS '" + monthName + " 4th Week',";
                //week += " SUM((CASE WHEN MONTH(JobDate) = MONTH(GETDATE()) AND ABS(DATEDIFF(DAY, JobDate, GETDATE())) > 18 AND ABS(DATEDIFF(DAY, JobDate, GETDATE())) <= 24 THEN 1 else 0 END))  AS '" + monthName + " 4th week',";

            }
            if (day > 24 && day <= 31)
            {
                week += "SUM(CASE WHEN YEAR(ShipmentDate) = YEAR(GETDATE()) AND MONTH(ShipmentDate) = MONTH(GETDATE()) AND(DATEPART(DAY, ShipmentDate) - 1) / 7 + 1 = 5 THEN 1 ELSE 0 END) AS '" + monthName + " 5th Week',";
                //week += " SUM((CASE WHEN MONTH(JobDate) = MONTH(GETDATE()) AND ABS(DATEDIFF(DAY, JobDate, GETDATE())) > 24 AND ABS(DATEDIFF(DAY, JobDate, GETDATE())) <= 31 THEN 1 else 0 END))  AS '" + monthName + " 5th week',";

            }

        }
        return week;
    }
    public string getMonthcgl(int crrMonth, int actualMonth)
    {
        DateTime currentDate = DateTime.Now;
        int crrYear = currentDate.Year;

        string filter_month = "";
        if (actualMonth > 0)
        {
            string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames;

            for (int i = 0; i < actualMonth; i++)
            {
                string month_text = monthNames[i];
                int month_val = i + 1;
                if ((actualMonth != crrMonth) || (month_val != actualMonth))
                {
                    filter_month += "SUM(CASE WHEN YEAR(JobDate)= " + crrYear + " and MONTH(JobDate) = " + month_val + " THEN 1 ELSE 0 END) AS '" + month_text + " " + crrYear + "',";
                }
                else
                {
                    filter_month += "SUM(CASE WHEN YEAR(JobDate)= " + crrYear + " and MONTH(JobDate) = " + month_val + " THEN 1 ELSE 0 END) AS '" + month_text + " " + crrYear + "',";
                }
            }
        }
        filter_month = filter_month.Length == 0 ? "SUM(CASE WHEN MONTH(JobDate) = 1 THEN 1 ELSE 0 END) AS Jan," : filter_month;
        return filter_month;
    }
    public string getDESCcgl(int crrMonth, int crrYear = 0)
    {

        string pastYearFilter = "";

        string Totalvalues = "";


        DateTime currentDate = DateTime.Now;
        DateTime elevenMonthsAgo = currentDate.AddMonths(-11);
        int PrevMonth = elevenMonthsAgo.Month;
        int PrevYear = elevenMonthsAgo.Year;

        int CurMonth = currentDate.Month;
        int CurYear = currentDate.Year;


        if (PrevMonth > CurMonth)
        {
            pastYearFilter = "SUM(CASE WHEN (YEAR(JobDate) = " + PrevYear + " AND MONTH(JobDate) >= " + PrevMonth + ") OR (YEAR(JobDate) = " + CurYear + " AND MONTH(JobDate) <= " + CurMonth + ") THEN 1 ELSE 0 END)";
        }



        Totalvalues = pastYearFilter;
        Totalvalues = Totalvalues.Length == 0 ? " SUM(CASE WHEN (YEAR(JobDate) = " + PrevYear + " AND MONTH(JobDate) >= " + PrevMonth + ") OR (YEAR(JobDate) = " + CurYear + " AND MONTH(JobDate) <= " + CurMonth + ") THEN 1 ELSE 0 END)" : Totalvalues;


        return Totalvalues;
    }
    public string getTotalcgl(int crrMonth, int crrYear = 0)
    {

        string pastYearFilter = "";

        string Totalvalues = "";


        DateTime currentDate = DateTime.Now;
        DateTime elevenMonthsAgo = currentDate.AddMonths(-11);
        int PrevMonth = elevenMonthsAgo.Month;
        int PrevYear = elevenMonthsAgo.Year;

        int CurMonth = currentDate.Month;
        int CurYear = currentDate.Year;


        if (PrevMonth > CurMonth)
        {
            pastYearFilter = "SUM(CASE WHEN (YEAR(JobDate) = " + PrevYear + " AND MONTH(JobDate) >= " + PrevMonth + ") OR (YEAR(JobDate) = " + CurYear + " AND MONTH(JobDate) <= " + CurMonth + ") THEN 1 ELSE 0 END) AS Total";
        }



        Totalvalues = pastYearFilter;
        Totalvalues = Totalvalues.Length == 0 ? " SUM(CASE WHEN (YEAR(JobDate) = " + PrevYear + " AND MONTH(JobDate) >= " + PrevMonth + ") OR (YEAR(JobDate) = " + CurYear + " AND MONTH(JobDate) <= " + CurMonth + ") THEN 1 ELSE 0 END) AS Total" : Totalvalues;


        return Totalvalues;
    }
    public string GetPrevYearMonthcgl(int crrMonth, int crrYear = 0)
    {

        string pastYearFilter = "";

        string Totalvalues = "";


        DateTime currentDate = DateTime.Now;
        DateTime elevenMonthsAgo = currentDate.AddMonths(-11);
        int PrevMonth = elevenMonthsAgo.Month;
        int PrevYear = elevenMonthsAgo.Year;

        int CurMonth = currentDate.Month;
        int CurYear = currentDate.Year;


        if (PrevMonth > CurMonth)
        {
            pastYearFilter = " OR (YEAR(JobDate) = " + PrevYear + " AND MONTH(JobDate) >= " + PrevMonth + ")";
        }



        Totalvalues = pastYearFilter;
        Totalvalues = Totalvalues.Length == 0 ? " OR (YEAR(JobDate) = " + PrevYear + " AND MONTH(JobDate) >= " + PrevMonth + ")" : Totalvalues;


        return Totalvalues;
    }

    public string getvariationMonths(int crrMonth, int crrYear = 0)
    {
        string currentYearFilter = "";
        string pastYearFilter = "";
        string filter_month = "";
        if (crrMonth > 0)
        {
            for (int i = 11; i >= 0; i--)
            {


                string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames;
                string month_text = monthNames[i];
                int month_val = i + 1;
                int year_value = 0;

                if (month_val > crrMonth)
                {
                    year_value = crrYear - 1;
                    pastYearFilter += "SUM(CASE WHEN YEAR(a.ShipmentDate)= " + year_value + " and MONTH(a.ShipmentDate) = " + month_val + " THEN 1 ELSE 0 END) AS '" + month_text + " " + year_value + "',";
                }
                else
                {
                    year_value = crrYear;
                    currentYearFilter += "SUM(CASE WHEN YEAR(a.ShipmentDate)= " + year_value + " and MONTH(a.ShipmentDate) = " + month_val + " THEN 1 ELSE 0 END) AS '" + month_text + " " + year_value + "',";
                }


            }
        }
        filter_month = currentYearFilter + pastYearFilter;
        filter_month = filter_month.Length == 0 ? "SUM(CASE WHEN MONTH(a.ShipmentDate) = 1 THEN 1 ELSE 0 END) AS Jan," : filter_month;
        return filter_month;
    }
    public string getvariationTotal(int crrMonth, int crrYear = 0)
    {
        string pastYearFilter = "";
        string Totalvalues = "";
        DateTime currentDate = DateTime.Now;
        DateTime elevenMonthsAgo = currentDate.AddMonths(-11);
        int PrevMonth = elevenMonthsAgo.Month;
        int PrevYear = elevenMonthsAgo.Year;
        int CurMonth = currentDate.Month;
        int CurYear = currentDate.Year;
        if (PrevMonth > CurMonth)
        {
            pastYearFilter = "SUM(CASE WHEN (YEAR(a.ShipmentDate) = " + PrevYear + " AND MONTH(a.ShipmentDate) >= " + PrevMonth + ") OR (YEAR(a.ShipmentDate) = " + CurYear + " AND MONTH(a.ShipmentDate) <= " + CurMonth + ") THEN 1 ELSE 0 END) AS Total";
        }
        Totalvalues = pastYearFilter;
        Totalvalues = Totalvalues.Length == 0 ? " SUM(CASE WHEN (YEAR(a.ShipmentDate) = " + PrevYear + " AND MONTH(a.ShipmentDate) >= " + PrevMonth + ") OR (YEAR(a.ShipmentDate) = " + CurYear + " AND MONTH(a.ShipmentDate) <= " + CurMonth + ") THEN 1 ELSE 0 END) AS Total" : Totalvalues;
        return Totalvalues;
    }
    public string GetvariationPrevYearMonth(int crrMonth, int crrYear = 0)
    {
        string pastYearFilter = "";

        string Totalvalues = "";

        DateTime currentDate = DateTime.Now;
        DateTime elevenMonthsAgo = currentDate.AddMonths(-11);
        int PrevMonth = elevenMonthsAgo.Month;
        int PrevYear = elevenMonthsAgo.Year;

        int CurMonth = currentDate.Month;
        int CurYear = currentDate.Year;


        if (PrevMonth > CurMonth)
        {
            pastYearFilter = " OR (YEAR(a.ShipmentDate) = " + PrevYear + " AND MONTH(a.ShipmentDate) >= " + PrevMonth + ")";
        }



        Totalvalues = pastYearFilter;
        Totalvalues = Totalvalues.Length == 0 ? " OR (YEAR(a.ShipmentDate) = " + PrevYear + " AND MONTH(a.ShipmentDate) >= " + PrevMonth + ")" : Totalvalues;


        return Totalvalues;
    }
    public string getvariationDESC(int crrMonth, int crrYear = 0)
    {
        string pastYearFilter = "";
        string Totalvalues = "";
        DateTime currentDate = DateTime.Now;
        DateTime elevenMonthsAgo = currentDate.AddMonths(-11);
        int PrevMonth = elevenMonthsAgo.Month;
        int PrevYear = elevenMonthsAgo.Year;
        int CurMonth = currentDate.Month;
        int CurYear = currentDate.Year;
        if (PrevMonth > CurMonth)
        {
            pastYearFilter = "SUM(CASE WHEN (YEAR(a.ShipmentDate) = " + PrevYear + " AND MONTH(a.ShipmentDate) >= " + PrevMonth + ") OR (YEAR(a.ShipmentDate) = " + CurYear + " AND MONTH(a.ShipmentDate) <= " + CurMonth + ") THEN 1 ELSE 0 END)";
        }
        Totalvalues = pastYearFilter;
        Totalvalues = Totalvalues.Length == 0 ? " SUM(CASE WHEN (YEAR(a.ShipmentDate) = " + PrevYear + " AND MONTH(a.ShipmentDate) >= " + PrevMonth + ") OR (YEAR(a.ShipmentDate) = " + CurYear + " AND MONTH(a.ShipmentDate) <= " + CurMonth + ") THEN 1 ELSE 0 END)" : Totalvalues;
        return Totalvalues;
    }
    public System.Data.DataTable AirBarchart(string mode, string companyID, string fromdate, string todate)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        string barchart = "";
        string Startdate = fromdate;
        string Enddate = todate;
        string company = companyID;

        //if (companyID != "0")
        //{
        //	barchart = "SELECT LEFT(DATENAME(MONTH, ShipmentDate), 3) + ' ' + CONVERT(nvarchar(4), YEAR(ShipmentDate)) AS Month,COUNT(ShipmentNo) AS Count,CAST(ROUND(SUM(CASE WHEN TRY_CONVERT(decimal(18, 2), REPLACE(ChargeableWt, ',', '')) IS NOT NULL THEN CONVERT(decimal(18, 3), REPLACE(ChargeableWt, ',', '')) / 1000 ELSE 0 END), 3) AS decimal(18, 3)) as weight FROM " + mode + " WHERE OperationsLockedOn !='' AND YEAR(ShipmentDate) = YEAR(GETDATE()) and Status IN ('Completed', 'Job Completed', 'Job Completed') and CompanyID='" + companyID + "' GROUP BY LEFT(DATENAME(MONTH, ShipmentDate), 3) + ' ' + CONVERT(nvarchar(4), YEAR(ShipmentDate)) order by min(ShipmentDate) ASC;";
        //}
        //else
        //{
        //	barchart = "SELECT LEFT(DATENAME(MONTH, ShipmentDate), 3) + ' ' + CONVERT(nvarchar(4), YEAR(ShipmentDate)) AS Month,COUNT(ShipmentNo) AS Count,CAST(ROUND(SUM(CASE WHEN TRY_CONVERT(decimal(18, 2), REPLACE(ChargeableWt, ',', '')) IS NOT NULL THEN CONVERT(decimal(18, 3), REPLACE(ChargeableWt, ',', '')) / 1000 ELSE 0 END), 3) AS decimal(18, 3)) as weight FROM " + mode + " WHERE OperationsLockedOn !='' AND YEAR(ShipmentDate) = YEAR(GETDATE()) and Status IN ('Completed', 'Job Completed', 'Job Completed') GROUP BY LEFT(DATENAME(MONTH, ShipmentDate), 3) + ' ' + CONVERT(nvarchar(4), YEAR(ShipmentDate)) order by min(ShipmentDate) ASC;";
        //}

        if (company != "0")
        {
            barchart = "SELECT LEFT(DATENAME(MONTH, ShipmentDate), 3) + ' ' + CONVERT(nvarchar(4), YEAR(ShipmentDate)) AS Month,COUNT(ShipmentNo) AS Count,CAST(ROUND(SUM(CASE WHEN TRY_CONVERT(decimal(18, 2), REPLACE(ChargeableWt, ',', '')) IS NOT NULL THEN CONVERT(decimal(18, 3), REPLACE(ChargeableWt, ',', '')) / 1000 ELSE 0 END), 3) AS decimal(18, 3)) as weight FROM " + mode + " WHERE OperationsLockedOn ='' and  CAST(ShipmentDate AS date) BETWEEN '" + Startdate + "' AND '" + Enddate + "' and Status!='Completed' and Status!='Job Completed' and CompanyID='" + company + "' GROUP BY LEFT(DATENAME(MONTH, ShipmentDate), 3) + ' ' + CONVERT(nvarchar(4), YEAR(ShipmentDate)) order by min(ShipmentDate) ASC;";
        }
        else
        {
            barchart = "SELECT LEFT(DATENAME(MONTH, ShipmentDate), 3) + ' ' + CONVERT(nvarchar(4), YEAR(ShipmentDate)) AS Month,COUNT(ShipmentNo) AS Count,CAST(ROUND(SUM(CASE WHEN TRY_CONVERT(decimal(18, 2), REPLACE(ChargeableWt, ',', '')) IS NOT NULL THEN CONVERT(decimal(18, 3), REPLACE(ChargeableWt, ',', '')) / 1000 ELSE 0 END), 3) AS decimal(18, 3)) as weight FROM " + mode + " WHERE OperationsLockedOn ='' and  CAST(ShipmentDate AS date) BETWEEN '" + Startdate + "' AND '" + Enddate + "' and Status!='Completed' and Status!='Job Completed' GROUP BY LEFT(DATENAME(MONTH, ShipmentDate), 3) + ' ' + CONVERT(nvarchar(4), YEAR(ShipmentDate)) order by min(ShipmentDate) ASC;";
        }
        SqlCommand monthvluessql = new SqlCommand(barchart);
        monthvluessql.CommandTimeout = 120;
        dt = DA.GetDataTable(monthvluessql);

        return dt;
    }


    public string gettonvariationMonths(int crrMonth, int crrYear = 0)
    {
        string currentYearFilter = "";
        string pastYearFilter = "";
        string filter_month = "";
        if (crrMonth > 0)
        {
            for (int i = 11; i >= 0; i--)
            {


                string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames;
                string month_text = monthNames[i];
                int month_val = i + 1;
                int year_value = 0;
                int Prevmonth_val = month_val - 1;


                if (month_val > crrMonth)
                {
                    year_value = crrYear - 1;
                    pastYearFilter += "CONCAT( FORMAT(SUM(CASE WHEN YEAR(ShipmentDate) = " + year_value + " AND MONTH(ShipmentDate) = " + month_val + " THEN TRY_CONVERT(decimal(18, 0), REPLACE(ChargeableWt, ',', '')) ELSE 0 END), '0'), ' (', CASE WHEN COALESCE(SUM(CASE WHEN YEAR(ShipmentDate) = " + year_value + " AND MONTH(ShipmentDate) = " + Prevmonth_val + " THEN TRY_CONVERT(decimal(18, 0), REPLACE(ChargeableWt, ',', '')) ELSE 0 END), 0) = 0 THEN '100' ELSE FORMAT( ( (SUM(CASE WHEN YEAR(ShipmentDate) = " + year_value + " AND MONTH(ShipmentDate) = " + month_val + " THEN TRY_CONVERT(DECIMAL(20, 2), REPLACE(ChargeableWt, ',', '')) ELSE 0 END) - SUM(CASE WHEN YEAR(ShipmentDate) = " + year_value + " AND MONTH(ShipmentDate) = " + Prevmonth_val + " THEN TRY_CONVERT(DECIMAL(20, 2), REPLACE(ChargeableWt, ',', '')) ELSE 0 END)) / NULLIF(SUM(CASE WHEN YEAR(ShipmentDate) = " + year_value + " AND MONTH(ShipmentDate) = " + Prevmonth_val + " THEN TRY_CONVERT(DECIMAL(20, 2), REPLACE(ChargeableWt, ',', '')) ELSE 0 END), 0) ) * 100,'0' ) END, '%)' ) AS '" + month_text + " " + year_value + "',";
                }
                else
                {
                    year_value = crrYear;
                    currentYearFilter += "CONCAT( FORMAT(SUM(CASE WHEN YEAR(ShipmentDate) = " + year_value + " AND MONTH(ShipmentDate) = " + month_val + " THEN TRY_CONVERT(decimal(18, 0), REPLACE(ChargeableWt, ',', '')) ELSE 0 END), '0'), ' (', CASE WHEN COALESCE(SUM(CASE WHEN YEAR(ShipmentDate) = " + year_value + " AND MONTH(ShipmentDate) = " + Prevmonth_val + " THEN TRY_CONVERT(decimal(18, 0), REPLACE(ChargeableWt, ',', '')) ELSE 0 END), 0) = 0 THEN '100' ELSE FORMAT( ( (SUM(CASE WHEN YEAR(ShipmentDate) = " + year_value + " AND MONTH(ShipmentDate) = " + month_val + " THEN TRY_CONVERT(DECIMAL(20, 2), REPLACE(ChargeableWt, ',', '')) ELSE 0 END) - SUM(CASE WHEN YEAR(ShipmentDate) = " + year_value + " AND MONTH(ShipmentDate) = " + Prevmonth_val + " THEN TRY_CONVERT(DECIMAL(20, 2), REPLACE(ChargeableWt, ',', '')) ELSE 0 END)) / NULLIF(SUM(CASE WHEN YEAR(ShipmentDate) = " + year_value + " AND MONTH(ShipmentDate) = " + Prevmonth_val + " THEN TRY_CONVERT(DECIMAL(20, 2), REPLACE(ChargeableWt, ',', '')) ELSE 0 END), 0) ) * 100,'0' ) END, '%)' ) AS '" + month_text + " " + year_value + "',";
                }


            }
        }
        filter_month = currentYearFilter + pastYearFilter;
        filter_month = filter_month.Length == 0 ? "CONCAT( FORMAT(SUM(CASE WHEN YEAR(ShipmentDate) = 2024 AND MONTH(ShipmentDate) = 2 THEN TRY_CONVERT(decimal(18, 0), REPLACE(ChargeableWt, ',', '')) ELSE 0 END), '0'), ' (', CASE WHEN COALESCE(SUM(CASE WHEN YEAR(ShipmentDate) = 2024 AND MONTH(ShipmentDate) = 1 THEN TRY_CONVERT(decimal(18, 0), REPLACE(ChargeableWt, ',', '')) ELSE 0 END), 0) = 0 THEN '100' ELSE FORMAT( ( (SUM(CASE WHEN YEAR(ShipmentDate) = 2024 AND MONTH(ShipmentDate) = 2 THEN TRY_CONVERT(DECIMAL(20, 2), REPLACE(ChargeableWt, ',', '')) ELSE 0 END) - SUM(CASE WHEN YEAR(ShipmentDate) = 2024 AND MONTH(ShipmentDate) = 1 THEN TRY_CONVERT(DECIMAL(20, 2), REPLACE(ChargeableWt, ',', '')) ELSE 0 END)) / NULLIF(SUM(CASE WHEN YEAR(ShipmentDate) = 2024 AND MONTH(ShipmentDate) = 1 THEN TRY_CONVERT(DECIMAL(20, 2), REPLACE(ChargeableWt, ',', '')) ELSE 0 END), 0) ) * 100, '0' ) END, '%)' ) AS 'Jan'," : filter_month;
        return filter_month;
    }
    public string gettonvariationTotal(int crrMonth, int crrYear = 0)
    {
        string pastYearFilter = "";
        string Totalvalues = "";
        DateTime currentDate = DateTime.Now;
        DateTime elevenMonthsAgo = currentDate.AddMonths(-11);
        int PrevMonth = elevenMonthsAgo.Month;
        int PrevYear = elevenMonthsAgo.Year;
        int CurMonth = currentDate.Month;
        int CurYear = currentDate.Year;
        if (PrevMonth > CurMonth)
        {
            pastYearFilter = "COALESCE( FORMAT( SUM( CASE WHEN (YEAR(ShipmentDate) = " + PrevYear + " AND MONTH(ShipmentDate) >= " + PrevMonth + ") OR (YEAR(ShipmentDate) = " + CurYear + " AND MONTH(ShipmentDate) <= " + CurMonth + ") THEN TRY_CONVERT(decimal(18, 0), REPLACE(ChargeableWt, ',', '')) ELSE 0 END ), '0' ), '0' ) AS 'Total'";
        }
        Totalvalues = pastYearFilter;
        Totalvalues = Totalvalues.Length == 0 ? "COALESCE( FORMAT( SUM( CASE WHEN (YEAR(ShipmentDate) = " + PrevYear + " AND MONTH(ShipmentDate) >= " + PrevMonth + ") OR (YEAR(ShipmentDate) = " + CurYear + " AND MONTH(ShipmentDate) <= " + CurMonth + ") THEN TRY_CONVERT(decimal(18, 0), REPLACE(ChargeableWt, ',', '')) ELSE 0 END ), '0' ), '0' ) AS 'Total'" : Totalvalues;
        return Totalvalues;
    }
    public void DaterangeFilter(DropDownList DD_Datefilter)
    {
        List<ListItem> list = new List<ListItem>();
        list.Add(new ListItem("Custom Range", "0"));
        list.Add(new ListItem("Today", "1"));
        list.Add(new ListItem("Yesterday", "2"));
        list.Add(new ListItem("Last 7 Days", "3"));
        list.Add(new ListItem("Last 30 Days", "4"));
        list.Add(new ListItem("Last 60 Days", "5"));
        List<ListItem> dataSource = list;
        DD_Datefilter.DataSource = dataSource;
        DD_Datefilter.DataTextField = "Text";
        DD_Datefilter.DataValueField = "Value";
        DD_Datefilter.DataBind();
        ListItem listItem = DD_Datefilter.Items.FindByValue("3");
        if (listItem != null)
        {
            listItem.Selected = true;
        }
    }
    public void DaterangeFilter1(DropDownList DD_Datefilter)
    {
        List<ListItem> list = new List<ListItem>();
        list.Add(new ListItem("Custom Range", "0"));
        list.Add(new ListItem("Today", "1"));
        list.Add(new ListItem("Yesterday", "2"));
        list.Add(new ListItem("Last 7 Days", "3"));
        list.Add(new ListItem("Last 30 Days", "4"));
        list.Add(new ListItem("Last 60 Days", "5"));
        List<ListItem> dataSource = list;
        DD_Datefilter.DataSource = dataSource;
        DD_Datefilter.DataTextField = "Text";
        DD_Datefilter.DataValueField = "Value";
        DD_Datefilter.DataBind();
        ListItem listItem = DD_Datefilter.Items.FindByValue("1");
        if (listItem != null)
        {
            listItem.Selected = true;
        }
    }
    public void LoadGroup(DropDownList DD_Group)
    {
        string str_grouplist = "select GM_PK,GM_GroupName from ICL_GroupMaster ";
        SqlCommand command = new SqlCommand(str_grouplist);
        DataSet ds = DA.GetDataSet(command);
        if (ds != null && ds.Tables.Count > 0)
        {
            DD_Group.DataSource = ds.Tables[0];
            DD_Group.DataTextField = "GM_GroupName";
            DD_Group.DataValueField = "GM_PK";
            DD_Group.DataBind();
            DD_Group.Items.Insert(0, new ListItem("Select Group", "0"));
        }
    }

}
