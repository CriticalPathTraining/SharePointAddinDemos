"use strict"

var Wingtip = window.Wingtip || {};

Wingtip.SharePointListManager = function () {

  var requestDigest;

  var initialize = function () {

    $.ajax({
      url: "../_api/contextinfo",
      type: "POST",
      headers: { "accept": "application/json;odata=verbose" }
    }).then(function (data) {
      requestDigest = data.d.GetContextWebInformation.FormDigestValue      
    });

  }

  var getItem = function (hostWebUrl, listId, itemId) {

    listId = listId.replace("{", "").replace("}", "");

    var requestUri = "../_api/SP.AppContextSite(@target)/Web/Lists(guid'" + listId + "')/items(" + itemId + ")/" +
                       "?$select=Id,Title" +
                       "&@target='" + hostWebUrl + "'";

    // send call across network
    return $.ajax({
      url: requestUri,
      headers: { "accept": "application/json;odata=verbose" }
    });

  };

  var updateItem = function (hostWebUrl, listId, itemType, itemId, title, etag) {

    listId = listId.replace("{", "").replace("}", "");

    var requestUri = "../_api/SP.AppContextSite(@target)/Web/Lists(guid'" + listId + "')/items(" + itemId + ")/" +
                   "?@target='" + hostWebUrl + "'";

    var requestHeaders = {
      "accept": "application/json;odata=verbose",
      "X-HTTP-Method": "PATCH",
      "X-RequestDigest": requestDigest,
      "If-Match": etag
    }

    var itemData = {
      __metadata: { "type": itemType },
      Title: title
    };

    var requestBody = JSON.stringify(itemData);

    return $.ajax({
      url: requestUri,
      type: "POST",
      contentType: "application/json;odata=verbose",
      headers: requestHeaders,
      data: requestBody,
    });

  };

  // public interface
  return {
    initialize: initialize,
    getItem: getItem,
    updateItem: updateItem
  };

}();