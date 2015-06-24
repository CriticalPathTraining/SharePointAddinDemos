using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProviderHostedCustomUIActionsWeb.Models;

namespace ProviderHostedCustomUIActionsWeb.Controllers {
  public class HomeController : Controller {
    [SharePointContextFilter]
    public ActionResult Index() {

      ContactsListManager contactsListManager = new ContactsListManager();
      var existingContactsLists = contactsListManager.GetContactsLists();
      return View(existingContactsLists);
  
    }

    public ActionResult CreateContactsList() {
      ContactsListManager contactsListManager = new ContactsListManager();
      ContactsList newList = contactsListManager.CreateContactsList();
      return Redirect(newList.DefaultViewUrl);
    }



  }
}
