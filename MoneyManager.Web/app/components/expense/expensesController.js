angular.module('moneyManager.expense').controller('expensesController', ['$scope', '$uibModal', 'expenseService', 'eventAggregatorService', function ($scope, $uibModal, expenseService, eventAggregatorService) {

    $scope.$parent.pageName = "EXPENSES";
    $scope.$parent.titleBarClass = "title-bar-expense";
    $scope.$parent.btnAddCaption = "Add Expense";
    $scope.$parent.addFunc = function () {
        expensePopup('add');
    };

    function expensePopup(mode, expense) {
        var modalInstance = $uibModal.open({
            animation: true,
            size: 'lg',
            templateUrl: '/app/components/expense/expense.html',
            controller: 'expenseController',
            resolve: {
                params: { dialogMode: mode, expense: expense }
            }
        });

        modalInstance.result.then(function () {
            //reload();
        });
    }

    $scope.options = {
        maintainAspectRatio: true,
        responsive: false
    };



    (function () {
        eventAggregatorService.publishEvent(eventAggregatorService.eventNames.loadingStarted)
        expenseService.getExpenseTotals().then(function (result) {
            $scope.totals = result.data;
            eventAggregatorService.publishEvent(eventAggregatorService.eventNames.loadingFinished)
        }, function (error) {

        });
    })();

    $scope.expenses = [
        { Date: '2016-04-10', Amount: 15.50, Description: 'Apteka - lekarstwa', Category: "Lekarstwa" },
        { Date: '2016-04-11', Amount: 10.20, Description: 'myjnia', Category: "Samochód" },
        { Date: '2016-04-12', Amount: 35, Description: '', Category: "Żywność" },
        { Date: '2016-04-12', Amount: 200.50, Description: 'Apteka - lekarstwa', Category: "Lekarstwa" },
        { Date: '2016-04-14', Amount: 55.20, Description: 'Apteka - lekarstwa', Category: "Zabawki" },
        { Date: '2016-04-15', Amount: 5.60, Description: '', Category: "Żywność" },
        { Date: '2016-04-15', Amount: 17.50, Description: '', Category: "Żywność" },
        { Date: '2016-04-16', Amount: 29.90, Description: 'Rossmann', Category: "Chemia" },
        { Date: '2016-04-16', Amount: 19, Description: '', Category: "Żywność" },
        { Date: '2016-04-17', Amount: 130, Description: 'Bluzka i biustonosz', Category: "Ciuchy" }
    ];

    $scope.labels = ["Download Sales", "In-Store Sales", "Mail-Order Sales"];
    $scope.data = [300, 500, 100];
      
    $scope.labels1 = ['2006', '2007', '2008', '2009', '2010', '2011', '2012'];
    $scope.series1 = ['Series A', 'Series B'];

    $scope.data1 = [
      [65, 59, 80, 81, 56, 55, 40],
      [28, 48, 40, 19, 86, 27, 90]
    ];
}]);