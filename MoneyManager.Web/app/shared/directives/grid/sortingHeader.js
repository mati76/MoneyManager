'use strict';

function sortingHeader() {
    return {
        template: '<div ng-click="changeSorting()" style="cursor: pointer"><span ng-transclude></span><img ng-src="../../../icons/icons-sorting{{sortingIcon}}.png" style="margin-left: 10px"/></div>',
        restrict: 'E',
        transclude: true,
        scope: { disabled: '=', sorted: '@', onSort: '&' },
        link: function (scope, element, attrs) {
            scope.changeSorting = function () {
                scope.asc = !scope.asc;
                scope.setIcon(scope.asc);
                scope.onSort({ asc: scope.asc });
            }

            scope.$watch('disabled', function (newval) {
                if (newval != null) {
                    scope.asc = scope.sorted === "false" ? false : (scope.sorted == "" ? null : true);
                    scope.setIcon(scope.asc);
                }
            });

            scope.$watch('sorted', function (newval, oldVal) {
                scope.asc = scope.sorted === "false" ? false : (scope.sorted == "" ? null : true);
                scope.setIcon(scope.asc);
            });

            scope.setIcon = function (asc) {
                if (scope.disabled != true) {
                    scope.sortingIcon = asc ? 'Up' : (asc == null ? 'Nothing' : 'Down');
                }
            }
        }
    }
}

module.exports = sortingHeader;