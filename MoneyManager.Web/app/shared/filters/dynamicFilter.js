'use strict';

dynamicFilter.$inject = ['$filter'];
function dynamicFilter($filter) {
    return function (value, filter) {
        if (filter != null) {
            value = $filter(filter)(value, "");
        }
        return value;
    };
}

module.exports = dynamicFilter;
