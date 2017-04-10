'use strict';

function totalsLabel() {
    return {
        restrict: 'E',
        template: '<p class="summary-label semi-bold">{{label}}</p>' +
            '<p ng-hide=\"isLoading\" ng-class="class" class="summary-value" ng-cloak> {{ (value | currency : \' \')}} {{suffix}}</p>' +
            '<loading-panel ng-show=\"isLoading\" width=\"16\" heigt=\"16\" is-loading=\"true\"><loading-panel>',
        scope: { value: '=', isLoading: '=', type: '=' },
        link: function (scope, element, attr) {
            applyStyle();
            scope.label = attr.label;

            scope.$watch('value', function (newVal, oldVal) {
                if (newVal != oldVal) {
                    applyStyle();
                }
            });

            function applyStyle() {
                switch (scope.type) {
                    case 'expense':
                        scope.class = 'expense-value';
                        scope.suffix = 'PLN';
                        break;
                    case 'income':
                        scope.class = 'income-value';
                        scope.suffix = 'PLN';
                        break;
                    case 'balance':
                        scope.class = scope.value >= 0 ? 'income-value' : 'expense-value';
                        scope.suffix = 'PLN';
                        break;
                    case 'percentage':
                        scope.class = scope.value >= 0 ? 'income-value' : 'expense-value';
                        scope.suffix = '%';
                        break;
                }
            };
        }
    }
}

module.exports = totalsLabel;