using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.WebParts;
using Microsoft.SharePoint.Client.EventReceivers;

namespace ProviderHostedCustomUIActionsWeb.Models {

  public class ContactsList {
    public string Title { get; set; }
    public string DefaultViewUrl { get; set; }
  }


  public class ContactsListManager {

    ClientContext clientContext;
    Web hostWeb;
    List contactsList;

    public ContactsListManager() {
      var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext.Current);
      clientContext = spContext.CreateUserClientContextForSPHost();
      hostWeb = clientContext.Web;
      clientContext.Load(hostWeb);
      clientContext.ExecuteQuery();
    }

    public ContactsListManager(Guid ListId) {
      var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext.Current);
      clientContext = spContext.CreateUserClientContextForSPHost();
      hostWeb = clientContext.Web;
      clientContext.Load(hostWeb);
      contactsList = hostWeb.Lists.GetById(ListId);
      clientContext.Load(contactsList);
      clientContext.ExecuteQuery();
    }

    public IEnumerable<ContactsList> GetContactsLists(){
      List<ContactsList> ExisitingContactsLists = new List<ContactsList>();

      ListCollection contactsLists = clientContext.Web.Lists;
      clientContext.Load(contactsLists, lists => lists.Where(list => list.BaseTemplate == 105)
                                              .Include(list => list.Title, list => list.DefaultViewUrl));
      clientContext.ExecuteQuery();
      foreach (var contactsList in contactsLists) {
        ExisitingContactsLists.Add(new ContactsList {
          Title = contactsList.Title,
          DefaultViewUrl = clientContext.Web.Url + contactsList.DefaultViewUrl
        });
      }

      return ExisitingContactsLists;
    }

    public ContactsList CreateContactsList() {

      ListCreationInformation lci = new ListCreationInformation();
      lci.Title = "Contacts";
      lci.TemplateType = 105;
      lci.Url = "Lists/Contacts";
      lci.QuickLaunchOption = QuickLaunchOptions.On;

      List contactsList  = clientContext.Web.Lists.Add(lci);
      clientContext.Load(contactsList, list => list.Title, list => list.DefaultViewUrl);
      clientContext.ExecuteQuery();

      return new ContactsList { 
        Title=contactsList.Title,
        DefaultViewUrl= (clientContext.Web.Url + contactsList.DefaultViewUrl)
      }; 
    }

    public void AddNewContact(string FirstName, string LastName, string Company, string Email, string WorkPhone, string HomePhone) {
      ListItem newItem = contactsList.AddItem(new ListItemCreationInformation());
      Customer customer = CustomerFactory.GetRandomCustomer();
      newItem["FirstName"] = FirstName;
      newItem["Title"] = LastName;
      newItem["Company"] = Company;
      newItem["Email"] = Email;
      newItem["WorkPhone"] = WorkPhone;
      newItem["HomePhone"] = HomePhone;
      newItem.Update();
      clientContext.ExecuteQuery();
    }

    public void AddRockStars() {
      AddNewContact("Jim", "Morrison", "The Doors", "jim@thisistheend.com", "(888)111-2222", "(813)222-3333");
      AddNewContact("John", "Lennon", "The Beatles", "john@givepeaceachance.com", "(777)111-3333", "(813)222-3333");
      AddNewContact("Janis", "Joplin", "The Holding Company", "janis@holdingcompany.com", "(888)111-2222", "(813)222-3333");
      AddNewContact("Eric", "Clapton", "Blind Faith", "jim@thisistheend.com", "(888)111-2222", "(813)222-3333");
      AddNewContact("Jimi", "Hendrix", "The Jimi Hendrix Experience", "jim@thisistheend.com", "(888)111-2222", "(813)222-3333");
    }

    public void AddNewContact() {
      ListItem newItem = contactsList.AddItem(new ListItemCreationInformation());
      Customer customer = CustomerFactory.GetRandomCustomer();
      newItem["FirstName"] = customer.FirstName;
      newItem["Title"] = customer.LastName;
      newItem["Company"] = customer.Company;
      newItem["Email"] = customer.EmailAddress;
      newItem["WorkPhone"] = customer.WorkPhone;
      newItem["HomePhone"] = customer.HomePhone;
      newItem.Update();
      clientContext.ExecuteQuery();
    }

    public void AddNewContacts(int CustomerCount) {
      for (int i = 1; i <= CustomerCount; i++) {
        ListItem newItem = contactsList.AddItem(new ListItemCreationInformation());
        Customer customer = CustomerFactory.GetRandomCustomer();
        newItem["FirstName"] = customer.FirstName;
        newItem["Title"] = customer.LastName;
        newItem["Company"] = customer.Company;
        newItem["Email"] = customer.EmailAddress;
        newItem["WorkPhone"] = customer.WorkPhone;
        newItem["HomePhone"] = customer.HomePhone;
        newItem.Update();
        clientContext.ExecuteQuery();
      }
    }

    public void DeleteAllItems() {

      CamlQuery camlQuery = new CamlQuery();
      camlQuery.ViewXml = @"<View><Query><Where><IsNotNull><FieldRef Name='ID' /></IsNotNull></Where></Query><ViewFields><FieldRef Name='ID' /></ViewFields></View>";
      ListItemCollection items = contactsList.GetItems(camlQuery);
      clientContext.Load(items);
      clientContext.ExecuteQuery();

      foreach (var item in items) {
        contactsList.GetItemById(item.Id).DeleteObject();
        clientContext.ExecuteQuery();
      }



    }

  }
}