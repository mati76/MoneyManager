angular.module('moneyManager.expense').factory('expenseService', ['$http', '$filter', 'API_END_POINT', function ($http, $filter, API_END_POINT) {
    var uri = API_END_POINT + '/api/expense';

    return {
        getExpense: getExpense,
        getExpenseTotals, getExpenseTotals,
        saveExpense: saveExpense,
        removeExpense: removeExpense
    };

    function getExpense(id) {
        return $http.get(uri);
    }

    function saveExpense(expense) {
        return $http.post(uri, expense);
    }

    function removeExpense(id) {
        return $http.delete(uri + '/' + id);
    }

    function getExpenseTotals() {
        return $http.get(uri + '/totals/' + $filter('date')(new Date(), 'yyyy-MM-dd'))
    }
}]);