angular.module('moneyManager.expense').controller('expensesController', ['$scope', 'expenseService', 'messageBoxService', 'eventAggregatorService', 'popupService', function ($scope, expenseService, messageBoxService, eventAggregatorService, popupService) {

    $scope.gridOptions = {
        fields: [
            { title: 'DATE', field: 'Date', filter: 'date', headerClass: 'align-left', headerStyle: {'width': '120px'} },
            { title: 'AMOUNT', field: 'Amount', filter: 'currency', headerClass: 'align-right', class: 'expense align-right', headerStyle: {'width': '100px'} },
            { title: 'DESCRIPTION', field: 'Comment', headerStyle: { 'padding-left': '40px' }, style: { 'width': '70%', 'padding-left': '40px' }},
            { title: 'CATEGORY', field: 'CategoryName', sortBy: 'Category.Name', headerClass: 'align-right', style: { 'width': '30%', 'text-align': 'right' } }
        ],
        noItemsLabel: 'NO TRANSACTIONS'
    };

    $scope.expenseParams = {
        DateFrom: new Date(2016, 9, 1),
        DateTo: new Date(),
        SortBy: "Date",
        SortAsc: false
    };

    $scope.$parent.pageName = "EXPENSES";
    $scope.$parent.titleBarClass = "title-bar-expense";
    $scope.$parent.btnAddCaption = "Add Expense";
    $scope.$parent.addFunc = function () {
        popupService.expensePopup('add', $scope.reload);
    };

    $scope.sort = function (args) {
        $scope.expenseParams.SortBy = args.f;
        $scope.expenseParams.SortAsc = args.e;
        $scope.loadExpenses();
    };
   
    $scope.expenseAction = function (e) {
        switch (e.action) {
            case 'edit':
                popupService.expensePopup('edit', $scope.reload, e.expense);
                break;
            case 'split':
                break;
            case 'delete':
                messageBoxService.showMessage('Are you sure you want to remove selected expense?', 'Delete expense', 'warning')
                    .then(function () {
                        expenseService.removeExpense(e.expense.Id).then(function () {
                            $scope.reload();
                        }, function (error) {

                        });
                    });
                break;
        }
    }

    $scope.options = {
        maintainAspectRatio: true,
        responsive: false
    };

    $scope.reload = function () {
        $scope.loadExpenses();
        $scope.loadTotals();
    }

    $scope.loadExpenses = function () {
        $scope.loadingExpenses = true;
        expenseService.getExpenses($scope.expenseParams).then(function (result) {
            $scope.expenses = result.data;
            $scope.loadingExpenses = false;
        }, function (error) {
            $scope.loadingExpenses = false;
        });
    }

    $scope.loadTotals = function () {
        $scope.loadingTotals = true;
        expenseService.getExpenseTotals().then(function (result) {
            $scope.loadingTotals = false;
            $scope.totals = result.data;
        }, function (error) {
            $scope.loadingTotals = false;
        });
    };

    (function () {
        $scope.reload();
    })();

    $scope.labels = ["Download Sales", "In-Store Sales", "Mail-Order Sales"];
    $scope.data = [300, 500, 100];
      
    $scope.labels1 = ['2006', '2007', '2008', '2009', '2010', '2011', '2012'];
    $scope.series1 = ['Series A', 'Series B'];

    $scope.data1 = [
      [65, 59, 80, 81, 56, 55, 40],
      [28, 48, 40, 19, 86, 27, 90]
    ];
}]);