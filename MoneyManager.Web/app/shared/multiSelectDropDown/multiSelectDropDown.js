angular.module('moneyManager.shared').directive('multiDropDown', function (dateService, messageBoxService, $filter, $state, $stateParams) {
    return {
        templateUrl: '/app/shared/multiSelectDropDown/multiSelectDropDown.html',
        restrict: 'E',
        scope: { title: '=', items: '=' },
        link: function (scope, element, attr) {
            scope.status = {
                isopen: false
            };

            scope.toggleDropdown = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                scope.status.isopen = !scope.status.isopen;
            };
        }
    }
});