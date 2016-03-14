angular.module('moneyManager.expense').controller('expensesController', ['$scope', '$uibModal', function ($scope, $uibModal) {

    $scope.$parent.pageName = "EXPENSES";
    $scope.$parent.addBtnClass = "title-bar-expense";
    $scope.$parent.addFunc = function () {
        expensePopup('add');
    };

    function expensePopup(mode, expense) {
        var modalInstance = $uibModal.open({
            animation: true,
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

    

    $scope.labels = ["Download Sales", "In-Store Sales", "Mail-Order Sales"];
    $scope.data = [300, 500, 100];
      
    $scope.labels1 = ['2006', '2007', '2008', '2009', '2010', '2011', '2012'];
    $scope.series1 = ['Series A', 'Series B'];

    $scope.data1 = [
      [65, 59, 80, 81, 56, 55, 40],
      [28, 48, 40, 19, 86, 27, 90]
    ];
}]);