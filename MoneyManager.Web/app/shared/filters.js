angular.module('moneyManager.shared').filter('dynamicFilter',  ['$filter', function ($filter) {
    return function (value, filter) {
        if (filter != null) {
            value = $filter(filter)(value, "");
        }
        return value;
    };
}]);

angular.module('moneyManager.shared').filter('percentage',  ['$filter', function ($filter) {
    return function (input, decimals) {
        return $filter('number')(input * 100, decimals) + '%';
    };
}]);
