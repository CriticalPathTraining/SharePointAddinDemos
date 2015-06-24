using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProviderHostedCustomUIActionsWeb.Models;

namespace ProviderHostedCustomUIActionsWeb.Controllers {
  
  public class UIActionsController : Controller {

    public ActionResult AddNewItem(string SPListId) {
      ContactsListManager contactsListManager = new ContactsListManager(new Guid(SPListId));
      contactsListManager.AddNewContact();
      return View();
    }


    public ActionResult AddTenItems(string SPListId) {
      ContactsListManager contactsListManager = new ContactsListManager(new Guid(SPListId));
      contactsListManager.AddNewContacts(10);
      return View();
    }


    public ActionResult AddFiftyItems(string SPListId) {
      ContactsListManager contactsListManager = new ContactsListManager(new Guid(SPListId));
      contactsListManager.AddNewContacts(50);
      return View();
    }

    public ActionResult AddRockStars(string SPListId) {
      ContactsListManager contactsListManager = new ContactsListManager(new Guid(SPListId));
      contactsListManager.AddRockStars();
      return View();
    }

    public ActionResult DeleteAllItems(string SPListId) {
      ContactsListManager contactsListManager = new ContactsListManager(new Guid(SPListId));
      contactsListManager.DeleteAllItems();
      return View();
    }
  }
}