using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAsemly_NoiThat.Model;

namespace WebAsemly_NoiThat.Service
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _httpClient.GetFromJsonAsync<List<Product>>("https://localhost:44320/api/Product");
        }

        public async Task<Product> GetProductId(int id)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"https://localhost:44320/api/Product/{id}");
        }

        /*public async Task<Account> GetAccountId(int id)
        {
            return await _httpClient.GetFromJsonAsync<Account>($"https://localhost:44320/api/Account/{id}");
        }*/

        public async Task<List<Category>> GetCategorys()
        {
            return await _httpClient.GetFromJsonAsync<List<Category>>("https://localhost:44320/api/Category");
        }
    }
}
