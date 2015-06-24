
$(function () {

  var hostWebUrl = getQueryStringParameter("SPHostUrl");
  var itemIds = getQueryStringParameter("SPListItemId").split(",");
  var listId = decodeURIComponent(getQueryStringParameter("SPListId"));

  Wingtip.SharePointListManager.initialize();

  for (var i = 0; i < itemIds.length; i++) {
    var itemId = itemIds[i];
    Wingtip.SharePointListManager.getItem(hostWebUrl, listId, itemId).done(function (data) {
      var itemId = data.d.Id;
      var currentTitle = data.d.Title;
      var newTitle = currentTitle.toUpperCase();
      var etag = data.d.__metadata.etag;
      var itemType = data.d.__metadata.type;
      Wingtip.SharePointListManager.updateItem(hostWebUrl, listId, itemType, itemId, newTitle, etag).done(function () {
      });
    });
  }

  sleep(3000);
  window.parent.postMessage('CloseCustomActionDialogRefresh', '*');

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

function sleep(milliseconds) {
  var start = new Date().getTime();
  for (var i = 0; i < 1e7; i++) {
    if ((new Date().getTime() - start) > milliseconds) {
      break;
    }
  }
}


