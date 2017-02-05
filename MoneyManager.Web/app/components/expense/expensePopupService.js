﻿angular.module('moneyManager.expense').factory('expensePopupService', ['$uibModal', function ($uibModal) {

    return { expensePopup: expensePopup };

    function expensePopup(mode, reloadFunc, saveFunc, expense ) {
        callback = reloadFunc;
        var modalInstance = $uibModal.open({
            animation: true,
            windowClass: 'expence-modal',
            templateUrl: '/app/components/expense/expense.html',
            controller: 'expenseController',
            resolve: {
                params: { dialogMode: mode, expense: expense, saveFunc: saveFunc }
            }
        });

        modalInstance.result.then(function () {
            callback();
        });
    }

}]);