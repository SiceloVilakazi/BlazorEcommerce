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
    }
}
