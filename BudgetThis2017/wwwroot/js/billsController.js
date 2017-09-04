(function () {
  "use strict";
  angular.module("app-budgetthis")
    .controller("billsController", billsController);

  function billsController($http, $filter) {
    var vm = this;

    vm.bills = [];
    vm.errorMessage = "";
    vm.isBusy = true;
    vm.newBill = {};
    vm.billId = 0;

    var url = "/api/bills/";

    $http.get(url)
      .then(function (response) {
        angular.copy(response.data, vm.bills);
      },
      function (error) {
        vm.errorMessage = "failed to load bills.";
      })
      .finally(function () {
        vm.isBusy = false;
      });

    vm.addBill = function () {
      vm.isBusy = true;
      $http.post(url, vm.newBill)
        .then(function (response) {
          
          var b = $filter("filter")(vm.bills, { dueDay: vm.newBill.dueDay })[0];
          console.log(b);
          var i = vm.bills.indexOf(b);
          console.log(i);

          //vm.bills.push(response.data);
          vm.bills.splice(i, 0, response.data);

          vm.newBill = {}; //clear form
        }, function (error) {
          vm.errorMessage = "failed to create bill.";
        })
        .finally(function () {
          vm.isBusy = false;
        });
    };

    vm.deleteBill = function (data) {
      vm.isBusy = true;
      $http.delete(url + "delete/" + data.id)
        .then(function () {
          var index = vm.bills.indexOf(data);
          vm.bills.splice(index, 1);
        }, function (error) {
          vm.errorMessage = "failed to delete bill.";
        })
        .finally(function () {
          vm.isBusy = false;
        });
    };


    vm.getSuffix = function (day) {
      switch (day) {
        case "1": return "st"; break;
        case "21": return "st"; break;
        case "31": return "st"; break;
        case 1: return "st"; break;
        case 21: return "st"; break;
        case 31: return "st"; break;

        case "2": return "nd"; break;
        case "22": return "nd"; break;
        case 2: return "nd"; break;
        case 22: return "nd"; break;

        case "3": return "rd"; break;
        case "23": return "rd"; break;
        case 3: return "rd"; break;
        case 23: return "rd"; break;

        default: return "th"; break;
      }
    };

  }
})();