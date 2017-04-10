'use strict';

function gridHeader() {
    return {
        restrict: 'A',
        replace: true,
        template: '<tr class="grid-header">' +
                '<th width = "10px"></th>' +
                '<th width = "60px"><check-box disabled="source.length == 0 || isLoading === true" is-checked=\"checkedAll\" click="checkAll({ isChecked: checked})"></th>' +
                '<th ng-repeat="field in options.fields" class="unselectable" ng-class="field.headerClass" ng-style="field.headerStyle">' +
                    '<sorting-header disabled="source.length == 0 || isLoading === true" sorted="{{ isSortedBy(field.sortBy) }}" on-sort = "sort({f: field.sortBy != null ? field.sortBy : field.field, e: asc})">{{field.title}}</sorting-header></th>' +
                '<th width = "60px"/>' +
                '</tr>',
        scope: false
    }
}

module.exports = gridHeader;