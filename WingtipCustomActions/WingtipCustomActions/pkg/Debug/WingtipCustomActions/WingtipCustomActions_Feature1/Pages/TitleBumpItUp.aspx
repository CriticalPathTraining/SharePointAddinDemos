<%@ Page Language="C#" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<WebPartPages:AllowFraming ID="AllowFraming" runat="server" />

<html>
<head>
  <title></title>
  <link href="../Content/bootstrap.css" rel="stylesheet" />
</head>
<body>

  <div style="padding:12px;">

    <h3>Modifying Title - Please Wait</h3>

    <div>
      <img src="../Content/Waiting.gif" alt="please wait" />
    </div>
  
  </div>

  <script src="../Scripts/jquery-2.1.4.js"></script>
  <script src="../Scripts/bootstrap.js"></script>
  <script type="text/javascript" src="/_layouts/15/MicrosoftAjax.js"></script>
  <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
  <script type="text/javascript" src="/_layouts/15/sp.js"></script>
  <script src="../Scripts/Wingtip.SharePointListManager.js"></script>
  <script src="../Scripts/TitleBumpItUp.js"></script>

</body>
</html>
