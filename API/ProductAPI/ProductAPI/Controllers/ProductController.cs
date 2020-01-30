using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Managers;
using ProductAPI.Models;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Consumes("application/json")]
    [Produces("application/json", "application/xml")]
    [Route("api/product")]
    [ApiController]
    [SwaggerTag("Create, read, and update products")]
    public class ProductController : ControllerBase
    {
        #region Private Variables
        private readonly IProductManager productManager;
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController" /> class.
        /// </summary>
        /// <param name="productManager">The product manager.</param>
        public ProductController(IProductManager productManager)
        {
            Log.Debug("Entering Class ProductController().");
            this.productManager = productManager;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the list of products
        /// </summary>
        /// <param name="computerName">Name of the computer.</param>
        /// <param name="qparams">The product type and manufacturer.</param>
        /// <returns>
        /// List of products.
        /// </returns>
        /// <exception cref="Exception">Database connection Open failed or any Db exception</exception>
        /// <response code="200">Returns a list of products</response>
        /// <response code="204">No content found</response>
        [HttpGet]
        [ProducesResponseType(typeof(IList<Product>), 200)]
        [SwaggerResponse(200, "The list of Product", typeof(IList<Product>))]
        [ProducesResponseType(typeof(void), 204)]
        [SwaggerResponse(204, "No product found")]
        [SwaggerOperation(
            Description = "Gets the list of product for a product type",
            OperationId = "GetProducts"
        )]
        public IActionResult GetProducts(
            [FromHeader(Name = "Computer-Id")] string computerName,
            [FromQuery] QueryParamsInput qparams = null)
        {
            Log.Debug("Entering Method GetProducts().");
            Log.Debug("Method Header:Computer-Id {@ComputerName}", computerName);

            try
            {
                var lstProduct = this.productManager.GetProducts(qparams.manufacturer, qparams.type);
                Log.Information("Products returned from api {@lstProduct}");


                if (lstProduct == null || lstProduct.Count == 0)
                {
                    Log.Information("No product found");
                    return NoContent();
                }
                else
                {
                    return Ok(lstProduct);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception while calling GetProducts()");
                return StatusCode(500);
            }
        }

        /// <summary>
        ///  Insert or update a product, the insert functinality is in progress
        /// </summary>
        /// <param name="computerName"> Name of the computer.</param>
        /// <param name="product">The product information that will insert or update</param>
        /// <returns></returns>
        /// <exception cref="Exception">Database connection Open failed or any exception thrown while insert or update the record in db</exception>
        /// <response code="200">Successfully inserted or updated the product</response>
        /// <response code="400"> input data format is not correct</response>
        [HttpPost]
        [ProducesResponseType(typeof(IList<Product>), 200)]
        [SwaggerResponse(200, "The list of Product", typeof(IList<Product>))]
        [ProducesResponseType(400)]
        [SwaggerResponse(400, "Bad request")]
        [SwaggerOperation(
            Description = "Insert or update the list of product",
            OperationId = "UpsertProduct"
        )]
        public IActionResult UpsertProduct(
            [FromHeader(Name = "Computer-Id")] string computerName,
            [FromBody]  Product product)
        {
            Log.Debug("Entering Method UpsertProduct().");
            Log.Debug("Method Header:Computer-Id {@ComputerName}", computerName);
            Log.Information("Updating the product {@product}", product);
            try
            {
                var isSuccessfullyUpdated = this.productManager.UpdateProduct(product);

                if (isSuccessfullyUpdated)
                {
                    Log.Information("Successfully updated the produc {@product}", product);
                    return Ok("Successfully updated the product");
                }
                else
                {
                    return Ok("product details did not updated successfully");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception while calling UpdateProduct()");
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

    }
}