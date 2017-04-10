'use strict';

function checkBox() {
    return {
        template: '<div class="check-box" ng-class="{\'glyphicon glyphicon-ok\': isChecked == true }" aria-hidden="true" ng-click="$event.stopPropagation(); onclick();"/>',
        restrict: 'E',
        scope: { disabled: '=', isChecked: '=', isDirty: '=', click: '&' },
        link: function (scope, element, attrs) {
            scope.title = attrs.title;
            scope.onclick = function () {

                if (scope.disabled != true) {
                    scope.isChecked = !scope.isChecked;
                    scope.isDirty = true;
                    scope.click({ checked: scope.isChecked });
                }
            };
        }
    }
}

module.exports = checkBox;