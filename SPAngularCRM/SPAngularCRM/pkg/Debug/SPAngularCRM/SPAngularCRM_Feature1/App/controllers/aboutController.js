'use strict';

(function () {

  // acquire reference to app module
  var app = angular.module('AngularCRM');

  // register controller with app module
  app.controller('aboutController', aboutController );

  // implement controller function
  function aboutController($scope) {

    // (1) create object to serve as view model
    var viewModel = {
      title: "About the Angular CRM App",
      description: "The Angular CRM App is a demo app which I wrote using Bootstrap and AngularJS"
    };

    // (2) add reference to $Scope to make make view model accessible to view template
    $scope.viewModel = viewModel;
  }

})();


