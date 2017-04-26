'use strict';

angular.module('moneyManager', [
    'ui.router',
    'ngMessages',
    'ngStorage',
    'ui.bootstrap',
    'chart.js',
    'moneyManager.shared',
    'moneyManager.category',
    'moneyManager.configuration',
    'moneyManager.expense',
    'moneyManager.income',
    'moneyManager.calendar',
    'moneyManager.auth',
    'moneyManager.login',
    'moneyManager.home',
    'moneyManager.budget',
    'infinite-scroll'
]);

angular.module('moneyManager').factory('authInterceptorService', require('./interceptors/authInterceptor'));

angular.module('moneyManager').config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
}]);

require('../configuration/configuration');
require('./router');
require('./prototypes');
require('./moneyManagerController');
require('./shared/shared');
require('./components/auth/auth');
require('./components/calendar/calendar');
require('./components/category/category');
require('./components/expense/expense');
require('./components/home/home');
require('./components/income/income');
require('./components/budget/budget');
require('./components/login/login');


