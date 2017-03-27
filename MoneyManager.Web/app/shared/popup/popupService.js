angular.module('moneyManager.shared').factory('popupService', ['$uibModal', function ($uibModal) {

    return { popup: popup };

    function popup(mode, options, model) {
        callback = options.callback;
        var modalInstance = $uibModal.open({
            animation: true,
            windowClass: options.windowClass,//'expence-modal',
            templateUrl: options.templateUrl,// '/app/components/expense/expense.html',
            controller: options.controller,// 'expenseController',
            resolve: {
                params: { title: options.title, dialogMode: mode, model: model, saveFunc: options.saveFunc }
            }
        });

        modalInstance.result.then(function () {
            callback();
        });
    }

}]);