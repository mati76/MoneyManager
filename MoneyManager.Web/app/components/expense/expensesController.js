angular.module('moneyManager.expense').controller('expensesController', ['$scope', '$state', '$stateParams', 'expenseService', 'messageBoxService', 'expensePopupService', 'eventAggregatorService', 'helperService', function ($scope, $state, $stateParams, expenseService, messageBoxService, expensePopupService, eventAggregatorService, helperService) {

    $scope.gridOptions = {
        fields: [
            { title: 'DATE', field: 'Date', filter: 'date', headerClass: 'align-left', headerStyle: {'width': '120px'} },
            { title: 'AMOUNT', field: 'Amount', filter: 'currency', headerClass: 'align-right', class: 'expense align-right', headerStyle: {'width': '100px'} },
            { title: 'DESCRIPTION', field: 'Comment', headerStyle: { 'padding-left': '40px' }, style: { 'width': '70%', 'padding-left': '40px' }},
            { title: 'CATEGORY', field: 'CategoryName', sortBy: 'Category.Name', headerClass: 'align-right', style: { 'width': '30%', 'text-align': 'right' } }
        ],
        label: 'TRANSACTIONS:',
        noItemsLabel: 'NO TRANSACTIONS',
        singleSelectActions: [
            { label: 'EDIT', css: 'grid-btn-edit', callback: function (item) { $scope.editExpense(item); } },
            { label: 'SPLIT', css: 'grid-btn-split', callback: function (item) { alert("SPLIT: " + item.Id); } },
            { label: 'REPEAT', css: 'grid-btn-repeat', callback: function (item) { alert("REPEAT: " + item.Id); } },
            { label: 'DELETE', css: 'grid-btn-delete', callback: function (item) { $scope.deleteExpense(item); } }
        ],
        multiSelectActions: [
            { label: 'REPEAT', css: 'grid-btn-repeat', callback: function () { alert("REPEAT multi"); } },
            { label: 'DELETE', css: 'grid-btn-delete', callback: function () { alert("DELETE multi"); } }
        ]
    };

    $scope.expenses = [];
    $scope.chartColours = [];
    $scope.$parent.pageName = "EXPENSES";
    $scope.$parent.titleBarClass = "title-bar-expense";
    $scope.$parent.btnAddCaption = "Add Expense";
    $scope.$parent.addFunc = function () {
        expensePopupService.expensePopup('add', $scope.reload, expenseService.saveExpense);
    };

    $scope.chartOptions = {
        maintainAspectRatio: true,
        responsive: false,
        cutoutPercentage: 70
    };

    $scope.chart1 = {
        loadDetails: function (categoryId) {
            $scope.loadCategoryTotals($scope.chart2, categoryId);
        }
    };

    $scope.chart2 = {};

    $scope.loadNextExpenses = function () {
        $scope.loadExpenses();
        $scope.expenseParams.Skip += $scope.expenseParams.Take;
    };

    $scope.sort = function (args) {
        $scope.expenseParams.SortBy = args.f;
        $scope.expenseParams.SortAsc = args.e;
        $scope.expenseParams.Skip = 0;
        $scope.expenses = [];
        $scope.loadExpenses();

        $state.go($state.$current.name, {
            from: $scope.expenseParams.DateFrom.yyyymmdd(),
            to: $scope.expenseParams.DateTo.yyyymmdd(),
            sort: args.f,
            asc: args.e ? 'asc' : 'desc'
        }, {notify: false});
    };

    $scope.showDetailsChart = function (legendItem) {
        $scope.detailsChartCategoryId = legendItem.categoryId;
        $scope.detailsChartCategoryName = legendItem.label.toUpperCase();
        $scope.loadCategoryTotals($scope.chart2, legendItem.categoryId);
    };
   
    $scope.editExpense = function (expense) {
        expensePopupService.expensePopup('edit', $scope.reload, expenseService.saveExpense, expense);
    };

    $scope.deleteExpense = function (expense) {
        messageBoxService.showMessage('Are you sure you want to remove selected expense?', 'Delete expense', 'warning')
        .then(function () {
            expenseService.removeExpense(expense.Id).then(function () {
                $scope.reload();
            }, function (error) {

            });
        });
    };

    $scope.loadTotals = function () {
        $scope.loadingTotals = true;
        expenseService.getExpenseTotals().then(function (result) {
            $scope.loadingTotals = false;
            $scope.totals = result.data;
        }, function (error) {
            $scope.loadingTotals = false;
        });
    };

    $scope.reload = function () {
        $scope.loadExpenses();
        $scope.loadTotals();
        $scope.loadCategoryTotals($scope.chart1);
    };

    $scope.loadExpenses = function () {
        $scope.loadingExpenses = true;
        expenseService.getExpenses($scope.expenseParams).then(function (result) {
            Array.prototype.push.apply($scope.expenses, result.data);
            $scope.loadingExpenses = false;
        }, function (error) {
            $scope.loadingExpenses = false;
        });
    };

    $scope.loadCategoryTotals = function (chart, categoryId) {
        chart.isLoading = true;
        $scope.chart2.isLoading = true;
        expenseService.getCategoryTotals($scope.expenseParams.DateFrom, $scope.expenseParams.DateTo, categoryId).then(function (result) {
            chart.isLoading = false;
            chart.colors = [],
            chart.legendItems = [],
            chart.labels = [],
            chart.data = [],
            chart.total = 0
            chart.noData = result.data.length == 0;

            for (var i = 0; i < result.data.length; i++) {
                var item = result.data[i];
                chart.total += item.TotalAmount;
                chart.legendItems.push({
                    categoryId: item.CategoryId,
                    label: item.CategoryName,
                    percent: item.Percent,
                    color: helperService.getColor(i),
                    amount: item.TotalAmount
                });
                chart.labels.push(item.CategoryName);
                chart.data.push(item.TotalAmount);
                chart.colors.push(helperService.getColor(i))
            }

            if (chart.loadDetails) {
                if (chart.data.length > 0) {
                    $scope.detailsChartCategoryId = result.data[0].CategoryId;
                    $scope.detailsChartCategoryName = result.data[0].CategoryName.toUpperCase();
                    chart.loadDetails(result.data[0].CategoryId)
                } else {
                    $scope.detailsChartCategoryName = '';
                    chart.loadDetails(0)
                }
            }
        }, function (error) {
            $scope.loadingTotals = false;
            chart.isLoading = false;
        });
    };

    (function () {
        var from = $stateParams.from;
        var to = $stateParams.to;
        from = new Date(from.substring(0, 4), from.substring(4, 6) - 1, from.substring(6, 8));
        to = new Date(to.substring(0, 4), to.substring(4, 6) - 1, to.substring(6, 8));

        $scope.expenseParams = {
            DateFrom: from,
            DateTo: to,
            SortBy: $stateParams.sort,
            SortAsc: $stateParams.asc == 'asc',
            Take: 50,
            Skip: 0
        };

        $scope.loadTotals();
        $scope.loadCategoryTotals($scope.chart1);
    })();
}]);