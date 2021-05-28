using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using System.Web.Configuration;
using kompis_gui;

namespace Monitor
{
    public partial class Default : System.Web.UI.Page
    {
        string monitorDirekteUrl;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            monitorDirekteUrl = WebConfigurationManager.AppSettings["KompisServerUrl"] + WebConfigurationManager.AppSettings["KompisPingPath"];
            GetTableQuery(monitorDirekteUrl);
        }

        public void GetTableQuery(string pMonitorUrl)
        {
            try
            {
                string ping = KompisApiClient.GetJson(pMonitorUrl);
                getQueryDirekte.CssClass = "applicationOK";  
            }
            catch (Exception)
            {
                getQueryDirekte.CssClass = "applicationERROR";
            }
        }

        public static string AssemblyVersion
        {
            get
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                var displayVersion = version.Major + "." + version.Minor + "." + version.Build;

                if (version.Revision > 0)
                {
                    displayVersion += "." + version.Revision;
                }

                return displayVersion;
            }
        }

        public static string DotNetVersion
        {
            get { return Assembly.GetExecutingAssembly().ImageRuntimeVersion; }
        }

        public string MonitorDirekteUrl
        {
            get { return monitorDirekteUrl; }
        }
    }
}