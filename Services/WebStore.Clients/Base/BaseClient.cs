using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient : IDisposable
    {
        protected readonly HttpClient _httpClient;

        public string ServiceAddress { get; set; }

        protected BaseClient(IConfiguration configuration)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(configuration["ClientAddress"])
            };

            _httpClient.DefaultRequestHeaders.Accept.Clear();

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected T Get<T>(string url) where T : new() => GetAsync<T>(url).Result;

        protected async Task<T> GetAsync<T>(
            string url,
            CancellationToken cancellationToken = default(CancellationToken))
            where T : new()
        {
            var response = await _httpClient.GetAsync(url, cancellationToken);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<T>();
            return default(T);
        }

        protected HttpResponseMessage Post<T>(string url, T value) => PostAsync(url, value).Result;

        protected async Task<HttpResponseMessage> PostAsync<T>(
            string url,
            T value,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _httpClient.PostAsJsonAsync(url, value, cancellationToken);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Put<T>(string url, T value) => PutAsync(url, value).Result;

        protected async Task<HttpResponseMessage> PutAsync<T>(
            string url,
            T value,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _httpClient.PutAsJsonAsync(url, value, cancellationToken);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;

        protected async Task<HttpResponseMessage> DeleteAsync(
            string url,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _httpClient.DeleteAsync(url, cancellationToken);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
