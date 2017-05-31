using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HtmlSelector.ViewModels;

namespace HtmlSelector.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new SelectorViewModel();

            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}