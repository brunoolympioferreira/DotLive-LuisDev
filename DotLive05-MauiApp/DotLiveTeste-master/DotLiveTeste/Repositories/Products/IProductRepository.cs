using DotLiveTeste.Models;

namespace DotLiveTeste.Repositories.Products
{
    public interface IProductRepository
    {
        Task<IList<Product>> GetAllAsync();
    }
}
