angular.module('moneyManager.expense').factory('budgetService', ['$http', '$filter', 'API_END_POINT', function ($http, $filter, API_END_POINT) {
    var uri = API_END_POINT + '/api/budget';

    return {
        getExpense: getExpense,
        getExpenses: getExpenses,
        getBudgetTotals: getBudgetTotals,
        saveExpense: saveExpense,
        removeExpense: removeExpense
    };

    function getExpense(id) {
        return $http.get(uri + '/expense/' + id);
    }

    function getExpenses() {
        return $http.get(uri + '/expense/' + $filter('date')(new Date(), 'yyyy-MM-dd'));
    }

    function getExpenses(criteria) {
        return $http.get(uri + '/expense', { params: criteria });
    }

    function saveExpense(expense) {
        return $http.post(uri + '/expense', expense);
    }

    function removeExpense(id) {
        return $http.delete(uri + '/expense/' + id);
    }

    function getBudgetTotals(year, month) {
        return $http.get(uri + '/totals/' + year + '/' + month);
    }
}]);