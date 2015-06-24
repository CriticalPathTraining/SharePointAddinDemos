'use strict';

(function () {

  var app = angular.module('AngularCRM');

  app.controller('homeController', homeController );

  function homeController($scope, wingtipCrmService) {
    wingtipCrmService.getCustomers().success(function (data) {
      $scope.customers = data.d.results;
      $scope.deleteCustomer = function (id) {
        wingtipCrmService.deleteCustomer(id).success(function (data) {
          $scope.$apply();
        });
      }
    });
  }

})();