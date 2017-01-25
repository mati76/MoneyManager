angular.module('moneyManager.category').controller('categoriesController', ['$scope', '$uibModal', 'categoryService', 'messageBoxService', 'eventAggregatorService', function ($scope, $uibModal, categoryService, messageBoxService, eventAggregatorService) {
   
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
        categoryService.removeExpenseCategory(categoryId).then(
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
        eventAggregatorService.publishEvent(eventAggregatorService.eventNames.loadingStarted)
        categoryService.getExpenseCategories().then(function (result) {
            var categories = result.data;

            $scope.categoryRows = [];
            for (var i = 0; i < categories.length; i++) {
                if (i % 3 == 0) {
                    $scope.categoryRows.push([]);
                }
                $scope.categoryRows[$scope.categoryRows.length - 1].push(categories[i]);
            }
            eventAggregatorService.publishEvent(eventAggregatorService.eventNames.loadingFinished)
        }),
        function (error) {
            eventAggregatorService.publishEvent(eventAggregatorService.eventNames.loadingFinished)
        }
    }

    (function () {
        reload();
    })();
}]);