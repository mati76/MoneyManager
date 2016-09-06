angular.module('moneyManager').controller('moneyManagerController', ['$scope', '$state', 'eventAggregatorService', function ($scope, $state, eventAggregatorService) {
    $scope.pageName = "";
    $scope.addFunc = function () {
        alert('dupa');
    };
    $scope.addBtnClass = "";
    $scope.isLoading = false;

    eventAggregatorService.subscribeToEvent(eventAggregatorService.eventNames.loadingStarted, function () { $scope.isLoading = true; });
    eventAggregatorService.subscribeToEvent(eventAggregatorService.eventNames.loadingFinished, function () { $scope.isLoading = false; });
}]);