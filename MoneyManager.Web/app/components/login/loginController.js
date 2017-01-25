angular.module('moneyManager.login').controller('loginController', ['$scope', '$state', 'authService', function ($scope, $state, authService) {

    $scope.credentials = {};

    $scope.register = function () {
        authService.register($scope.credentials).then(function () {
            
        });
    };

    $scope.login = function () {
        if ($scope.loginForm.$invalid) {
            $scope.loginForm.password.$dirty = true;
            $scope.loginForm.userId.$dirty = true;
            return;
        };
        authService.login($scope.credentials).then(function () {
            $state.go('dashboard');
        });
    };

}]);