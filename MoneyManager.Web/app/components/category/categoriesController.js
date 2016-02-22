angular.module('moneyManager.category').controller('categoriesController', ['$scope', '$uibModal', 'categoryService', function ($scope, $uibModal, categoryService) {
   
    $scope.$parent.pageName = "CATEGORIES";
    
    $scope.addCategory = function (parentCategoryId) {
        openDialog('add', parentCategoryId);
    };

    $scope.editCategory = function(category){
        openDialog('edit', null, category);
    };

    $scope.removeCategory = function (categoryId) {
        categoryService.removeCategory(categoryId).then(
            function (data) {
                reload();
            }, function (error) {
                alert(error);
            })
    }

    function openDialog(mode, parentCategoryId, category) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/app/components/category/category.html',
            controller: 'categoryController',
            resolve: {
                params: { dialogMode: mode, category: category, parentCategoryId: parentCategoryId }
            }
        });

        modalInstance.result.then(function () {
            reload();
        });
    };

    function reload() {
        $scope.$parent.isLoading = true;
        categoryService.getCategories().then(function (result) {
            var categories = result.data;

            $scope.categoryRows = [];
            for (var i = 0; i < categories.length; i++) {
                if (i % 3 == 0) {
                    $scope.categoryRows.push([]);
                }
                $scope.categoryRows[$scope.categoryRows.length - 1].push(categories[i]);
            }
            $scope.$parent.isLoading = false;
        }),
        function (error) {
            $scope.$parent.isLoading = false;
        }
    }

    (function () {
        reload();
    })();

    //var categories = [
    //    {
    //        "name": 'Spożywcze', color: '#435677',
    //        "categories": [
    //            { "name": 'Jedzenie', color: '#435677' },
    //            { "name": 'Alkohol', color: '#777777' }
    //        ]
    //    },
    //    {
    //        "name": 'Chemia', color: '#435677',
    //        "categories": [
    //            { "name": 'Sprzątanie', color: '#435677' },
    //            { "name": 'Kosmetyki', color: '#777777' }
    //        ]
    //    },
    //    {
    //        "name": 'Samochód', color: '#435677',
    //        "categories": [
    //            { "name": 'Mycie', color: '#435677' },
    //            { "name": 'Paliwo', color: '#777777' },
    //            { "name": 'Wymiany', color: '#777777' }
    //        ]
    //    },
    //    {
    //        "name": 'Kosmetyczne', color: '#435677',
    //        "categories": [
    //            { "name": 'Fryzjer', color: '#435677' },
    //            { "name": 'Kosmetyczka', color: '#777777' }
    //        ]
    //    },
    //    {
    //        "name": 'Inne wydatki', color: '#435677',
    //        "categories": [
    //            { "name": 'Kino', color: '#435677' },
    //            { "name": 'Ubrania', color: '#777777' },
    //            { "name": 'Restauracje', color: '#777777' },
    //            { "name": 'Dominik', color: '#777777' }
                
    //        ]
    //    },
    //    {
    //        "name": 'Wypoczynek', color: '#435677',
    //        "categories": [
    //            { "name": 'Wczasy', color: '#435677' }
    //        ]
    //    }
    //];
}]);