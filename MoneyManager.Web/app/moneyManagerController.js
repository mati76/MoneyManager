angular.module('moneyManager').controller('moneyManagerController', ['$scope', '$state', function ($scope, $state) {
    $scope.pageName = "";
    $scope.addFunc = function () {
        alert('dupa');
    };
    $scope.addBtnClass = "";
    $scope.isLoading = false;
}]);