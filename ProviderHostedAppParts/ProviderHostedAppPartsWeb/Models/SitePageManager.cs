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

  public class SitePageManager {

    string ProductId = "335c0b6e-8ec5-4b0f-b92e-7a6d9a50d79f";
    string FeatureId = "335c0b6e-8ec5-4b0f-b92e-7a6d9a50d7a0";
    string hostWebID; // determine at runtime


    ClientContext clientContext;
    Web hostWeb;
    List sitePages;
    NavigationNodeCollection topNavNodes;

    public SitePageManager(ClientContext clientContext) {
      this.clientContext = clientContext;
      this.hostWeb = clientContext.Web;
      this.sitePages = hostWeb.Lists.GetByTitle("Site Pages");
      clientContext.Load(hostWeb, web => web.Id);
      clientContext.Load(sitePages);
      topNavNodes = hostWeb.Navigation.TopNavigationBar;
      clientContext.Load(topNavNodes);
      clientContext.ExecuteQuery();
      hostWebID = hostWeb.Id.ToString();
    }

    public void UpdateWikiHomePage(string PageContent) {

      var WikiHomePage = sitePages.RootFolder.Files.GetByUrl("Home.aspx");
      WikiHomePage.ListItemAllFields["WikiField"] = PageContent;
      WikiHomePage.ListItemAllFields.Update();
      clientContext.ExecuteQuery();

    }

    public void CreateWikiPage(string FileName, string PageTitle, string PageContent) {

      clientContext.Load(sitePages.RootFolder, f => f.ServerRelativeUrl);
      clientContext.ExecuteQuery();

      var sitePagesUrl = sitePages.RootFolder.ServerRelativeUrl;
      var newWikiPageUrl = sitePagesUrl + "/" + FileName;

      var currentPageFile = hostWeb.GetFileByServerRelativeUrl(newWikiPageUrl);

      clientContext.Load(currentPageFile, f => f.Exists);
      clientContext.ExecuteQuery();

      if (currentPageFile.Exists) {
        currentPageFile.DeleteObject();
        clientContext.ExecuteQuery();
      }

      var newWikiPageFile = sitePages.RootFolder.Files.AddTemplateFile(newWikiPageUrl, TemplateFileType.WikiPage);
      ListItem newWikiPageItem = newWikiPageFile.ListItemAllFields;
      newWikiPageItem["Title"] = PageTitle;
      newWikiPageItem["WikiField"] = PageContent;
      newWikiPageItem.Update();
      clientContext.ExecuteQuery();

      CreateTopNavNode(PageTitle, FileName, false);

    }


    public void CreateWebPartPage(string FileName, string PageTitle) {

      clientContext.Load(sitePages.RootFolder, f => f.ServerRelativeUrl);
      clientContext.ExecuteQuery();

      var sitePagesUrl = sitePages.RootFolder.ServerRelativeUrl;
      var newWebPartPageUrl = sitePagesUrl + "/" + FileName;

      var currentPageFile = hostWeb.GetFileByServerRelativeUrl(newWebPartPageUrl);

      clientContext.Load(currentPageFile, f => f.Exists);
      clientContext.ExecuteQuery();

      if (currentPageFile.Exists) {
        currentPageFile.DeleteObject();
        clientContext.ExecuteQuery();
      }

      var newPage = new FileCreationInformation{
        Url= FileName,
        Overwrite=true,
        Content = Encoding.UTF8.GetBytes(Properties.Resources.WebPartPageTemplate)
      };

      var newWebPartPageFile = sitePages.RootFolder.Files.Add(newPage);
      //ListItem newWebPartPageItem = newWebPartPageFile.ListItemAllFields;
      //newWebPartPageItem["Title"] = PageTitle;
      //newWebPartPageItem.Update();
      clientContext.ExecuteQuery();

      CreateTopNavNode(PageTitle, FileName, false);

    }


    string GetAppPartDefinition(string AppPartName) {
      string webPartDefiniton = Properties.Resources.AppPartDefinition;
      webPartDefiniton = webPartDefiniton.Replace("@HostWebId", hostWebID.ToString());
      webPartDefiniton = webPartDefiniton.Replace("@ProductId", ProductId);
      webPartDefiniton = webPartDefiniton.Replace("@FeatureId", FeatureId);
      webPartDefiniton = webPartDefiniton.Replace("@AppPartName", AppPartName);
      return webPartDefiniton;
    }

    public void AddAppPartToPage(string targetPage, string AppPartName) {

      var page = clientContext.Web.GetFileByServerRelativeUrl("/SitePages/" + targetPage);
      var webPartManager = page.GetLimitedWebPartManager(PersonalizationScope.Shared);
      string webPartDefinitionXml = GetAppPartDefinition(AppPartName);
      WebPartDefinition webPartDefinition = webPartManager.ImportWebPart(webPartDefinitionXml);
      WebPart webPart = webPartDefinition.WebPart;
      webPartManager.AddWebPart(webPart, "FullPage", 0);
      clientContext.ExecuteQuery();
    
    }

    public void CreateTopNavNode(string NodeTitle, string NodeUrl, bool ExternalNode) {
      NavigationNodeCreationInformation newNode = new NavigationNodeCreationInformation();
      newNode.IsExternal = ExternalNode;
      newNode.Title = NodeTitle;
      newNode.Url = sitePages.RootFolder.ServerRelativeUrl + "/" + NodeUrl;
      newNode.AsLastNode = true;
      topNavNodes.Add(newNode);
      clientContext.ExecuteQuery();
    }

    public void CreateTopNavNode(string NodeTitle, string NodeUrl) {
      CreateTopNavNode(NodeTitle, NodeUrl, false);
    }

    public void DeleteAllTopNavNodes() {
      // delete all existing nodes
      for (int index = (topNavNodes.Count - 1); index >= 0; index--) {
        ExceptionHandlingScope scope = new ExceptionHandlingScope(clientContext);
        using (scope.StartScope()) {
          using (scope.StartTry()) {
            topNavNodes[index].DeleteObject();
          }
          using (scope.StartCatch()) {
          }
        }
        clientContext.ExecuteQuery();
      }
      clientContext.Load(topNavNodes);
      clientContext.ExecuteQuery();
    }
  
  }
}