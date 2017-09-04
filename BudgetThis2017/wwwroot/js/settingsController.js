(function () {
  "use strict";
  angular.module("app-budgetthis")
    .controller("settingsController", settingsController);

  function settingsController($routeParams, $http) {
    var vm = this;
    vm.errorMessage = "";
    vm.settings = {};

    $http.get("/api/settings/")
      .then(function (response) {
        angular.copy(response.data, vm.settings);
      },
      function (error) {
        vm.errorMessage = "failed to load settings.";
      })
      .finally(function () {
        vm.isBusy = false;
      });

    vm.saveSettings = function () {
      vm.isBusy = true;
      vm.message = "";
      vm.errorMessage = "";
      $http.put("/api/settings/update/" + vm.settings.userId, vm.settings)
        .then(function (response) {
          vm.message = "Settings Saved.";
        }, function (error) {
          vm.errorMessage = "failed to update settings.";
        })
        .finally(function () {
          vm.isBusy = false;
        });
    };
  }
}
)();