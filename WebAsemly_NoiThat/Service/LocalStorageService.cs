using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace WebAsemly_NoiThat.Service
{
    public class LocalStorageService
    {
        private readonly IJSRuntime _jsRuntime;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SetItemAsync(string key, object value)
        {
            var json = JsonConvert.SerializeObject(value);
            await _jsRuntime.InvokeVoidAsync("localStorageHelper.setItem", key, json);
        }

        public async Task<T> GetItemAsync<T>(string key)
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorageHelper.getItem", key);
            return json == null ? default : JsonConvert.DeserializeObject<T>(json);
        }

        public async Task RemoveItemAsync(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorageHelper.removeItem", key);
        }
    }
}