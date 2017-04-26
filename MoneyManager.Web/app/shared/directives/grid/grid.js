'use strict';

function grid() {
    return {
        restrict: 'E',
        template: '<div>' +
                    '<p style="margin-bottom: 25px" id="transactions-header" class="title-label">{{options.label}}' +
                        '<span id="transactions-grid-buttons" ng-show="multipleChecked()">' +
                            '<button ng-repeat="btn in options.multiSelectActions" style="display: inline !important" class="grid-btn" ng-class="btn.css" ng-bind="btn.label" ng-click="btn.callback()"></button>' +
                        '</span>' +
                        '<multi-select-drop-down ng-hide="multipleChecked()" class="float-right" items="filterItems" title="filterLabel" apply-changes="applyFilter()"></multi-drop-down>' +
                    '</p>' +
                    '<loading-panel is-loading="isLoading" width="50" height="50"></loading-panel>' +
                    '<table class="full-width">' +
                        '<tr grid-header></tr>' +
                    '</table>' +
                    '<div class="constrained">' +
                    '<table infinite-scroll="loadNextItems()" infinite-scroll-container=\'".constrained"\' id="transactions-grid" class="full-width">' +
                        '<tr ng-show="source.length > 0" ng-repeat-start="item in source track by item.Id" item="item" fields="options.fields" click="selectItem(e)" />' +
                        '<tr ng-repeat-end="" ng-if="item.isSelected" buttons="options.singleSelectActions" grid-buttons for="item" />' +
                    '</table>' +
                    '</div>' +
                    '<p class="nothing-label" ng-show="!isLoading && source.length == 0">{{options.noItemsLabel}}</p>' +
                    '</div>',
        scope: {
            label: '=', source: '=', options: '=', isLoading: '=', sortedBy: '=', onSort: '&', onButtonClick: '&',
            loadNextItems: '&', filterLabel: '=', filterItems: '=', applyFilter: '&'
        },
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
                scope.checkedAll = cnt > 0 && cnt == scope.source.length;
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
}

module.exports = grid;