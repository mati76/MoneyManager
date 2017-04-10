'use strict';

//angular.module('moneyManager.home').controller('homeController', ['$scope', 'expenseService', 'incomeService', 'budgetService', '$stateParams', '$state', 'helperService',

homeController.$inject = ['$scope', 'expenseService', 'incomeService', 'budgetService', '$stateParams', '$state', 'helperService'];
function homeController($scope, expenseService, incomeService, budgetService, $stateParams, $state, helperService) {

    $scope.$parent.pageName = "DASHBOARD";
    $scope.$parent.addFunc = null;
    $scope.chartColours = [];

    $scope.chartOptions = {
        maintainAspectRatio: true,
        responsive: false,
        cutoutPercentage: 70
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

    $scope.chart1 = { };
    $scope.chart2 = {};
    $scope.chart3 = {};

    $scope.loadBudgets = function (from, to) {
        $scope.chart3.isLoading = true;
        budgetService.getBudgetRealization(from, to).then(function (result) {
            $scope.chart3.isLoading = false;
            $scope.chart3.labels = [],
            $scope.chart3.noData = result.data.length == 0;
            $scope.chart3.series = ['Spent:', 'Left', 'Over'];
            $scope.chart3.colors = ['#3498DB', '#72C02C', '#DD2321'];

            var series1 = [];
            var series2 = [];
            var series3 = [];
            for (var i = 0; i < result.data.length; i++) {
                var item = result.data[i];
                $scope.chart3.labels.push(item.CategoryName);
                series1.push(item.Expense);
                series2.push(item.Left);
                series3.push(item.Over);
            }
            $scope.chart3.data = [series1, series2, series3];

        }, function (error) {
            $scope.chart3.isLoading = false;
        });
    };

    $scope.loadTotals = function (chart, from, to, service) {
        chart.isLoading = true;
        service.getCategoryTotals(from, to).then(function (result) {
            chart.isLoading = false;
            chart.colors = [],
            chart.legendItems = [],
            chart.labels = [],
            chart.data = [],
            chart.noData = result.data.length == 0;

            for (var i = 0; i < result.data.length; i++) {
                var item = result.data[i];
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

        $scope.loadTotals($scope.chart1, from, to, expenseService);
        $scope.loadTotals($scope.chart2, from, to, incomeService);
        $scope.loadBudgets(from, to);
    })();
}//]);

module.exports = homeController;