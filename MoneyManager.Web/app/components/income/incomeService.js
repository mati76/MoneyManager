angular.module('moneyManager.income').factory('incomeService', ['$http', '$filter', 'API_END_POINT', function ($http, $filter, API_END_POINT) {
    var uri = API_END_POINT + '/api/income';

    return {
        getIncome: getIncome,
        getIncomes: getIncomes,
        getIncomeTotals: getIncomeTotals,
        getCategoryTotals: getCategoryTotals,
        saveIncome: saveIncome,
        removeIncome: removeIncome
    };

    function getIncome(id) {
        return $http.get(uri + '/' + id);
    }

    function getIncomes() {
        return $http.get(uri + '/' + $filter('date')(new Date(), 'yyyy-MM-dd'));
    }

    function getIncomes(criteria) {
        return $http.get(uri, { params: criteria });
    }

    function getCategoryTotals(dateFrom, dateTo, categoryId) {
        return $http.get(uri + '/' + $filter('date')(dateFrom, 'yyyy-MM-dd') + '/' + $filter('date')(dateTo, 'yyyy-MM-dd') + '/category'
            + (categoryId ? '/' + categoryId : ''));
    }

    function saveIncome(income) {
        return $http.post(uri, income);
    }

    function removeIncome(id) {
        return $http.delete(uri + '/' + id);
    }

    function getIncomeTotals() {
        return $http.get(uri + '/totals/' + $filter('date')(new Date(), 'yyyy-MM-dd'));
    }
}]);