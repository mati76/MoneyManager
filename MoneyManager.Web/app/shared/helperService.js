'use strict';

function helperService() {

    var colors = ['#72C02C', '#3498DB', '#717984', '#FFAAAA', '#F1C40F', '#ea5d4b', '#8642f4', '#f4ad42', '#d4f4eb'];

    return {
        getColor: getColor
    };

    function getColor(index) {
        if (index < colors.length) {
            return colors[index];
        }
        return getRandomColor();
    }

    function getRandomColor() {
        var letters = '0123456789ABCDEF';
        var color = '#';
        for (var i = 0; i < 6; i++ ) {
            color += letters[Math.floor(Math.random() * 16)];
        }
        return color;
    }
}

module.exports = helperService;