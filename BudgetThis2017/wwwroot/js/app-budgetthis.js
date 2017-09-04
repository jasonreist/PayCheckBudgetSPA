(function () {
  "use strict";
  angular.module("app-budgetthis", ["ngRoute"])
  .config(function ($routeProvider) {

    $routeProvider.when("/", {
      controller: "homeController",
      controllerAs: "vm",
      templateUrl: "/views/home.html"
    });

    $routeProvider.when("/bills/", {
      controller: "billsController",
      controllerAs: "vm",
      templateUrl: "/views/bills.html"
    });

    $routeProvider.when("/settings/", {
      controller: "settingsController",
      controllerAs: "vm",
      templateUrl: "/views/settings.html"
    });

    $routeProvider.when("/bill/:billId", {
      controller: "billController",
      controllerAs: "vm",
      templateUrl: "/views/bill.html"
    });

    $routeProvider.otherwise({ redirectTo: "/" });
  });
}
)();