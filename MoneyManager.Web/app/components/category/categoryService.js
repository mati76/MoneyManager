angular.module('moneyManager.category').factory('categoryService', ['$http', 'API_END_POINT', function ($http, API_END_POINT) {
    var uri = API_END_POINT + '/api/category';

    return {
        getExpenseCategories: getExpenseCategories,
        getIncomeCategories: getIncomeCategories,
        saveExpenseCategory: saveExpenseCategory,
        removeExpenseCategory: removeExpenseCategory,
        getTopExpenseCategories: getTopExpenseCategories
    };

    function getExpenseCategories() {
        return $http.get(uri + '/expense');
    }

    function getIncomeCategories() {
        return $http.get(uri + '/income');
    }

    function saveExpenseCategory(category) {
        return $http.post(uri, category);
    }

    function removeExpenseCategory(categoryId) {
        return $http.delete(uri + '/expense' + categoryId);
    }

    function getTopExpenseCategories() {
        return $http.get(uri + '/expense/top');
    }
}]);