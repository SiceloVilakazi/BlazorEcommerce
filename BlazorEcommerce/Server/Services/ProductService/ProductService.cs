namespace BlazorEcommerce.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var response = new ServiceResponse<List<Product>>()
            {
                Data = await _context.Products.Include(x => x.Variants).ToListAsync()
            };

        return response;
        }
        public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
        {
            var response = new ServiceResponse<Product>();
            var product = await _context.Products
                .Include(x=>x.Variants).ThenInclude(y=>y.ProductType)
                .FirstOrDefaultAsync(x=>x.Id==productId);

            if (product == null)
            {
                response.success = false;
                response.Message = "product does not exist";
            }
            else
            {
                response.Data = product;
            }
            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductbyCategory(string categoryUrl)
        {
            var response = new ServiceResponse<List<Product>>()
            {
                Data = await _context.Products
                .Where(x=>x.Category.Url.ToLower() == categoryUrl.ToLower()).Include(x => x.Variants).ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> SearchProducts(string searchText)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await FindProductsBySearchText(searchText)
            };
            return response;
        }

        private async Task<List<Product>> FindProductsBySearchText(string searchText)
        {
            return await _context.Products
                            .Where(p => p.Title.ToLower().Contains(searchText.ToLower())
                            || p.Description.ToLower().Contains(searchText.ToLower()))
                            .Include(x => x.Variants).ToListAsync();
        }

        public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText)
        {
            var products = await FindProductsBySearchText(searchText);

            List<string> suggestions = new List<string>();

            foreach (var product in products)
            {
                if(product.Title.Contains(searchText,StringComparison.OrdinalIgnoreCase))
                    suggestions.Add(product.Title);

                if(product.Description !=null)
                {
                    var punctuation = product.Description.Where(char.IsPunctuation)
                        .Distinct().ToArray();
                    var words = product.Description.Split()
                        .Select(s => s.Trim(punctuation));

                    foreach (var word in words)
                    {
                        if(word.Contains(searchText,StringComparison.OrdinalIgnoreCase)
                            && !suggestions.Contains(word))
                        {
                            suggestions.Add(word);
                        }
                    }
                }
            }

            return new ServiceResponse<List<string>> { Data = suggestions };
        }
    }
}
