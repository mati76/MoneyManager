'use strict';

function loadingPanel() {
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
}

module.exports = loadingPanel;