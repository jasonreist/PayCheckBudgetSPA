(function () {
  "use strict";
  angular.module("app-budgetthis")
    .controller("billController", billController);

  function billController($routeParams, $http) {
    var vm = this;
    vm.bill = {};
    vm.billId = $routeParams.billId;
    vm.errorMessage = "";
    vm.message = "";
    vm.isBusy = true;
    vm.suffix = "";
    vm.newCustomBill = {};
    vm.iconOptions = [
      { id: "fa fa-credit-card", name: "fa fa-credit-card" },
      { id: "fa fa-car", name: "fa fa-car" },
      { id: "fa fa-phone", name: "fa fa-phone" },
      { id: "fa fa-tv", name: "fa fa-tv" },
      { id: "fa fa-home", name: "fa fa-home" },
      { id: "fa fa-asterisk", name: "fa fa-asterisk" },
      { id: "fa fa-lightbulb-o", name: "fa fa-lightbulb-o" }
    ];

    vm.getSuffix = function (day) {
      day = day == "0" ? $('#dueDay').val() : day;
      switch (day) {
        case "1": vm.suffix = "st of the month"; break;
        case "21": vm.suffix = "st of the month"; break;
        case "31": vm.suffix = "st of the month"; break;
        case 1: vm.suffix = "st of the month"; break;
        case 21: vm.suffix = "st of the month"; break;
        case 31: vm.suffix = "st of the month"; break;

        case "2": vm.suffix = "nd of the month"; break;
        case "22": vm.suffix = "nd of the month"; break;
        case 2: vm.suffix = "nd of the month"; break;
        case 22: vm.suffix = "nd of the month"; break;

        case "3": vm.suffix = "rd of the month"; break;
        case "23": vm.suffix = "rd of the month"; break;
        case 3: vm.suffix = "rd of the month"; break;
        case 23: vm.suffix = "rd of the month"; break;

        default: vm.suffix = "th of the month"; break;
      }
    };

    $http.get("/api/bills/" + vm.billId)
      .then(function (response) {
        angular.copy(response.data, vm.bill);
      },
        function (error) {
          vm.errorMessage = "failed to load bill.";
        })
      .finally(function () {
        vm.isBusy = false;
        vm.getSuffix(vm.bill.dueDay);
      });

    vm.saveBill = function () {
      vm.isBusy = true;
      vm.message = "";
      vm.errorMessage = "";
      $http.put("/api/bills/update/" + vm.billId, vm.bill)
        .then(function (response) {
          vm.message = "Bill Saved.";
        }, function (error) {
          vm.errorMessage = "failed to update bill.";
        })
        .finally(function () {
          vm.isBusy = false;
        });
    };

    vm.createCustomBill = function () {
      vm.isBusy = true;
      vm.message = "";
      vm.errorMessage = "";
      vm.newCustomBill.billId = vm.bill.id;
      $http.post("api/custombills/", vm.newCustomBill)
        .then(function (response) {
          vm.bill.customBills.push(response.data);
          vm.message = "Custom Bill Created.";
          vm.newCustomBill = {};
        }, function (error) {
          vm.errorMessage = "failed to create custom bill.";
        })
        .finally(function () {
          vm.isBusy = false;
        });
    };

    vm.updateCustomBill = function () {
      vm.isBusy = true;
      vm.message = "";
      vm.errorMessage = "";
      $http.post("api/custombills/", vm.bill)
        .then(function (response) {
          vm.message = "Custom Bill Saved.";
        }, function (error) {
          vm.errorMessage = "failed to update custom bill.";
        })
        .finally(function () {
          vm.isBusy = false;
        });
    };

    vm.removeCustomBill = function (data) {
      vm.isBusy = true;
      vm.message = "";
      vm.errorMessage = "";
      $http.delete("/api/custombills/delete/" + data.id)
        .then(function (data) {
          var index = vm.bill.customBills.indexOf(data);
          vm.bill.customBills.splice(index, 1);
        }, function (error) {
          vm.errorMessage = "failed to delete custom bill.";
        })
        .finally(function () {
          vm.message = "Custom Bill Removed.";
          vm.isBusy = false;
        });
    };
    

  }
}
)();