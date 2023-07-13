using System.Net.Http.Json;

namespace BlazorEcommerce.Client.Services.ProductService
{
    public class ProductService : IProductService
    {

        private readonly HttpClient _httpClient;
        public ProductService(HttpClient client)
        {
            _httpClient = client;
        }
       public List<Product> Products { get; set; } = new List<Product>();

        public event Action OnProductChanged;

        public async Task<ServiceResponse<Product>> GetProduct(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{id}");
             return  result;
        }

        public async Task GetProducts(string? categoryUrl=null)
        {

            var result = categoryUrl == null ?
               await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product") :
                await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/Product/getByCategory/{categoryUrl}");
            if (result != null && result.Data != null)
                Products = result.Data;

            OnProductChanged.Invoke();
        }
    }
}
