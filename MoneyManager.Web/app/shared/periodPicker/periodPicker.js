angular.module('moneyManager.shared').directive('periodPicker', function (dateService, $filter) {
    return {
        templateUrl: '/app/shared/periodPicker/periodPicker.html',
        restrict: 'E',
        link: function (scope, element, attr) {
            scope.selected = { title: 'Today', dateFrom: new Date(), dateTo: new Date(), mode: 'Today' }
            scope.customPeriodOption = "from";
            scope.customDate = { from: new Date(), to: new Date() };
            scope.select = function (mode) {
                scope.showCustom = false;
                switch (mode) {
                    case 'Today':
                        scope.selected.title = mode;
                        scope.selected.mode = mode;
                        scope.selected.dateFrom = new Date();
                        scope.selected.dateTo = new Date();
                        break;
                    case 'Week':
                        var week = dateService.getWeek();
                        scope.selected.title = 'This Week';
                        scope.selected.mode = mode;
                        scope.selected.dateFrom = week.from;
                        scope.selected.dateTo = week.to;
                        break;
                        break;
                    case 'Month':
                        var month = dateService.getMonth();
                        scope.selected.title = 'This Month';
                        scope.selected.mode = mode;
                        scope.selected.dateFrom = month.from;
                        scope.selected.dateTo = month.to;
                        break;
                    case 'Year':
                        var year = dateService.getYear();
                        scope.selected.title = 'This Year';
                        scope.selected.mode = mode;
                        scope.selected.dateFrom = year.from;
                        scope.selected.dateTo = year.to;
                        break;
                    case 'Custom':
                        var dateFormat = 'dd-MMM-yyyy';
                        if (scope.customDate['from'].getYear() == scope.customDate['to'].getYear()){
                            if(scope.customDate['from'].getMonth() == scope.customDate['to'].getMonth()){
                                dateFormat = 'dd';
                            } else {
                                dateFormat = 'dd-MMM';
                            }
                        }

                        scope.selected.title = $filter('date')(scope.customDate['from'], dateFormat)
                            + ' - ' + $filter('date')(scope.customDate['to'], 'dd-MMM-yyyy');
                        scope.selected.mode = mode;
                        scope.selected.dateFrom = scope.customDate['from']
                        scope.selected.dateTo = scope.customDate['to']
                        break;
                }
            }
        }
    }
});