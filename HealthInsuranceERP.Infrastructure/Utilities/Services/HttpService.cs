using HealthInsuranceERP.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Infrastructure.Services
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _clientFactory;

        public HttpService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<HttpResponseMessage> Get(string url, IDictionary<string, string> headers, string token)
        {
            var client = _clientFactory.CreateClient();


            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            foreach (var tm in headers)
            {
                client.DefaultRequestHeaders.Add(tm.Key, tm.Value);
            }
            return await client.GetAsync(url);
        }

        public async Task<HttpResponseMessage> Post(string url, HttpContent content, IDictionary<string, string> headers, string token)
        {
            try
            {
                var client = _clientFactory.CreateClient();

                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                foreach (var tm in headers)
                {
                    client.DefaultRequestHeaders.Add(tm.Key, tm.Value);
                }
                var e = await client.PostAsync(url, content);

                return e;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public async Task<HttpResponseMessage> Put(string url, HttpContent content, IDictionary<string, string> headers, string token)
        {
            var client = _clientFactory.CreateClient();
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            foreach (var tm in headers)
            {
                client.DefaultRequestHeaders.Add(tm.Key, tm.Value);
            }
            return await client.PutAsync(url, content);
        }

    }
}
