angular.module('moneyManager.budget').controller('budgetController', ['$scope', 'budgetService', 'popupService', '$stateParams', '$state', 'helperService', 'messageBoxService',
    function ($scope, budgetService, popupService, $stateParams, $state, helperService, messageBoxService) {

    $scope.$parent.pageName = "BUDGET";
    $scope.$parent.titleBarClass = "title-bar-budget";
    $scope.$parent.btnAddCaption = "Add Planned Expense";
    $scope.$parent.addFunc = function () {
        popupService.popup('add', {
            callback: $scope.reload,
            windowClass: 'expence-modal',
            templateUrl: '/app/components/expense/expense.html',
            controller: 'expenseController',
            saveFunc: budgetService.saveExpense
        })
    };
    $scope.chartColours = [];
    $scope.chart = {};

    $scope.gridOptions = {
        fields: [
            { title: 'DATE', field: 'Date', filter: 'date', headerClass: 'align-left', headerStyle: { 'width': '120px' } },
            { title: 'AMOUNT', field: 'Amount', filter: 'currency', headerClass: 'align-right', class: 'expense align-right', headerStyle: { 'width': '100px' } },
            { title: 'DESCRIPTION', field: 'Comment', headerStyle: { 'padding-left': '40px' }, style: { 'width': '70%', 'padding-left': '40px' } },
            { title: 'CATEGORY', field: 'CategoryName', sortBy: 'CategoryName', headerClass: 'align-right', class: 'align-right', style: { 'width': '30%', 'text-align': 'right' } }
        ],
        label: 'PLANNED TRANSACTIONS:',
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

    $scope.barChartOptions = {
        legend: { labels: { fontColor: "black", fontSize: 18 } },
        maintainAspectRatio: true,
        responsive: true,
        tooltips: {
            callbacks: {
                label: function (tooltipItem, data) {
                    var value = tooltipItem.xLabel;
                    var label = data.datasets[tooltipItem.datasetIndex].label;
                    return value > 0 ? label + ' ' + value : '';
                }
            }
        },
        scales: {
            xAxes: [{
                stacked: true
            }],
            yAxes: [{
                stacked: true,
                ticks: {
                    fontColor: "black",
                },
                gridLines: {
                    display: false,
                },
            }]
        }
    };

    $scope.sort = function (args) {
        $scope.pageParams.SortBy = args.f;
        $scope.pageParams.SortAsc = args.e;

        $state.go($state.$current.name, {
            from: $scope.pageParams.DateFrom.yyyymmdd(),
            to: $scope.pageParams.DateTo.yyyymmdd(),
            sort: args.f,
            asc: args.e ? 'asc' : 'desc'
        }, { notify: false });
    };

    $scope.editExpense = function (expense) {
        popupService.popup('edit', {
            callback: $scope.reload,
            windowClass: 'expence-modal',
            templateUrl: '/app/components/expense/expense.html',
            controller: 'expenseController',
            saveFunc: budgetService.saveExpense
        }, expense)
    };

    $scope.deleteExpense = function (expense) {
        messageBoxService.showMessage('Are you sure you want to remove selected expense?', 'Delete expense', 'warning')
        .then(function () {
            budgetService.removeExpense(expense.Id).then(function () {
                $scope.reload();
            }, function (error) {

            });
        });
    };

    $scope.loadTotals = function () {
        $scope.loadingTotals = true;
        budgetService.getBudgetTotals($scope.pageParams.DateFrom, $scope.pageParams.DateTo).then(function (result) {
            $scope.loadingTotals = false;
            $scope.totals = result.data;
        }, function (error) {
            $scope.loadingTotals = false;
        });
    };

    $scope.loadBudgetRealization = function () {
        $scope.chart.isLoading = true;
        budgetService.getBudgetRealization($scope.pageParams.DateFrom, $scope.pageParams.DateTo).then(function (result) {
            $scope.chart.isLoading = false;
            $scope.chart.labels = [],
            $scope.chart.noData = result.data.length == 0;
            $scope.chart.series = ['Spent:', 'Left', 'Over'];
            $scope.chart.colors = ['#3498DB', '#72C02C', '#DD2321'];

            var series1 = [];
            var series2 = [];
            var series3 = [];
            for (var i = 0; i < result.data.length; i++) {
                var item = result.data[i];
                $scope.chart.labels.push(item.CategoryName);
                series1.push(item.Expense);
                series2.push(item.Left);
                series3.push(item.Over);
            }
            $scope.chart.data = [series1, series2, series3];

        }, function (error) {
            $scope.chart.isLoading = false;
        });
    };

    $scope.reload = function () {
        $scope.loadBudgetExpenses();
        $scope.loadBudgetRealization();
        $scope.loadTotals();
    };

    $scope.loadBudgetExpenses = function () {
        $scope.loadingExpenses = true;
        budgetService.getExpenses($scope.pageParams).then(function (result) {
            $scope.expenses = result.data;
            $scope.loadingExpenses = false;
        }, function (error) {
            $scope.loadingExpenses = false;
        });
    };

    (function () {
        var from = $stateParams.from;
        var to = $stateParams.to;
        from = new Date(from.substring(0, 4), from.substring(4, 6) - 1, from.substring(6, 8));
        to = new Date(to.substring(0, 4), to.substring(4, 6) - 1, to.substring(6, 8));

        $scope.pageParams = {
            DateFrom: from,
            DateTo: to,
            SortBy: $stateParams.sort,
            SortAsc: $stateParams.asc == 'asc'
        };

        $scope.reload();
    })();

}]);
