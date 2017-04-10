'use strict';

//angular.module('moneyManager.category').controller('categoryPickerController', ['$scope', '$uibModalInstance', 'params', function ($scope, $uibModalInstance, params) {
categoryPickerController.$inject = ['$scope', '$uibModalInstance', 'params'];

function categoryPickerController($scope, $uibModalInstance, params) {
    $scope.close = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.select = function (category) {
        $uibModalInstance.close(category);
    }

    $scope.categoryRows = params.categories;
}//]);
 
module.exports = categoryPickerController;