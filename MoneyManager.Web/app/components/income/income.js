'use strict';

angular.module('moneyManager.income', ['moneyManager.shared'])
    .factory('incomeService', require('./incomeService'))
    .controller('incomeController', require('./incomeController'))
    .controller('incomesController', require('./incomesController'));
