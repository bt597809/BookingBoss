using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DBClient;
using Repository;
using Dto;
using AutoMapper;
using Entity.Model;
using Boss.WEBAPI.Filters;
using Boss.WEBAPI.Helpers.ErrorHelpers;
using CacheService;
using Boss.WEBAPI.Helpers;
using Services;

namespace Boss.WEBAPI.Controllers
{
    [AuthorizationRequired]
    public class ProductsController : ApiController
    {
        public readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;

        }
        // GET: api/Products
        public HttpResponseMessage GetProducts()
        {
            var products = _productService.GetAll();

            if (products != null && products.Count() > 0)
                return Request.CreateResponse(HttpStatusCode.OK, products);

            throw new ApiDataException(1000, "Products not found", HttpStatusCode.NotFound);
        }
        // GET: api/Products/Id
        public async Task<HttpResponseMessage> GetProducts(long Id)
        {
            var products = await Task.Run(() => _productService.GetbyId(Id));

            if (products != null)
                return Request.CreateResponse(HttpStatusCode.OK, products);

            throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
        }
        // PUT: api/Products
        [HttpPut]
        public IHttpActionResult PutProducts([FromBody]ProductDetail productDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productDetail == null)
            {
                return BadRequest("Product detail should not be null.");
            }

            //SaveUpdate to DB the Data async
            _productService.SaveProductsAsync(productDetail.Products);

            return Ok("Products saved successfully.");
        }
        // POST api/product
        [HttpPost]
        public IHttpActionResult PostProducts([FromBody] Dto.Product productDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productDetail == null)
            {
                return BadRequest("Product detail should not be null.");
            }

            var retrurnProd = _productService.AddProduct(productDetail);

            if (retrurnProd != null && retrurnProd.Id > 0)
                return Ok(retrurnProd);
            else
                throw new ApiDataException(1002, "Product not saved.", HttpStatusCode.NotFound);
        }
        // POST api/product
        [HttpDelete]
        public IHttpActionResult DeleteProducts([FromBody] Dto.Product productDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productDetail == null)
            {
                return BadRequest("Product detail should not be null.");
            }

            bool isSucess = _productService.DeleteProduct(productDetail);

            if (isSucess)
                return Ok("Products deleted successfully.");
            else
                throw new ApiDataException(1001, "Product not deleted.", HttpStatusCode.NotFound);
        }

    }
}