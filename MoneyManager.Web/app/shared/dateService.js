angular.module('moneyManager.shared').factory('dateService', [function () {
    return {
        getWeek: getWeek,
        getMonth: getMonth,
        getYear: getYear
    };

    function getWeek() {
        var current = new Date();     
        var weekstart = current.getDate() - current.getDay() + 1;
        var weekend = weekstart + 6; 
        return {
            from: new Date(current.setDate(weekstart)),
            to: new Date(current.setDate(weekend))
        };
    }

    function getMonth() {
        var current = new Date(); 
        var month = current.getMonth() + 1;
        var year = current.getYear();
        return {
            from: new Date(year, month, 1),
            to: new Date(year, month, daysInMonth(month - 1, year))
        };
    }

    function getYear() {
        var year = new Date().getYear();
        return {
            from: new Date(year, 1, 1),
            to: new Date(year, 12, 31)
        };
    }

    function daysInMonth(month, year) {
        return new Date(year, month, 0).getDate();
    }

}]);