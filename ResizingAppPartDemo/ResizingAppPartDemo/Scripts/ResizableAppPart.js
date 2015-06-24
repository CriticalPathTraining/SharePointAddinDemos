'use strict';

$(function(){
  $("#cmdGetSmall").click(onGetSmall);
  $("#cmdGetBig").click(onGetBig);

});


function onGetSmall() {
  var SPHostUrl = getQueryStringParameter("SPHostUrl");
  var senderId = getQueryStringParameter("SenderId");
  var message = "<message senderId=" + senderId + ">resize(250, 250)</message>";
  parent.postMessage(message, SPHostUrl);
}

function onGetBig() {
  var SPHostUrl = getQueryStringParameter("SPHostUrl");
  var senderId = getQueryStringParameter("SenderId");
  var message = "<message senderId=" + senderId + ">resize(800, 400)</message>";
  parent.postMessage(message, SPHostUrl);
}

var getQueryStringParameter = function (param) {
  var querystring = document.URL.split("?")[1];
  if (querystring) {
    var params = querystring.split("&");
    for (var index = 0; (index < params.length) ; index++) {
      var current = params[index].split("=");
      if (param.toUpperCase() === current[0].toUpperCase()) {
        return decodeURIComponent(current[1]);
      }
    }
  }
}
