using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWebStore.DomainEntities.Entities;
using MyWebStore.Infrastructure.Interfaces;

namespace MyWebStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IProductData _productData;

        public HomeController(IProductData productData)
        {
            _productData = productData;
        }

        public IActionResult Index() => View();

        public IActionResult Create() => View();

        public IActionResult Edit(int id) => View();

        public IActionResult Details(int id) => View();

        public IActionResult Delete(int id) => View();

        public IActionResult ProductsList() => View(_productData.GetProducts());


    }
}