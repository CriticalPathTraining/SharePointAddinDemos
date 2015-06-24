using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProviderHostedAppPartsWeb.Models;

namespace ProviderHostedAppPartsWeb.Controllers
{
    public class AppPart1Controller : Controller
    {
        // GET: AppPart1
        public ActionResult Index()
        {
            return View(SampleData.GetCustomers());
        }
    }
}