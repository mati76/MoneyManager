'use strict';

messageBoxController.$inject = ['$scope', 'params', '$uibModalInstance'];

function messageBoxController($scope, params, $uibModalInstance){

    $scope.message = params.message;
    $scope.title = params.title;
    $scope.type = params.type;

    $scope.ok = function () {
        $uibModalInstance.close();
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

module.exports = messageBoxController;