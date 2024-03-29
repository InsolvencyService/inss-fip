﻿using Microsoft.AspNetCore.Mvc;

namespace INSS.FIP.Web.Controllers
{
    public class ErrorsController : Controller
    {
        [Route("/errors/{statusCode?}")]
        public IActionResult Index(int? statusCode)
        {
            Response.StatusCode = 200;
            
            if (statusCode != null)
            {
                switch (statusCode.Value)
                {
                    case 404:
                        return View("NotFound");
                    case 503:
                        return View("Maintenance");
                }
            }

            return View();
        }
    }
}