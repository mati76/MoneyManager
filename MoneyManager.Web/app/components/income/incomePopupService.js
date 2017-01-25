angular.module('moneyManager.income').factory('incomePopupService', ['$uibModal', function ($uibModal) {

    return { incomePopup: incomePopup };

    function incomePopup(mode, reloadFunc, income) {
        callback = reloadFunc;
        var modalInstance = $uibModal.open({
            animation: true,
            windowClass: 'income-modal',
            templateUrl: '/app/components/income/income.html',
            controller: 'incomeController',
            resolve: {
                params: { dialogMode: mode, income: income }
            }
        });

        modalInstance.result.then(function () {
            callback();
        });
    }

}]);