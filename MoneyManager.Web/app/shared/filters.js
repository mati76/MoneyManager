angular.module('moneyManager.shared').filter('dynamicFilter', function ($filter) {
    return function (value, filter) {
        if (filter != null) {
            value = $filter(filter)(value, "");
        }
        return value;
    };
});
