<%@ Page Language="C#" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<WebPartPages:AllowFraming ID="AllowFraming" runat="server" />

<html>
<head>
  <title></title>
  <link href="../Content/bootstrap.css" rel="stylesheet" />
  <script src="../Scripts/jquery-2.1.4.js"></script>
  <script src="../Scripts/bootstrap.js"></script>
  <script type="text/javascript" src="/_layouts/15/MicrosoftAjax.js"></script>
  <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
  <script type="text/javascript" src="/_layouts/15/sp.js"></script>
  <script src="../Scripts/Wingtip.SharePointListManager.js"></script>
  <script src="../Scripts/TitleBumper.js"></script>

</head>
<body>

  <table class="table table-striped">
     <tr>
      <td style="width:150px;">Item ID:</td>
      <td id="itemId" style="width:350px;" ></td>
    </tr>
      <tr>
      <td style="width:150px;">eTag:</td>
      <td id="etag" style="width:350px;" ></td>
    </tr>
    <tr>
      <td style="width:150px;">Item Type:</td>
      <td id="itemType" style="width:350px;" ></td>
    </tr>
    <tr>
      <td style="width:150px;">Current Title:</td>
      <td>
        <input type="text" id="currentTitle" style="width:350px;" readonly="readonly" />
      </td>
    </tr>
    <tr>
      <td>Proposed Title:</td>
      <td>
        <input type="text" id="proposedTitle" style="width:350px;" readonly="readonly" />
      </td>
    </tr>
   
  </table>

  <div>
    <input class="btn" value="Update" id="cmdUpdate" type="button" />
    <input class="btn" value="Cancel" id="cmdCancel" type="button" />
  </div>

</body>
</html>
