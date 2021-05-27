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
        internal static string GetJson(string url)
        {
            var client = new WebClientWithTimeout();
            client.Headers["Content_type"] = "application/json;charset=UTF-8";
            client.Encoding = System.Text.Encoding.UTF8;
            string myOut = client.DownloadString(url);
            return myOut;
        }

    }
}