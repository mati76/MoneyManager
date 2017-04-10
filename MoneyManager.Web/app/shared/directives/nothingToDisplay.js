'use strict';

function nothingToDisplay() {
    return {
        template: '<div class="ntd-label unselectable bold">' +
            '<p class="nothing-label">NOTHING TO DISPLAY</p>' +
            '</div>',
        restrict: 'E'
    };
}

module.exports = nothingToDisplay;