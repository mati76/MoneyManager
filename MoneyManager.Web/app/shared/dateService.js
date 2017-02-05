angular.module('moneyManager.shared').factory('dateService', [function () {
    return {
        getWeek: getWeek,
        getMonth: getMonth,
        getNextMonth: getNextMonth,
        getPrevMonth: getPrevMonth,
        getYear: getYear,
        isRangeFullMonth, isRangeFullMonth
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

    function getNextMonth() {
        var current = new Date();
        var month = current.getMonth() + 1;
        var year = current.getFullYear();

        if (month == 12) {
            month = 0;
            year++;
        }
        return {
            from: new Date(year, month, 1),
            to: new Date(year, month, daysInMonth(month, year))
        };
    }

    function getPrevMonth() {
        var current = new Date();
        var month = current.getMonth() - 1;
        var year = current.getFullYear();
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

    

}]);