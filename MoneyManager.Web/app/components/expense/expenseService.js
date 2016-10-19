angular.module('moneyManager.expense').factory('expenseService', ['$http', '$filter', 'API_END_POINT', function ($http, $filter, API_END_POINT) {
    var uri = API_END_POINT + '/api/expense';

    return {
        getExpense: getExpense,
        getExpenses: getExpenses,
        getExpenseTotals, getExpenseTotals,
        saveExpense: saveExpense,
        removeExpense: removeExpense
    };

    function getExpense(id) {
        return $http.get(uri + '/' + id);
    }

    function getExpenses() {
        return $http.get(uri + '/' + $filter('date')(new Date(), 'yyyy-MM-dd'));
    }

    function getExpenses(criteria) {
        return $http.get(uri, { params: criteria });
    }

    function saveExpense(expense) {
        return $http.post(uri, expense);
    }

    function removeExpense(id) {
        return $http.delete(uri + '/' + id);
    }

    function getExpenseTotals() {
        return $http.get(uri + '/totals/' + $filter('date')(new Date(), 'yyyy-MM-dd'));
    }
}]);