angular.module('moneyManager.shared').directive('checkBox', function () {
    return {
        template: '<div class="check-box" ng-class="{\'glyphicon glyphicon-ok\': isChecked == true }" aria-hidden="true" ng-click="onclick()"/>',
        restrict: 'E',
        replace: true,
        scope: { isChecked: '=', click: '&' },
        link: function(scope, element, attrs) {
            scope.title = attrs.title;

            scope.onclick = function () {
                scope.isChecked = !scope.isChecked;
                scope.click({ checked: scope.isChecked });
            };
        }
    }
});

angular.module('moneyManager.shared').directive('loadingPanel', function ($compile) {
    return {
        template: '<div class="loading-panel" ng-class="{ \'loading-panel-black\': modal}" ng-show="isLoading"><img ng-src="icons/loading{{suffix}}.gif" width="{{width}}" height="{{height}}"/></div>',
        restrict: 'E',
        scope: { isLoading: '=' },
        link: function (scope, element, attr) {
            scope.width = attr.width;
            scope.height = attr.height;
            scope.modal = attr.modal === "true";
            scope.suffix = scope.modal ? '' : '-black';
        }

    };
});

angular.module('moneyManager.shared').directive('sortingHeader', function(){
    return {
        template: '<div ng-click="changeSorting()" style="cursor: pointer"><span ng-transclude></span><img ng-src="../../../icons/icons-sorting{{sortingIcon}}.png" style="margin-left: 10px"/></div>',
        restrict: 'E',
        transclude: true,
        scope: { sorted: '@', onSort: '&' },
        link: function (scope, element, attrs) {
            scope.changeSorting = function () {
                scope.asc = !scope.asc;
                scope.setIcon(scope.asc);
                scope.onSort({ asc: scope.asc });
            }

            scope.$watch('sorted', function(newval, oldVal){
                scope.asc = scope.sorted === "false" ? false : (scope.sorted == "" ? null : true);
                scope.setIcon(scope.asc);
            });

            scope.setIcon = function(asc) {
                scope.sortingIcon = asc ? 'Up' : (asc == null ? 'Nothing' : 'Down');
            }

            scope.asc = scope.sorted === "false" ? false : (scope.sorted == "" ? null : true);
            scope.setIcon(scope.asc);
        }
    }
});
