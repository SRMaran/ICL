using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for PhTemplateSupplier
/// </summary>
public class PhTemplateExt:PhTemplate
{
    public PhTemplateExt()
	{

        this.SetCurrencyColumn = new string[] { "OrderTotal", "TotalPrice" };

		//
		// TODO: Add constructor logic here
		//
	}

    //public DataSet GetApprovedBuyerList()
    //{
    //    string str_DataURL = new AppVar().WebRoot + "sys/formxml.ashx?fid=e_Customer_form" + "&filter=CusStatus=2";
    //    return this.GetDataSetFromUrl(str_DataURL);
    //}

    public void LoadQuestionWithAnswerOption(DataSet ds, PlaceHolder ph_Grid, string str_ItemTemplateFile, string str_TableName)
    {
        AppVar AV = new AppVar ();
        string str_Template = this.ReadFileToString(str_ItemTemplateFile);
        string str_AnswerOptionRadio =  this.ReadFileToString("UserAnswer_Radio2.txt");
        string str_AnswerOptionCheck =  this.ReadFileToString("UserAnswer_Check2.txt");
        string str_AnswerOptionText = this.ReadFileToString("UserAnswer_TextArea2.txt");

        string str_TemplateUpdated = "";
        
        string str_ColumnValue = "";
        Literal li = new Literal();

        if (ds.Tables.Count == 0) return; // throw new Exception("Records not found, Grid load failed");

        DataTable dt_Data;
        if (str_TableName == "") dt_Data = ds.Tables[0];
        else dt_Data = ds.Tables[str_TableName];

        int intRow = 0;
        string str_ItemTemplate = "";
        string str_ItemDetail = "";
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

            li = new Literal();
            li.Text = str_TemplateUpdated;
            str_ItemTemplate = str_AnswerOptionRadio;
            if (dr["QueAnsTypeId"].ToString() == "2") str_ItemTemplate = str_AnswerOptionCheck;
            if (dr["QueAnsTypeId"].ToString() == "4") str_ItemTemplate = str_AnswerOptionText;

            int i = 0;
            str_ItemDetail = "";
            if (ds.Tables.Count == 2)
            {
                foreach (DataRow dr_AnswerOption in ds.Tables[1].Select("QueKey='" + dr["QueKey"].ToString() + "'"))
                {
                    str_ItemDetail += str_ItemTemplate;

                    str_ItemDetail = str_ItemDetail.Replace("%%OptText%%", dr_AnswerOption["OptText"].ToString());
                    str_ItemDetail = str_ItemDetail.Replace("%%OptRowKey%%", dr_AnswerOption["OptRowKey"].ToString());
                    str_ItemDetail = str_ItemDetail.Replace("%%QueKey%%", dr["QueKey"].ToString());
                }
            }

            li.Text = li.Text.Replace("%%AnswerOption%%", str_ItemDetail);
            ph_Grid.Controls.Add(li);
        }
    }



    public void LoadFileGird(DataSet ds, PlaceHolder ph_Grid, string str_ItemTemplateFile, string str_TableName)
    {
        AppVar AV = new AppVar();
        string str_Template = this.ReadFileToString(str_ItemTemplateFile);

        string str_TemplateUpdated = "";

        string str_ColumnValue = "";
        Literal li = new Literal();

        if (ds.Tables.Count == 0) return; // throw new Exception("Records not found, Grid load failed");

        DataTable dt_Data;
        if (str_TableName == "") dt_Data = ds.Tables[0];
        else dt_Data = ds.Tables[str_TableName];

        int intRow = 0;
        string str_FileName = "",str_FileName_ForDisplay = "";
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

                str_FileName = Convert.ToString(dr["FileName"]).ToLower();
                
                if (str_FileName == "") 
                {
                    str_TemplateUpdated = str_TemplateUpdated.Replace("%%DisplayFile%%", " - ");
                }
                
                else if (str_FileName.Contains(".jpeg") || str_FileName.Contains(".jpg") || str_FileName.Contains(".png") || str_FileName.Contains(".gif"))
                {
                    str_TemplateUpdated = str_TemplateUpdated.Replace("%%DisplayFile%%", "<img width='60' height='60' src='../UploadFile/Documents/" + dr["FileName"] + "'>  </img>");
                }
                else
                {
                    str_FileName_ForDisplay = str_FileName.Remove(0, 37);
                    str_TemplateUpdated = str_TemplateUpdated.Replace("%%DisplayFile%%", "<a target='_blank' href='../UploadFile/Documents/" + str_FileName + "'>"+ str_FileName_ForDisplay + "</a>");
                }
            }
         

            li = new Literal();
            li.Text = str_TemplateUpdated;
            ph_Grid.Controls.Add(li);
        }
    }
}