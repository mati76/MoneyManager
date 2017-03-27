angular.module('moneyManager.shared').directive('dropDown', function () {
    return {
        restrict: 'E',
        templateUrl: '/app/shared/dropDown/dropDown.html',
        scope: { items: '=', css: '=' },
        link: function (scope, element, attr) {
            if (scope.items.length > 0) {
                scope.caption = scope.items[0];
            } else {
                scope.caption = "Select ...";
            }
            
            scope.select = function (item) {
                scope.caption = item;
            };
        }
    };
});