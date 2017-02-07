angular.module('moneyManager.shared').directive('periodPicker', function (dateService, messageBoxService, $filter, $state, $stateParams) {
    return {
        templateUrl: '/app/shared/periodPicker/periodPicker.html',
        restrict: 'E',
        scope: { showToday: '=', showWeek: '=', showMonth: '=', showNextMonth: '=', showPrevMonth: '=', showYear: '=', showCustomDates: '=', onPeriodChanged: '&', selectedOption: '=' },
        link: function (scope, element, attr) {
            scope.options = {
                Today: { title: 'Today', mode: 'Today', from: new Date(), to: new Date() },
                Week: { title: 'This Week', mode: 'Week', from: dateService.getWeek().from, to: dateService.getWeek().to },
                Month: { title: 'This Month', mode: 'Month', from: dateService.getMonth().from, to: dateService.getMonth().to },
                NextMonth: { title: 'Next Month', mode: 'NextMonth' },
                PrevMonth: { title: 'Previous Month', mode: 'PrevMonth' },
                Year: { title: 'This Year', mode: 'Year', from: dateService.getYear().from, to: dateService.getYear().to }
            };
            scope.customPeriodOption = "from";
            scope.customDate = { from: new Date(), to: new Date() };

            scope.parseOptions = function (from, to) {
                scope.selected = {};
                from = new Date(from.substring(0, 4), from.substring(4, 6) - 1, from.substring(6,8));
                to = new Date(to.substring(0, 4), to.substring(4, 6) - 1, to.substring(6, 8));

                for (var key in scope.options) {
                    var option = scope.options[key];
                    if (option.from != null && option.to != null &&
                        option.from.yyyymmdd() == from.yyyymmdd() && option.to.yyyymmdd() == to.yyyymmdd()) {
                        scope.title = option.title;
                        scope.selected.mode = option.mode;
                        scope.selected.dateFrom = option.from;
                        scope.selected.dateTo = option.to;
                    }
                }
                if (scope.selected.mode == null) {
                    if (dateService.isRangeFullMonth(from, to)) {
                        scope.title = from.toLocaleString('en-us', { month: "long" }) + ' '  + from.getFullYear();
                    } else {
                        scope.title = $filter('date')(from, 'dd-MMM-yyyy')
                            + ' - ' + $filter('date')(to, 'dd-MMM-yyyy');
                    }
                    scope.selected.mode = 'Custom';
                    scope.selected.dateFrom = from;
                    scope.selected.dateTo = to;
                }
            }

            scope.parseOptions($stateParams.from, $stateParams.to);

            scope.applySelection = function (mode) {
                scope.showCustom = false;

                if(mode == 'Custom'){
                    if (scope.customDate['to'] < scope.customDate['from']) {
                        messageBoxService.showMessage("Start date must be less than end date", 'Error', 'error');
                        return;
                    }
                    var dateFormat = 'dd-MMM-yyyy';
                    if (scope.customDate['from'].getYear() == scope.customDate['to'].getYear()){
                        if(scope.customDate['from'].getMonth() == scope.customDate['to'].getMonth()){
                            dateFormat = 'dd';
                        } else {
                            dateFormat = 'dd-MMM';
                        }
                    }

                    scope.title = $filter('date')(scope.customDate['from'], dateFormat)
                        + ' - ' + $filter('date')(scope.customDate['to'], 'dd-MMM-yyyy');
                    scope.selected.dateFrom = scope.customDate['from']
                    scope.selected.dateTo = scope.customDate['to']
                } else if (mode == 'NextMonth' || mode == 'PrevMonth') {
                    var dates = {};
                    if (mode == 'NextMonth') {
                        dates = dateService.getNextMonth(scope.selected.dateTo);
                    } else {
                        dates = dateService.getPrevMonth(scope.selected.dateFrom);
                    }
                    
                    scope.title = $filter('date')(dates.from, dateFormat)
                        + ' - ' + $filter('date')(dates.to, 'dd-MMM-yyyy');
                    scope.selected.dateFrom = dates.from;
                    scope.selected.dateTo = dates.to;
                } else {
                    scope.title = scope.options[mode].title;
                    scope.selected.mode = mode;
                    scope.selected.dateFrom = scope.options[mode].from;
                    scope.selected.dateTo = scope.options[mode].to;
                }
                $state.go($state.$current.name, { from: scope.selected.dateFrom.yyyymmdd(), to: scope.selected.dateTo.yyyymmdd() });
            };
        }
    }
});