using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.WebParts;
using Microsoft.SharePoint.Client.EventReceivers;

using ProviderHostedAppPartsWeb.Models;

namespace ProviderHostedAppPartsWeb.Services {
  public class AppEventReceiver : IRemoteEventService {

    public void PrepareHostWeb(ClientContext clientContext) {

      SitePageManager spManager = new SitePageManager(clientContext);

      spManager.CreateWebPartPage("AppPart1.aspx", "App Part 1");
      spManager.AddAppPartToPage("AppPart1.aspx", "AppPart1");

      spManager.CreateWebPartPage("AppPart2.aspx", "App Part 2");
      spManager.AddAppPartToPage("AppPart2.aspx", "AppPart2");

      spManager.CreateWebPartPage("AppPart3.aspx", "App Part 3");
      spManager.AddAppPartToPage("AppPart3.aspx", "AppPart3");

    }

    public SPRemoteEventResult ProcessEvent(SPRemoteEventProperties properties) {
      using (ClientContext clientContext = TokenHelper.CreateAppEventClientContext(properties, useAppWeb: false)) {
        PrepareHostWeb(clientContext);
      }
      return new SPRemoteEventResult();
    }

    public void ProcessOneWayEvent(SPRemoteEventProperties properties) {
      throw new NotImplementedException();
    }

  }
}
