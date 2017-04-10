'use strict';

function gridButtons() {
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
}

module.exports = gridButtons;