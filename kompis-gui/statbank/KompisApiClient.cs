using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kompis_gui
{
    public class KompisApiClient
    {
        private KompisApiClient() { }

        /// <summary>
        /// Calls the url and returns string in horror format.
        /// </summary>
        /// <param name="url">url to the data you want.</param>
        /// <returns></returns>
        internal static string GetJson(string url, bool status404isOk)
        {
            var client = new WebClientWithTimeout();
            client.Headers["Content_type"] = "application/json;charset=UTF-8";
            client.Encoding = System.Text.Encoding.UTF8;
            string myOut = "";
            try
            {
                myOut = client.DownloadString(url);
            } catch(System.Net.WebException e)
            {
                if (!(status404isOk && e.Message.Equals("Den eksterne serveren returnerte feilen (404) Finner ikke filen.")))
                {
                    throw e;
                }
            }

            return myOut;
        }

    }
}