angular.module('moneyManager.expense').factory('expenseService', ['$http', 'API_END_POINT', function ($http, API_END_POINT) {
    var uri = API_END_POINT + '/api/expense';

    return {
        getExpense: getExpense,
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
}]);