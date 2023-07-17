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
        public string Message { get; set; } = "Loading Products...";
        public int CurrentPage { get ; set ; }
        public int PageCount { get; set ; }
        public string LastSearchText { get; set; }

        public event Action OnProductChanged;

        public async Task<ServiceResponse<Product>> GetProduct(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{id}");
             return  result;
        }

        public async Task GetProducts(string? categoryUrl=null)
        {

            var result = categoryUrl == null ?
               await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/Product/GetFeaturedProducts") :
                await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/Product/getByCategory/{categoryUrl}");
            if (result != null && result.Data != null)
                Products = result.Data;

            OnProductChanged.Invoke();
        }

        public async Task<List<string>> GetProductsSuggestions(string searchText)
        {
            var result = await _httpClient
                .GetFromJsonAsync<ServiceResponse<List<string>>>($"api/Product/searchSuggestions/{searchText}");
            return result.Data;

        }

        public async Task SearchProducts(string searchText, int page)
        {
            var result = await _httpClient
                .GetFromJsonAsync<ServiceResponse<ProductSearchResult>>($"api/Product/search/{searchText}/{page}");
            if (result != null && result.Data != null)
                
            if (Products.Count == 0) Message = "No products found.";
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }
            OnProductChanged?.Invoke();
        }
    }
}
