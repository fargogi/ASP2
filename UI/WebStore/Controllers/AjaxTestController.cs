﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyWebStore.Controllers
{
    public class AjaxTestController : Controller
    {
        public IActionResult Index() => View();

        public async Task<IActionResult> GetTestView()
        {
            await Task.Delay(2000);
            return PartialView("Partial/_DataView", DateTime.Now);
        }

        public async Task<IActionResult> GetJson(int? id, string msg)
        {
            await Task.Delay(2000);

            return Json(new
            {
                Message = $"Response ({id ?? -1}): {msg ?? "<null>"}",
                ServerTime = DateTime.Now
            });
        }
    }
}