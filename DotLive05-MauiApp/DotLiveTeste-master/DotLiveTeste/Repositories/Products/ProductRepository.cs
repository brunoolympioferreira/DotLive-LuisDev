using DotLiveTeste.Helpers;
using DotLiveTeste.Models;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;

namespace DotLiveTeste.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        public async Task<IList<Product>> GetAllAsync()
        {
            try
            {
                var response = await Constants.BASE_URL
                    .AppendPathSegment("/products")
                    .GetAsync();

                if (response.ResponseMessage.IsSuccessStatusCode)
                {
                    var content = await response.ResponseMessage.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IList<Product>>(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return new List<Product>();
        }
    }
}
