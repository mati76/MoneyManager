'use strict';

function item() {
    return {
        restrict: 'A',
        replace: true,
        template: '<tr class="grid-row" ng-class="{\'cursor-hand\': showHand && !override, \'grid-row-expanded\': item.isSelected}" ng-click="click({e: item})" ' +
            ' ng-mouseover="showHand = true" ng-mouseleave="showHand = false"> ' +
            '<td width = "10px"/>' +
            '<td width = "60px" ng-click="$event.stopPropagation()" ng-mouseover="override = true" ng-mouseleave="override = false"> ' +
                '<check-box is-checked="item.checked"/>' +
            '</td>' +
            '<td ng-style="field.headerStyle" ng-repeat="field in fields" ng-style="field.style" ng-class="field.class"</td>{{item[field.field] | dynamicFilter: field.filter }} ' +
            '<td width = "60px"/>' +
            '</tr>',
        scope: { item: '=', fields: '=', click: '&' },
        link: function (scope) {
            scope.item.isSelected = false;
        }
    }
}

module.exports = item;