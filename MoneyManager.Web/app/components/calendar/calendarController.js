angular.module('moneyManager.calendar').controller('calendarController', ['$scope', 'dateService', '$state', '$stateParams', 'popupService', function ($scope, dateService, $state, $stateParams, popupService) {

    $scope.$parent.btnAddCaption = "Add Expense";
    $scope.$parent.pageName = "CALENDAR";
    $scope.$parent.titleBarClass = "title-bar-calendar";

    $scope.$parent.addFunc = function () {
        popupService.popup('edit', {
            windowClass: 'expence-modal',
            templateUrl: '/app/components/calendar/repeat.html',
            controller: 'repeatController',
            saveFunc: null,
            title: 'REPEAT EXPENSE'
        }, {});
    };

    $scope.days = [
        'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'
    ];

    var from = $stateParams.from;
    from = new Date(from.substring(0, 4), from.substring(4, 6) - 1, from.substring(6, 8));
    $scope.calendar = dateService.getCalendar(from.getFullYear(), from.getMonth());
}]);