'use strict';

multiSelectDropDown.$inject = ['dateService', '$filter', '$state', '$stateParams'];
function multiSelectDropDown(dateService, $filter, $state, $stateParams) {
    return {
        templateUrl: '/app/shared/directives/multiSelectDropDown/multiSelectDropDown.html',
        restrict: 'E',
        scope: { title: '=', items: '=', applyChanges: '&' },
        link: function (scope, element, attr) {
            scope.saved = false;

            scope.status = {
                isopen: false
            };

            scope.toggled = function (isOpen) {
                if (isOpen) {
                    scope.saved = false;
                } else if(!scope.saved){
                    angular.forEach(scope.items, function (item) {
                        if (item.isDirty) {
                            item.isSelected = !item.isSelected;
                            item.isDirty = false;
                        }
                    });
                }
            };

            scope.toggleDropdown = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                scope.status.isopen = !scope.status.isopen;
                scope.applyChanges();
                scope.saved = true;
                angular.forEach(scope.items, function (item) {
                    item.isDirty = false;
                });
            };
        }
    }
}

module.exports = multiSelectDropDown;