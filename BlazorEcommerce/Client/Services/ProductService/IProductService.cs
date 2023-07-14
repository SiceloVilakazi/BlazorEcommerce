namespace BlazorEcommerce.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action OnProductChanged;
        List<Product> Products { get; set; }

        string Message { get; set; }
        Task GetProducts(string? categoryUrl=null);
        Task<ServiceResponse<Product>> GetProduct(int id);

        Task SearchProducts(string searchText);
        Task<List<string>> GetProductsSuggestions(string searchText);
    }
}
