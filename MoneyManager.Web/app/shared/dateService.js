angular.module('moneyManager.shared').factory('dateService', [function () {
    return {
        getWeek: getWeek,
        getMonth: getMonth,
        getYear: getYear
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

    function getYear() {
        var year = new Date().getFullYear();
        return {
            from: new Date(year, 0, 1),
            to: new Date(year, 11, 31)
        };
    }

    function daysInMonth(month, year) {
        return new Date(year, month + 1, 0).getDate();
    }

}]);