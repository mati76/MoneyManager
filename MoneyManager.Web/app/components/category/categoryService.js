angular.module('moneyManager.category').factory('categoryService', ['$http', function ($http) {
    return {
        getCategories: getCategories,
        saveCategory: saveCategory,
        removeCategory: removeCategory
    };

    function httpGet(url) {
        return $http({
            method: 'GET',
            url: url
        })
    }

    function httpPost(url, data) {
        return $http({
            headers: {
                'Content-Type': 'application/json'
            },
            data: JSON.stringify(data),
            method: 'POST',
            url: url
        })
    }

    function getCategories() {
        return httpGet('http://localhost:8080/api/category');
    }

    function saveCategory(category) {
        return httpPost('http://localhost:8080/api/category', category);
    }

    function removeCategory() {

    }
}]);