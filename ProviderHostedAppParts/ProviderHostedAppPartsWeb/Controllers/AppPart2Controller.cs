using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProviderHostedAppPartsWeb.Models;

namespace ProviderHostedAppPartsWeb.Controllers {
  public class AppPart2Controller : Controller {
    public ActionResult Index() {

      

      HostWebManager hostWebManager = new HostWebManager();
      return View(hostWebManager.GetHostWeb());
    }
  }
}
