﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HtmlSelector.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HtmlSelector.Controllers
{
    [Route("api/[controller]")]
    public class ProxyController : Controller
    {
        [HttpPost]
        public string Post(SelectorViewModel model)
        {
            return "Hi dude";
        }
    }
}