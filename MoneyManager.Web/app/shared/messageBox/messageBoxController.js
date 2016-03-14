angular.module('moneyManager.shared').controller('messageBoxController', ['$scope', 'params', '$uibModalInstance', function ($scope, params, $uibModalInstance) {

    $scope.message = params.message;
    $scope.title = params.title;
    $scope.type = params.type;

    $scope.ok = function () {
        $uibModalInstance.close();
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}]);