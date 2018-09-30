using AutoMapper;
using Dto;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Boss.WEBAPI.Controllers
{
   // [Authorize]
   [Route("Product")]
    public class ProductController: ApiController
    {

        ProductRepository repository = new ProductRepository();
        [HttpGet]
        
        public async Task<HttpResponseMessage> GetProducts()
        {
            ProductList products = new ProductList();

            products = await Task.Run(() => repository.GetProductsAsync());

            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> PutProduct(ProductDetail productDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productDetail == null)
            {
                return BadRequest("Product detail should not be null.");
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDto, Model.Product>();
            });

            IMapper iMapper = config.CreateMapper();

            var products = iMapper.Map<List<ProductDto>, List<Model.Product>>(productDetail.Products);

            await Task.Run(() => repository.SaveProductsAsync(products));           
            var productList = await Task.Run(() => repository.GetProductsAsync());           

            return Ok("Products saved successfully.");
        }
    }
}
