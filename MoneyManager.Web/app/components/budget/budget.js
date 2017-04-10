'use strict';

angular.module('moneyManager.budget', ['moneyManager.shared'])
    .controller('budgetController', require('./budgetController'))
    .factory('budgetService', require('./budgetService'));
