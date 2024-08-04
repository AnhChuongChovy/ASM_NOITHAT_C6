using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAsemly_NoiThat.Model;
using WebAsemly_NoiThat.Models;

namespace WebAsemly_NoiThat.Service
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //List Product

        public async Task<List<Product>> GetProducts()
        {
            return await _httpClient.GetFromJsonAsync<List<Product>>("https://localhost:44320/api/Product");
        }
        //List Category
        public async Task<List<Category>> GetCategorys()
        {
            return await _httpClient.GetFromJsonAsync<List<Category>>("https://localhost:44320/api/Category");
        }

        //List CategoryType
        public async Task<List<CategoryType>> GetCategoryTypes()
        {
            return await _httpClient.GetFromJsonAsync<List<CategoryType>>("https://localhost:44320/api/CategoryType");
        }

        //CategoryType ID
        public async Task<CategoryType> GetCategoryTypesId(int id)
        {
            return await _httpClient.GetFromJsonAsync<CategoryType>($"https://localhost:44320/api/CategoryType/{id}");
        }

        //Product ID
        public async Task<Product> GetProductId(int id)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"https://localhost:44320/api/Product/{id}");
        }
        //Account ID
        public async Task<Account> GetAccountId(int id)
        {
            return await _httpClient.GetFromJsonAsync<Account>($"https://localhost:44320/api/Account/{id}");
        }
        public async Task UpdateAccount(Account account)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:44320/api/Account/{account.ID}", account);
            response.EnsureSuccessStatusCode();
        }


        public async Task<List<CategoryType>> GetCategoryTypesAsync(int categoryId)
        {
            return await _httpClient.GetFromJsonAsync<List<CategoryType>>($"https://localhost:44320/api/CategoryType/{categoryId}/types");
        }

        public async Task<List<Product>> GetProductsAsync(int categoryTypeId)
        {
            return await _httpClient.GetFromJsonAsync<List<Product>>($"https://localhost:44320/api/CategoryType/{categoryTypeId}/products");
        }
    }
}
