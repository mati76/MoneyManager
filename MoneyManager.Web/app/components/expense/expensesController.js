angular.module('moneyManager.expense').controller('expensesController', ['$scope', 'expenseService', 'eventAggregatorService', 'popupService', function ($scope, expenseService, eventAggregatorService, popupService) {

    $scope.gridOptions = {
        fields: [
            { title: 'DATE', field: 'Date' },
            { title: 'AMOUNT', field: 'Amount' },
            { title: 'DESCRIPTION', field: 'Comment' },
            { title: 'CATEGORY', field: 'Category.Name' }
        ]
    };

    $scope.$parent.pageName = "EXPENSES";
    $scope.$parent.titleBarClass = "title-bar-expense";
    $scope.$parent.btnAddCaption = "Add Expense";
    $scope.$parent.addFunc = function () {
        popupService.expensePopup('add');
    };

    $scope.sortColumn = "Date";
    $scope.sortAscending = false;
   
    $scope.isSortedBy = function (field) {
        return $scope.sortColumn == field ? $scope.sortAscending  : undefined;
    }

    $scope.multipleChecked = function () {
        var cnt = 0;
        if ($scope.expenses == null) {
            return false;
        }

        $scope.expenses.forEach(function (expense) {
            if (expense.checked) {
                cnt++;
            }
            if (cnt > 1) {
                return true;
            }
        });
        $scope.checkedAll = cnt == $scope.expenses.length;
        return cnt > 1;
    };

    $scope.checkAll = function (args) {
        $scope.expenses.forEach(function (e) {
            e.checked = args.isChecked;
        });
    };

    $scope.sort = function (e) {
        $scope.sortColumn = e.f;
        $scope.sortAscending = e.e;
        $scope.onLoad();
    };
   
    $scope.expenseAction = function (e) {
        switch (e.action) {
            case 'edit':
                popupService.expensePopup('edit', $scope.onLoad, e.expense);
                break;
            case 'split':
                break;
            case 'delete':
                break;
        }
    }

    $scope.selectExpense = function (e) {
        e.isSelected = !e.isSelected;
        $scope.expenses.forEach(function (expense) {
            if (expense.Id != e.Id) {
                expense.isSelected = false;
            }
        });
    };

    $scope.options = {
        maintainAspectRatio: true,
        responsive: false
    };

    $scope.onLoad = function () {
        var criteria = {
            DateFrom: new Date(2016,9,1),
            DateTo: new Date(),
            SortBy: $scope.sortColumn,
            SortAsc: $scope.sortAscending
        };
        expenseService.getExpenses(criteria).then(function (result) {
            $scope.expenses = result.data;
            eventAggregatorService.publishEvent(eventAggregatorService.eventNames.loadingFinished)
        }, function (error) {

        });

        expenseService.getExpenseTotals().then(function (result) {
            $scope.totals = result.data;
            eventAggregatorService.publishEvent(eventAggregatorService.eventNames.loadingFinished)
        }, function (error) {

        });
    };

    (function () {
        eventAggregatorService.publishEvent(eventAggregatorService.eventNames.loadingStarted)

        $scope.onLoad();
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