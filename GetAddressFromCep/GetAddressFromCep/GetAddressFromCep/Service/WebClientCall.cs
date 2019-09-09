using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace GetAddressFromCep.Service
    {
    class WebClientCall
        {

        private static readonly WebClient webClient = new WebClient();

        public static string DownloadString(string url) {
            
            return webClient.DownloadString(url);

            }
        }
    }
