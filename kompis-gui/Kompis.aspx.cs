using log4net;
using Newtonsoft.Json;
using parts_of_PCAxis.Web.Core;
using System;
using System.Web.Configuration;

namespace kompis_gui
{
    public partial class Kompis : System.Web.UI.Page
    {
        private static readonly ILog log = LogManager.GetLogger("Kompis");

        string komisApiUrlBase;
        string id;
        string val;
        string lang;
        string KompisUrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            init();
            ShowKompisInfo();
        }
        private void init()
        {
            komisApiUrlBase = WebConfigurationManager.AppSettings["KompisServerUrl"] + WebConfigurationManager.AppSettings["KompisApiPath"];

            try
            {
                lang = ValidationManager.GetValue(Request.QueryString["lang"].ToString());
                id = ValidationManager.GetValue(Request.QueryString["id"].ToString());
                val = ValidationManager.GetValue(Request.QueryString["val"].ToString());
                // old parameter ver(tion) er ignored by api, so it is no longer read,
                // but may be present in QueryString
            }
            catch (Exception e)
            {
                log.Error(e);
                ShowErrorMessage();
            }
 
            KompisUrl = komisApiUrlBase + "/" + id + "/" + val + "/";
        }
        private void ShowKompisInfo()
        {
            try
            {
                string json = KompisApiClient.GetJson(KompisUrl);

                Kompisresult kompisresult = JsonConvert.DeserializeObject<Kompisresult>(json);


                if (lang == "no")
                {
                    lblTitleValue.Text = kompisresult.variable.labelNo;
                    lblDescription.Text = kompisresult.variable.descriptionNo;
                    //lblFormulaHeading.Text = "Formel:";
                    btnClose.Text = "Lukk";
                }
                else
                {
                    lblTitleValue.Text = kompisresult.variable.labelEn;
                    lblDescription.Text = kompisresult.variable.descriptionEn;
                    //lblFormulaHeading.Text = "Formel:";
                    btnClose.Text = "Close";
                }

                //if (kompisresult.formula != null)
                //{
                //    lblFormulaHeading.Visible = true;
                //    lblFormula.Text = kompisresult.formula.ToString();
                //}
            }
            catch (Exception e)
            {
                log.Error("Source api url:" + KompisUrl);
                log.Error(e);
                ShowErrorMessage();
            }
        }

        private void ShowErrorMessage()
        {
            if (lang == "no")
            {
                lblTitleValue.Text = "Beskrivelse midlertidig utilgjengelig";
                btnClose.Text = "Lukk";
            }
            else
            {
                lblTitleValue.Text = "Description temporary unavailable";
                btnClose.Text = "Close";
            }

        }
    }
}