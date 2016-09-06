agGrid.initialiseAgGridWithAngular1(angular);

angular.module('moneyManager', [
    'ui.router',
    'ngMessages',
    'ui.bootstrap',
    'chart.js',
    'moneyManager.shared',
    'moneyManager.category',
    'moneyManager.configuration',
    'moneyManager.expense',
    'moneyManager.income'
]);