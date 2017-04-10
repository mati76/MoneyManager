'use strict';

//angular.module('moneyManager.category').controller('categoryController', ['$scope', '$uibModalInstance', 'params', 'categoryService', function ($scope, $uibModalInstance, params, categoryService) {

categoryController.$inject = ['$scope', '$uibModalInstance', 'params', 'categoryService'];

function categoryController($scope, $uibModalInstance, params, categoryService){
    var mode = {
        add: 'Add New ',
        edit: 'Edit '
    };

    $scope.model = {
        Name: '',
        ParentId: 0,
        Id: 0
    }

    $scope.mode = mode[params.dialogMode];

    $scope.save = function () {
        if ($scope.categoryForm.$valid) {
            categoryService.saveExpenseCategory($scope.model).then(function (result) {
                $uibModalInstance.close();
            }, function (error) {
                //handle error
      
                $scope.cancel();
            });
        }
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    //onLoad
    (function () {
        if (params.dialogMode == 'add') {
            $scope.model.ParentId = params.parentCategoryId;
        } else {
            $scope.model.Name = params.category.Name;
            $scope.model.ParentId = params.category.ParentId;
            $scope.model.Id = params.category.Id;
        }
    })();
}//]);

module.exports = categoryController;