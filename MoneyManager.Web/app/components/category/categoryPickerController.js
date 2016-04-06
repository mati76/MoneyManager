angular.module('moneyManager.category').controller('categoryPickerController', ['$scope', '$uibModalInstance', 'params', function ($scope, $uibModalInstance, params) {
    $scope.close = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.select = function (category) {
        $uibModalInstance.close(category);
    }

    $scope.categoryRows = params.categories;
}]);
 