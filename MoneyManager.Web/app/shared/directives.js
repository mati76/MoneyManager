angular.module('moneyManager.shared').directive('totalsLabel', function () {
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
});

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

angular.module('moneyManager.shared').directive('nothingToDisplay', function () {
    return {
        template: '<div class="ntd-label unselectable bold">' +
            '<p class="nothing-label">NOTHING TO DISPLAY</p>' +
            '</div>',
        restrict: 'E'
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
                        '<button ng-repeat="btn in buttons" ng-bind="btn.label" class="grid-btn" ng-class="btn.css" ng-click="btn.callback(for)"></button>' +
                    '</div>' +
                '</td>' +
            '</tr>',
        scope: { buttons: '=', for: '=', click: '&' }
    }
});

angular.module('moneyManager.shared').directive('grid', function () {
    return {
        restrict: 'E',
        template: '<div>' +
                    '<p style="margin-bottom: 25px" id="transactions-header" class="title-label">{{options.label}}' +
                        '<span id="transactions-grid-buttons" ng-show="multipleChecked()">' +
                            '<button ng-repeat="btn in options.multiSelectActions" style="display: inline !important" class="grid-btn" ng-class="btn.css" ng-bind="btn.label" ng-click="btn.callback()"></button>' +
                        '</span>' +
                    '</p>' +
                    '<loading-panel is-loading="isLoading" width="50" height="50"></loading-panel>' +
                    '<table id="transactions-grid" class="full-width">' +
                        '<tr grid-header>' +
                        '<tr ng-show="source.length > 0" ng-repeat-start="item in source track by item.Id" item="item" fields="options.fields" click="selectItem(e)" />' +
                        '<tr ng-repeat-end="" ng-if="item.isSelected" buttons="options.singleSelectActions" grid-buttons for="item" />' +
                    '</table>' +
                    '<p class="nothing-label" ng-show="source.length == 0">{{options.noItemsLabel}}</p>' +
                    '</div>',
        scope: { label: '=', source: '=', options: '=', isLoading: '=', sortedBy: '=', onSort: '&', onButtonClick: '&' },
        link: function (scope, element, attr) {
            scope.isSortedBy = function (field) {
                return scope.sortedBy.SortBy == field ? scope.sortedBy.SortAsc : undefined;
            };

            scope.multipleChecked = function () {
                var cnt = 0;
                if (scope.source == null) {
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
                scope.checkedAll = cnt == scope.source.length;
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
