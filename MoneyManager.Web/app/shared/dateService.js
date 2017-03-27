angular.module('moneyManager.shared').factory('dateService', [function () {
    return {
        getWeek: getWeek,
        getMonth: getMonth,
        getNextMonth: getNextMonth,
        getPrevMonth: getPrevMonth,
        getYear: getYear,
        isRangeFullMonth, isRangeFullMonth,
        getCalendar: getCalendar
    };

    function getWeek() {
        var current = new Date();     
        var weekstart = current.getDate() - (current.getDay() == 0 ? 6 : current.getDay() - 1);
        var weekend = weekstart + 6; 
        return {
            from: new Date(current.setDate(weekstart)),
            to: new Date(current.setDate(weekend))
        };
    }

    function getMonth() {
        var current = new Date(); 
        var month = current.getMonth();
        var year = current.getYear();
        return {
            from: new Date(current.setDate(1)),
            to: new Date(current.setDate(daysInMonth(month, year)))
        };
    }

    function getNextMonth(date) {
        var month = date.getMonth() + 1;
        var year = date.getFullYear();

        if (month == 12) {
            month = 0;
            year++;
        }
        return {
            from: new Date(year, month, 1),
            to: new Date(year, month, daysInMonth(month, year))
        };
    }

    function getPrevMonth(date) {
        var month = date.getMonth() - 1;
        var year = date.getFullYear();
        if (month == -1) {
            month = 11;
            year--;
        }
        return {
            from: new Date(year, month, 1),
            to: new Date(year, month, daysInMonth(month, year))
        };
    }

    function getYear() {
        var year = new Date().getFullYear();
        return {
            from: new Date(year, 0, 1),
            to: new Date(year, 11, 31)
        };
    }

    function isRangeFullMonth(from, to) {
        return from.getMonth() == to.getMonth() && from.getYear() == to.getYear() &&
            from.getDate() == 1 && to.getDate() == daysInMonth(to.getMonth(), to.getFullYear());
    }

    function daysInMonth(month, year) {
        return new Date(year, month + 1, 0).getDate();
    }

    function isFirstDayOfMonth(date) {
        return date.getDate() == 1;
    }

    function isFirstMonthOfYear(date) {
        return date.getMonth() == 0;
    }

    function isLastDayOfMonth(date) {
        return date.getDate() == daysInMonth(date.getMonth(), date.getFullYear());
    }

    function isLastMonthOfYear(date) {
        return date.getMonth() == 11;
    }

    function getPrevDay(date) {
        if (isFirstDayOfMonth(date)) {
            if (isFirstMonthOfYear(date)) {
                return new Date(date.getFullYear() -1, 11, 30);
            }
            return new Date(date.getFullYear(), date.getMonth() - 1, daysInMonth(date.getMonth() - 1, date.getFullYear()));
        }
        return new Date(date.getFullYear(), date.getMonth(), date.getDate() - 1);
    }

    function getNextDay(date){
        if (isLastDayOfMonth(date)) {
            if (isLastMonthOfYear(date)) {
                return new Date(date.getFullYear() + 1, 1, 1);
            }
            return new Date(date.getFullYear(), date.getMonth() + 1, 1);
        }
        return new Date(date.getFullYear(), date.getMonth(), date.getDate() + 1);
    }

    function getCalendar(year, month) {
        var days = [];
        
        var noDaysInMonth = daysInMonth(month, year);
        var firstDay = new Date(year, month, 1);
        var lastDay = new Date(year, month, noDaysInMonth);

        var totalDays = firstDay.getDay() + noDaysInMonth + 6 - lastDay.getDay();
        var weeks = totalDays / 7;

        var day = firstDay;
        for (var i = 0; i < firstDay.getDay() ; i++) {
            day = getPrevDay(day);
        }
        
        for(var i = 0; i < weeks; i++){
            var week = [];
            for(var j = 0; j < 7; j++){
                week.push(day);
                day = getNextDay(day);
            }
            days.push(week.slice());
        }
        return days;
    }
}]);