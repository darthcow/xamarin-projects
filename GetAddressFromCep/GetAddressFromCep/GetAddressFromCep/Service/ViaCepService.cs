using System;
using System.Collections.Generic;
using System.Text;
using GetAddressFromCep.Model;
using Newtonsoft.Json;


namespace GetAddressFromCep.Service
    {
    public class ViaCepService
        {

            private static string addressUrl = "http://viacep.com.br/ws/{0}/json";

        public static Address searchAddress(string cep)
        {
            cep = string.Format(addressUrl, cep);
            return JsonConvert.DeserializeObject<Address>(WebClientCall.DownloadString(cep));
            }
        }
    }
