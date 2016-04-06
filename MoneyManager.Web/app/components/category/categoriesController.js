angular.module('moneyManager.category').controller('categoriesController', ['$scope', '$uibModal', 'categoryService', 'messageBoxService', function ($scope, $uibModal, categoryService, messageBoxService) {
   
    $scope.$parent.pageName = "CATEGORIES";
    $scope.$parent.titleBarClass = "title-bar-category";
    $scope.$parent.btnAddCaption = "Add Category";
    $scope.$parent.addFunc = function () {
        $scope.addCategory();
    };
    
    $scope.addCategory = function (parentCategoryId) {
        openDialog('add', parentCategoryId);
    };

    $scope.editCategory = function(category){
        openDialog('edit', null, category);
    };

    $scope.removeCategory = function (categoryId) {
        messageBoxService.showMessage('Are you sure you want to remove category?', 'Remove category', 'warning')
            .then(function () {
                removeCategory(categoryId)
            });
    }

    function removeCategory(categoryId) {
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
}]);