using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace ServiceRegistry.Client 
{
    public class Client 
    {
        private HttpClient _client;
        public Client(HttpClient client) 
            : this(client, "http://localhost:9595") {}
        
        public Client(HttpClient client, string baseUrl)
        {
            client.BaseAddress = new Uri(baseUrl);
            _client = client;
        }
        
        public async Task<List<SavedServiceDefinition>> GetAllServices() 
        {
            var response = await _client.GetAsync("/services");
            if(!response.IsSuccessStatusCode) return null;
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<SavedServiceDefinition>>(json);
        }

        public async Task<SavedServiceDefinition> RegisterService(ServiceDefinition serviceDefinition) 
        {
            var json = JsonConvert.SerializeObject(serviceDefinition);
            var body = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/services", body);
            if(!response.IsSuccessStatusCode) return null;                
            json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SavedServiceDefinition>(json);
        }

        public async Task<SavedServiceDefinition> FindServiceById(Guid id)
        {
            var response = await _client.GetAsync($"/services/{id}");
            if(!response.IsSuccessStatusCode) return null;        
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SavedServiceDefinition>(json);
        }

        public async Task<List<SavedServiceDefinition>> FindServiceByType(string type)
        {
            var response = await _client.GetAsync($"/services/{type}");
            if(!response.IsSuccessStatusCode) return null;
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<SavedServiceDefinition>>(json);
        }

        public async Task<SavedServiceDefinition> DeleteService(Guid id)
        {
            var response = await _client.DeleteAsync($"/services/{id}");
            if(!response.IsSuccessStatusCode) return null;
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SavedServiceDefinition>(json);
        }
    }
}