angular.module('moneyManager.income').controller('incomeController', ['$scope', '$uibModal', '$uibModalInstance', 'params', 'incomeService', 'categoryService',
    function ($scope, $uibModal, $uibModalInstance, params, incomeService, categoryService) {

    var mode = {
        add: 'Add New ',
        edit: 'Edit '
    };

    $scope.format = 'dd.MM.yyyy';
    $scope.dateOptions = {
        formatYear: 'yy',
        maxDate: new Date(2020, 5, 22),
        minDate: new Date(),
        startingDay: 1
    };

    $scope.model = {
        Amount: '',
        Date: new Date(),
        Comment: '',
        Id: 0
    }

    $scope.mode = mode[params.dialogMode];

    $scope.save = function () {
        if ($scope.incomeForm.$valid) {
            var income = {
                Amount: $scope.model.Amount,
                Comment: $scope.model.Comment,
                Date: new Date($scope.model.Date),
                Id: $scope.model.Id,
                CategoryId: $scope.model.CategoryId 
            };
            incomeService.saveIncome(income).then(function (result) {
                $uibModalInstance.close();
            }, function (error) {
                //handle error

                $scope.cancel();
            });
        }  else {
            $scope.incomeForm.category.$dirty = true;
            $scope.incomeForm.amount.$dirty = true;
            $scope.incomeForm.date.$dirty = true;
        }
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    //onLoad
    (function () {
        categoryService.getIncomeCategories().then(function (data) {
            $scope.categories = data.data;
        }, function (error) {
            
        });

        if (params.dialogMode == 'add') {
            $scope.model.Date = new Date();
            $scope.label = 'ADD INCOME';
        } else {
            $scope.model.Amount = params.income.Amount;
            $scope.model.Comment = params.income.Comment;
            $scope.model.Date = Date.parse(params.income.Date);
            $scope.model.Id = params.income.Id;
            $scope.model.CategoryId = params.income.CategoryId;
            $scope.label = 'EDIT INCOME';
        }
    })();

    $scope.pickerPopup = {
        opened: false
    };

    $scope.openPicker = function () {
        $scope.pickerPopup.opened = true;
    }
}]);