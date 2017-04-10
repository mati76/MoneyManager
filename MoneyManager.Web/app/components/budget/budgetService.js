'use strict';

//angular.module('moneyManager.expense').factory('budgetService', ['$http', '$filter', 'API_END_POINT', function ($http, $filter, API_END_POINT) {

budgetService.$inject = ['$http', '$filter', 'API_END_POINT'];

function budgetService($http, $filter, API_END_POINT){
    var uri = API_END_POINT + '/api/budget';

    return {
        getExpense: getExpense,
        getExpenses: getExpenses,
        getBudgetTotals: getBudgetTotals,
        saveExpense: saveExpense,
        removeExpense: removeExpense,
        getBudgetRealization: getBudgetRealization
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

    function getBudgetTotals(dateFrom, dateTo) {
        return $http.get(uri + '/totals/' + $filter('date')(getMonthStart(dateFrom), 'yyyy-MM-dd') + '/' + $filter('date')(getMonthEnd(dateTo), 'yyyy-MM-dd'));
    }

    function getBudgetRealization(dateFrom, dateTo) {
        return $http.get(uri + '/realization/' + $filter('date')(getMonthStart(dateFrom), 'yyyy-MM-dd') + '/' + $filter('date')(getMonthEnd(dateTo), 'yyyy-MM-dd'));
    }

    function getMonthStart(date) {
        return new Date(date.getFullYear(), date.getMonth(), 1);
    }

    function getMonthEnd(date) {
        return new Date(date.getFullYear(), date.getMonth(), date.getDate());
    }
}

module.exports = budgetService;