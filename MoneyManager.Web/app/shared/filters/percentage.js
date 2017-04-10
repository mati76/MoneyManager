'use strict';

percentage.$inject = ['$filter'];
function percentage($filter) {
    return function (input, decimals) {
        return $filter('number')(input * 100, decimals) + '%';
    };
}

module.exports = percentage;