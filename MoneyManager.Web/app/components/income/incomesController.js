angular.module('moneyManager.income').controller('incomesController', ['$scope', '$state', '$stateParams', 'incomeService', 'messageBoxService', 'incomePopupService', 'eventAggregatorService', 'helperService', function ($scope, $state, $stateParams, incomeService, messageBoxService, incomePopupService, eventAggregatorService, helperService) {
    $scope.$parent.pageName = "INCOMES";

    $scope.gridOptions = {
        fields: [
            { title: 'DATE', field: 'Date', filter: 'date', headerClass: 'align-left', headerStyle: { 'width': '120px' } },
            { title: 'AMOUNT', field: 'Amount', filter: 'currency', headerClass: 'align-right', class: 'income align-right', headerStyle: { 'width': '100px' } },
            { title: 'DESCRIPTION', field: 'Comment', headerStyle: { 'padding-left': '40px' }, style: { 'width': '70%', 'padding-left': '40px' } },
            { title: 'CATEGORY', field: 'CategoryName', sortBy: 'Category.Name', headerClass: 'align-right', style: { 'width': '30%', 'text-align': 'right' } }
        ],
        noItemsLabel: 'NO TRANSACTIONS'
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

    $scope.$parent.titleBarClass = "title-bar-income";
    $scope.$parent.btnAddCaption = "Add Income";
    $scope.$parent.addFunc = function () {
        incomePopupService.incomePopup('add', $scope.reload);
    };

    $scope.sort = function (args) {
        $scope.incomeParams.SortBy = args.f;
        $scope.incomeParams.SortAsc = args.e;
        $scope.loadIncomes();

        $state.go($state.$current.name, {
            from: $scope.incomeParams.DateFrom.yyyymmdd(),
            to: $scope.incomeParams.DateTo.yyyymmdd(),
            sort: args.f,
            asc: args.e ? 'asc' : 'desc'
        }, { notify: false });
    };

    $scope.showDetailsChart = function (legendItem) {
        $scope.detailsChartCategoryId = legendItem.categoryId;
        $scope.detailsChartCategoryName = legendItem.label.toUpperCase();
        $scope.loadCategoryTotals($scope.chart2, legendItem.categoryId);
    };

    $scope.incomeAction = function (e) {
        switch (e.action) {
            case 'edit':
                incomePopupService.incomePopup('edit', $scope.reload, e.income);
                break;
            case 'split':
                break;
            case 'delete':
                messageBoxService.showMessage('Are you sure you want to remove selected income?', 'Delete income', 'warning')
                    .then(function () {
                        incomeService.removeExpense(e.income.Id).then(function () {
                            $scope.reload();
                        }, function (error) {

                        });
                    });
                break;
        }
    };

    $scope.reload = function () {
        $scope.loadIncomes();
        $scope.loadTotals();
        $scope.loadCategoryTotals($scope.chart1);
    };

    $scope.loadIncomes = function () {
        $scope.loadingIncomes = true;
        incomeService.getIncomes($scope.incomeParams).then(function (result) {
            $scope.incomes = result.data;
            $scope.loadingIncomes = false;
        }, function (error) {
            $scope.loadingIncomes = false;
        });
    };

    $scope.loadCategoryTotals = function (chart, categoryId) {
        chart.isLoading = true;
        $scope.chart2.isLoading = true;
        incomeService.getCategoryTotals($scope.incomeParams.DateFrom, $scope.incomeParams.DateTo, categoryId).then(function (result) {
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

        $scope.incomeParams = {
            DateFrom: from,
            DateTo: to,
            SortBy: $stateParams.sort,
            SortAsc: $stateParams.asc == 'asc'
        };

        $scope.loadIncomes();
        $scope.loadCategoryTotals($scope.chart1);
    })();
}]);