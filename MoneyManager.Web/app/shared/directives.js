angular.module('moneyManager.shared').directive('checkBox', function () {
    return {
        template: '<div class="check-box" ng-class="{\'glyphicon glyphicon-ok\': isChecked == true }" aria-hidden="true" ng-click="onclick()"/>',
        restrict: 'E',
        scope: { disabled: '=', isChecked: '=', click: '&' },
        link: function(scope, element, attrs) {
            scope.title = attrs.title;

            scope.onclick = function () {
                if (scope.disabled != true) {
                    scope.isChecked = !scope.isChecked;
                    scope.click({ checked: scope.isChecked });
                }
            };
        }
    }
});

angular.module('moneyManager.shared').directive('loadingPanel', function () {
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

            scope.$watch('sorted', function(newval, oldVal){
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
});

angular.module('moneyManager.shared').directive('gridHeader', function () {
    return {
        restrict: 'A',
        replace: true,
        template: '<tr class="grid-header">' +
                '<th width = "10px"></th>' +
                '<th width = "60px"><check-box disabled="source.length == 0 || isLoading === true" is-checked=\"checkedAll\" click="checkAll({ isChecked: checked})"></th>' +
                '<th ng-repeat="field in options.fields" class="unselectable" ng-class="field.headerClass" ng-style="field.headerStyle">' +
                    '<sorting-header disabled="source.length == 0 || isLoading === true" sorted="{{ isSortedBy(field.field) }}" on-sort = "sort({f: field.sortBy != null ? field.sortBy : field.field, e: asc})">{{field.title}}</sorting-header></th>' +
                '<th width = "60px"/>' +
                '</tr>',
        scope: false
    }
});

angular.module('moneyManager.shared').directive('item', function () {
    return {
        restrict: 'A',
        replace: true,
        template: '<tr class="grid-row" ng-class="{\'cursor-hand\': showHand && !override, \'grid-row-expanded\': item.isSelected}" ng-click="click({e: item})" ' +
            ' ng-mouseover="showHand = true" ng-mouseleave="showHand = false"> ' +
            '<td/>' +
            '<td ng-click="$event.stopPropagation()" ng-mouseover="override = true" ng-mouseleave="override = false"> ' +
                '<check-box is-checked="item.checked"/>' +
            '</td>' +
            '<td ng-repeat="field in fields" ng-style="field.style" ng-class="field.class"</td>{{item[field.field] | dynamicFilter: field.filter }} ' +
            '<td/>' +
            '</tr>',
        scope: { item: '=', fields: '=', click: '&' },
        link: function (scope) {
            scope.item.isSelected = false;
        }
    }
});

angular.module('moneyManager.shared').directive('gridButtons', function () {
    return {
        restrict: 'A',
        replace: true,
        template: '<tr class="grid-row-expanded">' +
                '<td colspan="7">' +
                    '<div class="grid-buttons">' +
                        '<button class="grid-btn grid-btn-edit" ng-click="click({e: {action: \'edit\', expense: for}} )">EDIT</button>' +
                        '<button class="grid-btn grid-btn-split" ng-click="click({e: {action: \'split\', expense: for}} )")>SPLIT</button>' +
                        '<button class="grid-btn grid-btn-repeat" ng-click="click({e: {action: \'repeat\', expense: for}} )")>REPEAT</button>' +
                        '<button class="grid-btn grid-btn-delete" ng-click="click({e: {action: \'delete\', expense: for}} )")>DELETE</button>' +
                    '</div>' +
                '</td>' +
            '</tr>',
        scope: { for: '=', click: '&' }
    }
});

angular.module('moneyManager.shared').directive('grid', function () {
    return {
        restrict: 'E',
        template: '<div>' +
                    '<loading-panel is-loading="isLoading" width="50" height="50"></loading-panel>' +
                    '<table id="transactions-grid" class="full-width">' +
                        '<tr grid-header>' +
                        '<tr ng-show="source.length > 0" ng-repeat-start="item in source track by item.Id" item="item" fields="options.fields" click="selectItem(e)" />' +
                        '<tr ng-repeat-end="" ng-if="item.isSelected" grid-buttons for="item" click="onButtonClick({e : e })" />' +
                    '</table>' +
                    '<p class="nothing-label" ng-show="source.length == 0">{{options.noItemsLabel}}</p>' +
                    '</div>',
        scope: { source: '=', options: '=', isLoading: '=', sortedBy: '=', onSort: '&', onButtonClick: '&' },
        link: function (scope, element, attr) {
            scope.isSortedBy = function (field) {
                return scope.sortedBy.SortBy == field ? scope.sortedBy.SortAsc : undefined;
            };

            scope.multipleChecked = function () {
                var cnt = 0;
                if ($scope.expenses == null) {
                    return false;
                }

                scope.source.forEach(function (item) {
                    if (item.checked) {
                        cnt++;
                    }
                    if (cnt > 1) {
                        return true;
                    }
                });
                scope.checkedAll = cnt == scope.expenses.length;
                return cnt > 1;
            };

            scope.checkAll = function (args) {
                scope.source.forEach(function (e) {
                    e.checked = args.isChecked;
                });
            };

            scope.selectItem = function (e) {
                e.isSelected = !e.isSelected;
                scope.source.forEach(function (item) {
                    if (item.Id != e.Id) {
                        item.isSelected = false;
                    }
                });
            };

            scope.sort = function (args) {
                if (scope.isLoading) {
                    return;
                }
                scope.sortedBy.SortBy = args.f;
                scope.sortedBy.SortAsc = args.e;
                scope.onSort({ e: args });
            };
        }
    };
});
