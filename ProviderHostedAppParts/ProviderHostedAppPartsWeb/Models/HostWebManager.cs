using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.WebParts;
using Microsoft.SharePoint.Client.EventReceivers;

namespace ProviderHostedAppPartsWeb.Models {


  public class SharePointList {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string DefaultViewUrl { get; set; }
  }
  


  public class SharePointHostWeb {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string SiteTemplate { get; set; }
    public string MasterUrl { get; set; }
    public string CustomMasterUrl { get; set; }
    public string AlternateCssUrl { get; set; }
    public string Language { get; set; }
    public IEnumerable<SharePointList> Lists { get; set; }
  }
  
  public class HostWebManager {
    
    ClientContext clientContext;
    Web hostWeb;

    public HostWebManager() {
      var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext.Current);
      clientContext = spContext.CreateUserClientContextForSPHost();
      hostWeb = clientContext.Web;
      clientContext.Load(hostWeb);
      clientContext.ExecuteQuery();
    }

    public SharePointHostWeb GetHostWeb() {

      
      clientContext.Load(hostWeb.Lists, lists => 
        lists.Include(list => list.Id, list => list.Title, list => list.DefaultViewUrl)
             .Where(list => (list.BaseType == 0 && !list.Hidden)));
      clientContext.ExecuteQuery();
      
      List<SharePointList> listsInHostWeb = new List<SharePointList>();
      foreach(List list in hostWeb.Lists){
        listsInHostWeb.Add(new SharePointList{
          Id=list.Id,
          Title = list.Title,  
          DefaultViewUrl = hostWeb.Url + list.DefaultViewUrl
        });
      }

      return new SharePointHostWeb {
        Id = hostWeb.Id,
        Title = hostWeb.Title,
        Url = hostWeb.Url,
        MasterUrl = hostWeb.MasterUrl,
        CustomMasterUrl = hostWeb.CustomMasterUrl,
        AlternateCssUrl = hostWeb.AlternateCssUrl,
        Language = hostWeb.Language.ToString(),
        SiteTemplate = hostWeb.WebTemplate + "#" + hostWeb.Configuration.ToString(),
        Lists = listsInHostWeb
      };
    }
    

  }
}