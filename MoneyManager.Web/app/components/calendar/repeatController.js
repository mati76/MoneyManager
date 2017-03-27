angular.module('moneyManager.calendar').controller('repeatController', ['$scope', 'params', '$uibModal', '$uibModalInstance', 'categoryService', function ($scope, params, $uibModal, $uibModalInstance, categoryService) {

    $scope.title = params.title;
    $scope.model = params.model;
    $scope.model.Date = Date.parse(params.model.Date);
    $scope.repeatDays = [];
    for (var i = 1; i <= 31; i++) {
        $scope.repeatDays.push(i);
    }

    $scope.repeatPeriods = [
        'Days', 'Weeks', 'Months', 'Years'
    ];

    $scope.save = function () {
        if ($scope.form.$valid) {
            $scope.model.Date = new Date($scope.model.Date);
            $scope.model.Until = new Date($scope.model.Until);
            var expense = {
                Amount: $scope.model.Amount,
                Comment: $scope.model.Comment,
                Date: new Date($scope.model.Date),
                Id: $scope.model.Id,
                CategoryId: $scope.model.CategoryId
            };
            $scope.saveFunc(expense).then(function (result) {
                $uibModalInstance.close();
            }, function (error) {
                //handle error

                $scope.cancel();
            });
        } else {
            $scope.expenseForm.category.$dirty = true;
            $scope.expenseForm.amount.$dirty = true;
            $scope.expenseForm.date.$dirty = true;
        }
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    function showCategoryPicker(cat) {
        var modalInstance = $uibModal.open({
            animation: true,
            size: 'lg',
            templateUrl: '/app/components/category/categoryPicker.html',
            controller: 'categoryPickerController',
            resolve: {
                params: { categories: cat }
            }
        });

        modalInstance.result.then(function (category) {
            $scope.model.CategoryId = category.Id;
            $scope.model.CategoryName = category.Name;
        });
    }

    $scope.categoryPopup = function () {
        $scope.loadingCategories = true;
        categoryService.getExpenseCategories().then(function (result) {
            $scope.loadingCategories = false;
            var data = result.data;

            var categories = [];
            for (var i = 0; i < data.length; i++) {
                if (i % 3 == 0) {
                    categories.push([]);
                }
                categories[categories.length - 1].push(data[i]);
            }
            showCategoryPicker(categories);
        }),
        function (error) {

        }
    }

    $scope.pickerPopup = {
        opened: false
    };

    $scope.pickerPopup1 = {
        opened: false
    };

    $scope.openPicker = function (picker) {
        if (picker == 'date') {
            $scope.pickerPopup.opened = true;
        } else {
            $scope.pickerPopup1.opened = true;
        }
    }

}]);