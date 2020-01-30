using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Product.WebApp.Models;
using System.Text.Json;
using Product.WebApp.Repository;

namespace Product.WebApp.Controllers
{
    [Route("Product")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;


        public ProductController(IProductRepository repository)
        {
            this._repository = repository;
        }
        [Route("/")]
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var products = _repository.OnGet().Result;
            return View(products);
        }


        [Route("details")]
        public IActionResult GetProductDetails(string id)
        {
            return View();
        }
    }
}