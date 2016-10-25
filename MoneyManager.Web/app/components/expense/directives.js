angular.module('moneyManager.expense').directive('expenseTotals', function () {
    return {
        restrict: 'E',
        template: '<p class="summary-label semi-bold">{{label}}</p>' +
            '<p ng-hide=\"isLoading\" class="summary-value expense-value" ng-cloak> {{ (value | currency : \' \') + \" PLN\" }}</p>' +
            '<loading-panel ng-show=\"isLoading\" width=\"16\" heigt=\"16\" is-loading=\"true\"><loading-panel>',
        scope: { value: '=', isLoading: '=' },
        link: function (scope, element, attr) {
            scope.label = attr.label;
        }
    }
});