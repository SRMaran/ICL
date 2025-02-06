using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System.Collections.Specialized; 
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

    public class LogWriter
    {

        bool bln_LogEnable = true;
        string str_LogFileName = "MMddyyyy";
        string str_LogFolderName = "LogFiles";
        string str_LogForSeverity = "Error,Warning,Information,Critical,Unknown";

        public enum Severity
        {
            Unknown = 0,
            Critical = 1,
            Error = 2,
            Warning = 3,
            Information = 4
        }

        public bool LogEnable { get { return this.bln_LogEnable; } set { this.bln_LogEnable = value; } }
        public string LogFileName { get { return this.str_LogFileName; } set { this.str_LogFileName = value; } }
        public string LogFolderName { get { return this.str_LogFolderName; } set { this.str_LogFolderName = value; } }
        public string LogForSeverity { get { return this.str_LogForSeverity; } set { this.str_LogForSeverity = value; } }

        public LogWriter()
        {
            this.Intialize();
        }


        private void Intialize()
        {
            try
            {
                string str_LogConfig = System.Configuration.ConfigurationManager.AppSettings["LogConfig"];
                if (str_LogConfig == null) 
                { 
                    this.MakeDirectoryIfNotExists(this.LogFolderName); 
                    return; 
                }
                
                string str_LogKeyId = "";
                string str_LogKeyValue = "";
                foreach (string str_LogKey in str_LogConfig.Split(';'))
                {
                    str_LogKeyId = str_LogKey.Substring(0,str_LogKey.IndexOf("=")).ToLower();
                    str_LogKeyValue = str_LogKey.Substring(str_LogKey.IndexOf("=") + 1);
                    if (str_LogKeyId.Contains("logenable")) this.LogEnable = Convert.ToBoolean(str_LogKeyValue);
                    else if (str_LogKeyId.Contains("logfilename")) this.LogFileName = str_LogKeyValue;
                    else if (str_LogKeyId.Contains("logfoldername")) this.LogFolderName = str_LogKeyValue;
                    else if (str_LogKeyId.Contains("logforseverity")) this.LogForSeverity = str_LogKeyValue; 
                }
                //this.LogEnable = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["LogEnable"]);
                //this.LogFileName = System.Configuration.ConfigurationManager.AppSettings["LogFileName"];
                //this.LogFolderName = System.Configuration.ConfigurationManager.AppSettings["LogFolderName"];
                //this.LogForSeverity = System.Configuration.ConfigurationManager.AppSettings["LogForSeverity"];
            }
            catch (Exception ex)
            {
                ex.Source += "Log Setting in web.config is incorrect or not found, check web.config";
                throw ex;
            }

            if (this.LogEnable)
                this.MakeDirectoryIfNotExists(this.LogFolderName);

        }


        private void MakeDirectoryIfNotExists(string str_FolderName)
        {
            if (!(Directory.Exists(this.ApplicationPath()+ str_FolderName)))
                Directory.CreateDirectory(this.ApplicationPath() + str_FolderName);
        }

        public void WriteLog(string str_Log, Severity sev_Log)
        {
            this.WriteLog(new Exception(str_Log),sev_Log);
        }
        public void WriteLog(Exception ex, Severity sev_Log)
        {
            this.WriteLog(ex, "", sev_Log);
        }

        public void WriteLog(Exception ex, string str_addl_info, Severity sev_Log)
        {
            //return;

            if (!this.LogEnable) return;

            if (!this.LogForSeverity.ToLower().Contains(sev_Log.ToString().ToLower())) return;
            
            DateTime dt_current = DateTime.Today;
            //string str_FileNameWithPath = this.ApplicationPath() + this.LogFolderName + "\\" + this.LogFileName + "_" + dt_current.Day.ToString() + dt_current.DayOfWeek.ToString() + dt_current.Year.ToString() + ".txt";
            string str_FileNameWithPath = this.ApplicationPath() + this.LogFolderName + "\\" + DateTime.Now.ToString("MMddyyyy") + ".txt";
            try
            {
                StreamWriter sw = new StreamWriter(str_FileNameWithPath, true);
                sw.WriteLine("Severity:" + sev_Log.ToString() +  ";Source:" + ex.Source + "; Message:" + ex.Message  + "; StackTrace:"  + ex.StackTrace  + "; InnerException:" + ex.InnerException + ";CustomReference:" + str_addl_info + ";");
                sw.Close();
            }
            catch (Exception ex1)
            {
                if (ex1.Message.IndexOf("because it is being used by another process") != -1)
                {
                    WriteLog(ex, sev_Log); 
                }
                else throw ex1;
            }

        }

        public string ApplicationPath()
        {
            string str_AppPath = System.Web.HttpContext.Current.Request.ApplicationPath;
            string str_AppPathFull = System.Web.HttpContext.Current.Server.MapPath(str_AppPath);
            
            if (str_AppPath != "/")  str_AppPathFull += "\\";
            return str_AppPathFull;
        }
    }

