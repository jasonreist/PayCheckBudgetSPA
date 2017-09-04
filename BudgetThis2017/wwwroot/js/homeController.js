(function () {
  "use strict";
  angular.module("app-budgetthis")
    .controller("homeController", homeController);

  function homeController($http) {
    var vm = this;
    vm.errorMessage = "This page will eventually be the home/calendar view.";
  }
}
)();