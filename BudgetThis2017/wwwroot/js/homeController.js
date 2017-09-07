(function () {
  "use strict";
  angular.module("app-budgetthis")
    .controller("homeController", homeController);

  function homeController($http) {
    var vm = this;
    vm.errorMessage = "";
    vm.home = {};
    vm.isBusy = true;

    $http.get("/api/home/")
      .then(function (response) {
        angular.copy(response.data, vm.home);
      },
      function (error) {
        vm.errorMessage = "failed to load homepage.";
      })
      .finally(function () {
        vm.isBusy = false;
      });
  }
}
)();