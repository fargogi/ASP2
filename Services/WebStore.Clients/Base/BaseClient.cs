using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient
    {
        protected readonly HttpClient _httpClient;

        public string ServiceAddress { get; set; }

        protected BaseClient(IConfiguration configuration)
        {
            _httpClient = new HttpClient
            {
                BaseAddress=new Uri(configuration["ClientAddress"])
            };

            _httpClient.DefaultRequestHeaders.Accept.Clear();

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
