$(function () {

  var hostWebUrl = getQueryStringParameter("SPHostUrl");
  var itemId = getQueryStringParameter("SPListItemId");
  var listId = decodeURIComponent(getQueryStringParameter("SPListId"));

  $("#cmdCancel").click(function () {
    window.parent.postMessage('CloseCustomActionDialogRefresh', '*');
  });


  $("#cmdUpdate").click(function () {

    var itemId = $("#itemId").text();
    var itemType = $("#itemType").text();
    var etag = $("#etag").text();
    var newTitle = $("#proposedTitle").val();

    Wingtip.SharePointListManager.updateItem(hostWebUrl, listId, itemType , itemId, newTitle, etag).done(function () {
      window.parent.postMessage('CloseCustomActionDialogRefresh', '*');
    });

  });

  Wingtip.SharePointListManager.initialize();

  Wingtip.SharePointListManager.getItem(hostWebUrl, listId, itemId).done(function (data) {
    $("#itemId").text(data.d.Id);
    $("#itemType").text(data.d.__metadata.type);
    $("#etag").text(data.d.__metadata.etag);
    $("#currentTitle").val(data.d.Title);
    $("#proposedTitle").val(data.d.Title.toUpperCase());
  });

});

function getQueryStringParameter(paramName) {
  var querystring = document.URL.split("?")[1];
  if (querystring) {
    var params = querystring.split("&");
    for (var index = 0; (index < params.length) ; index++) {
      var current = params[index].split("=");
      if (paramName.toUpperCase() === current[0].toUpperCase()) {
        return decodeURIComponent(current[1]);
      }
    }
  }
}
