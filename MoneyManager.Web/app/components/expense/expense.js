'use strict';

angular.module('moneyManager.expense', ['moneyManager.shared'])
    .controller('expenseController', require('./expenseController'))
    .controller('expensesController', require('./expensesController'))
    .factory('expenseService', require('./expenseService'));
