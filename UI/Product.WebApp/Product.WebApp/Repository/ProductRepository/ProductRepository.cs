using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Product.WebApp.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        public ProductRepository(IHttpClientFactory clientFactory)
        {
            this._clientFactory = clientFactory;
        }
        public async Task<IEnumerable<Models.Product>> OnGet()
        {
            IEnumerable<Models.Product> lstProducts = new List<Models.Product>();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5055/api/product");
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                lstProducts = await JsonSerializer.DeserializeAsync<IEnumerable<Models.Product>>(responseStream);
            }
            return lstProducts;
        }
    }
}
