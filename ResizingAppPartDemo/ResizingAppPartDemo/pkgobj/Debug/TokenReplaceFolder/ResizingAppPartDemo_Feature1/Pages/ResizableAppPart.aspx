<%@ Page Language="C#" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<WebPartPages:AllowFraming ID="AllowFraming" runat="server" />

<html>
<head>
  <title></title>
  <script src="../Scripts/jquery-1.9.1.js"></script>
  <script src="../Scripts/ResizableAppPart.js"></script>
  <link href="../Content/App.css" rel="stylesheet" />
</head>
<body style="background-color:yellow">

  <h2>Resizable App part</h2>

  <div>
    <input type="button" id="cmdGetSmall" value="Get Small" />
  </div>

  <div>
    <input type="button" id="cmdGetBig" value="Get Big" />
  </div>


</body>
</html>
