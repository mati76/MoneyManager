angular.module('moneyManager.category').factory('categoryService', ['$http', function ($http) {
    var uri = 'http://localhost:8080/api/category';

    return {
        getCategories: getCategories,
        saveCategory: saveCategory,
        removeCategory: removeCategory
    };

    //function httpPost(url, data) {
    //    return $http({
    //        headers: {
    //            'Content-Type': 'application/json'
    //        },
    //        data: JSON.stringify(data),
    //        method: 'POST',
    //        url: url
    //    })
    //}

    function getCategories() {
        return $http.get(uri);
    }

    function saveCategory(category) {
        return $http.post(uri, category);
    }

    function removeCategory(categoryId) {
        return $http.delete(uri + '/' + categoryId);
    }
}]);