angular.module('moneyManager.expense').controller('expenseController', ['$scope', '$uibModalInstance', 'params', 'expenseService', function ($scope, $uibModalInstance, params, expenseService) {

    var mode = {
        add: 'Add New ',
        edit: 'Edit '
    };

    $scope.model = {
        Amount: '',
        Date: new Date(),
        Comment: '',
        Id: 0
    }

    $scope.mode = mode[params.dialogMode];

    $scope.save = function () {
        if ($scope.expenseForm.$valid) {
            expenseService.saveExpense($scope.model).then(function (result) {
                $uibModalInstance.close();
            }, function (error) {
                //handle error

                $scope.cancel();
            });
        }
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    //onLoad
    (function () {
        if (params.dialogMode == 'add') {
            $scope.model.Date = new Date();
        } else {
            $scope.model.Amount = params.expense.Amount;
            $scope.model.Comment = params.expense.Comment;
            $scope.model.Date = params.expense.Date;
            $scope.model.Id = params.expense.Id;
        }
    })();

    $scope.format = 'dd.MM.yyyy';
    $scope.dateOptions = {
        formatYear: 'yy',
        maxDate: new Date(2020, 5, 22),
        minDate: new Date(),
        startingDay: 1
    };
    $scope.pickerPopup = {
        opened: false
    };

    $scope.openPicker = function () {
        $scope.pickerPopup.opened = true;
    }
}]);