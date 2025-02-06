using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Windows.Documents;

public partial class Masterpage_MasterPage : System.Web.UI.MasterPage
{
    DataAccess DA;
    SessionCustom SC;
    PhTemplate PH;
    string str_userid = "";
    string str_Email = "";
    string str_userRole = "";
    string Userimage = "";
    string Company_Id = "";
    string Company_Name = "";
    string str_MenuAcive = "";
    string Str_groupName = "";
    string qsnumber = "";
    string selectedOrgname = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        DA = new DataAccess();
        SC = new SessionCustom();
        str_Email = SC.username;
        str_userRole = SC.UserRole;
        str_userid = SC.Userid;
        if (!IsPostBack)
        {
            hd_title.InnerHtml = this.SC.lablename;
            li_title.Text = "ICL HUB | " + this.SC.headername; 
            string currentPage = Request.Url.Segments[Request.Url.Segments.Length - 1];
            this.usermages();
            int menunamewhere = 0;
            string str_sql1 = ("select * from ICL_Menu inner join ICL_SubMaster a on Menukey=a.SM_MenuId inner join ICL_GroupMaster b on b.GM_Pk=a.SM_GroupId inner join ICL_Users c on b.GM_Role = c.ICL_role where parentmenuid='" + menunamewhere + "' and c.ICL_role='" + str_userRole + "' and ICL_Email='" + str_Email + "' order by menulist asc");
            DataTable oDataTable1 = DA.GetDataTable(str_sql1);
            string str_sql = ("select * from ICL_Menu inner join ICL_SubMaster a on Menukey=a.SM_MenuId inner join ICL_GroupMaster b on b.GM_Pk = a.SM_GroupId inner join ICL_Users c on b.GM_Role = c.ICL_role where c.ICL_role='" + str_userRole + "' and ICL_Email='" + str_Email + "' order by menulist asc");
            DataTable oDataTable = DA.GetDataTable(str_sql);
            string str_role = oDataTable1.Rows[0]["Gm_role"].ToString();
            switch (str_role)
            {
                case "0":
                    {
                        sp_role.InnerHtml = "Admin";
                        OrgID.Visible = false;
                        break;
                    }

                case "1": { 
                        sp_role.InnerHtml = SC.Org_names;
                        
                       
                            this.dropdowname();
                        
                       
                        break; 
                    }
                default:break;
            }
            DataRow[] drParentMenu1 = oDataTable1.Select();
            DataRow[] drParentMenu = oDataTable.Select("ParentmenuID is null");
            var oStringBuilder = new StringBuilder();
            var oStringBuilder1 = new StringBuilder();
            string MenuList = GenerateMenu(drParentMenu, oDataTable, oStringBuilder, currentPage);
            string MenuList1 = GenerateSingleMenu(drParentMenu1, oDataTable1, oStringBuilder1, currentPage);
            Literal1.Text = MenuList1 + MenuList;
            
            
        }
    }

    private void dropdowname()
    {
        
        string str_grouplist = "select org_name,org_id,org_ccode from ICl_Organization  where org_userid='" + str_userid + "'";
        SqlCommand command = new SqlCommand(str_grouplist);
        DataSet ds = DA.GetDataSet(command);
        if (ds != null && ds.Tables.Count > 0)
        {
            Orgname.DataSource = ds.Tables[0];
            Orgname.DataTextField = "org_name";
            Orgname.DataValueField = "org_id";
            Orgname.DataBind();
            Orgname.SelectedValue = SC.Orgname;
        }
    }
    private string GenerateMenu(DataRow[] drParentMenu, DataTable oDataTable, StringBuilder oStringBuilder,string currentPage)
    {
        if (drParentMenu.Length > 0)
        {
            foreach (DataRow dr in drParentMenu)
            {
                string MenuURL = dr["Pagename"].ToString();
                string MenuName = dr["Menuname"].ToString();
                string folder = dr["Foldername"].ToString();
                string micons = dr["Menuicon"].ToString();
                string title = dr["menudescription"].ToString();
                string parentid = dr["ParentmenuID"].ToString();
                string line = "";
                bool isActive = string.Equals(MenuURL, currentPage, StringComparison.OrdinalIgnoreCase);
                string activeClass = isActive ? "active" : string.Empty;

                if (string.IsNullOrEmpty(folder))
                {
                    line = String.Format(@"<li class='submenu {0}' ><a  title=""{2}"" ><div class=""balance-bit-img"" ><i class=""{1}""></i></div> <span>{2} </span> <span class=""menu-arrow""></span></a>", activeClass, micons, MenuName, title);
                    oStringBuilder.Append(line);
                    string MenuID = dr["Menukey"].ToString();
                    string ParentID = dr["ParentmenuID"].ToString();
                    DataRow[] subMenu = oDataTable.Select(String.Format("ParentmenuID = '" + MenuID + "'"));
                    if (subMenu.Length > 0 && !MenuID.Equals(ParentID))
                    {
                        var subMenuBuilder = new StringBuilder();
                        oStringBuilder.AppendLine("<ul style=\"display: none;\">");
                        oStringBuilder.AppendLine("<li>");
                        oStringBuilder.Append(GenerateMenu(subMenu, oDataTable, subMenuBuilder, currentPage));
                    }
                }
                else
                {
                    line = String.Format(@"<li  class='{0}'><a href=""../{1}/{2}"" title=""{4}"" ><i style=""color:#1d64d6"" class=""fa fa-arrow-right""></i><span> {3}</span></a>", activeClass, folder, MenuURL, MenuName, title);
                    oStringBuilder.Append(line);
                    string MenuID = dr["Menukey"].ToString();
                    string ParentID = dr["ParentmenuID"].ToString();
                    DataRow[] subMenu = oDataTable.Select(String.Format("ParentmenuID = '" + MenuID + "'"));
                    if (subMenu.Length > 0 && !MenuID.Equals(ParentID))
                    {
                        var subMenuBuilder = new StringBuilder();
                        oStringBuilder.Append(GenerateMenu(subMenu, oDataTable, subMenuBuilder, currentPage));
                    }
                    oStringBuilder.Append("</li>");
                }
            }
        }

        oStringBuilder.AppendLine("</li>");
        oStringBuilder.Append("</ul>");
        oStringBuilder.AppendLine("</li>");
        return oStringBuilder.ToString();
    }
    private string GenerateSingleMenu(DataRow[] drParentMenu1, DataTable oDataTable1, StringBuilder oStringBuilder1, string currentPage)
    {
        oStringBuilder1.AppendLine("<ul>"); if (drParentMenu1.Length > 0)
        {
            foreach (DataRow dr in oDataTable1.Rows)
            {
                string MenuURL1 = dr["Pagename"].ToString();
                string MenuName1 = dr["Menuname"].ToString();
                string folder1 = dr["Foldername"].ToString();
                string micons1 = dr["Menuicon"].ToString();
                string title1 = dr["menudescription"].ToString();
                bool isActive = string.Equals(MenuURL1, currentPage, StringComparison.OrdinalIgnoreCase);
                string activeClass = isActive ? "active" : string.Empty;
                string line1 = String.Format(@"<li class=""{5}"" ><a href=""../{0}/{1}"" class=""active"" title=""{4}""><div class=""balance-bit-img""><i  class=""{2}""></i></div><span>{3}</span></a>", folder1, MenuURL1, micons1, MenuName1, title1, activeClass);
                oStringBuilder1.Append(line1);
            }
        }
        oStringBuilder1.Append("</li>");
        return oStringBuilder1.ToString();
    }
    private void usermages()
    {
        string str_image = "select ICL_Image,ICL_UserName from  ICL_Users where ICL_Email=@ICL_Email";
        SqlCommand cmd = new SqlCommand(str_image);
        cmd.Parameters.AddWithValue("@ICL_Email", this.str_Email);
        DataTable DT_image = DA.GetDataTable(cmd);
        if (DT_image.Rows.Count > 0)
        {
            string str_loadimage = DT_image.Rows[0]["ICL_Image"].ToString();
            lb_Username.InnerText = SC.Name = DT_image.Rows[0]["ICL_UserName"].ToString();

            if (str_loadimage == "")
            {
                img_profile.ImageUrl = "~/images/user.jpg";
            }
            else if (str_loadimage == str_userid)
            {
                img_profile.ImageUrl = "~/images/user.jpg";
            }
            else
            {
                img_profile.ImageUrl = "~/Images/" + str_loadimage;
            }
        }
    }

    protected void Orgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedOrgname = Orgname.SelectedValue;

        
        string str_org = "select top 1 org_name,org_id,org_ccode from  ICl_Organization  where org_id='" + selectedOrgname + "'";
        SqlCommand org = new SqlCommand(str_org);

        DataTable dt_org = this.DA.GetDataTable(org);
        if (dt_org.Rows.Count > 0)
        {
            SC.Orgname = dt_org.Rows[0]["org_id"].ToString();
            SC.Orgnamecode = dt_org.Rows[0]["org_ccode"].ToString();
            SC.Org_names = dt_org.Rows[0]["org_name"].ToString();
        }
        sp_role.InnerHtml = SC.Org_names;
        Orgname.Text = SC.Orgname;
        Response.Redirect(Request.RawUrl);

    }
    public string GetSelectedDropdownValue()
    {
        return SC.Orgnamecode = Orgname.SelectedValue;
    }
}
