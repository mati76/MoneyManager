angular.module('moneyManager.expense').controller('expenseController', ['$scope', '$uibModal', '$uibModalInstance', 'params', 'expenseService', 'categoryService',
    function ($scope, $uibModal, $uibModalInstance, params, expenseService, categoryService) {

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
    $scope.categoryToggleBtnText = "Select...";
    $scope.isOtherCategorySelected = false;
    
    $scope.isSelected = function (categoryId) {
        return $scope.model.Category != null &&
            $scope.model.Category.Id == categoryId && !$scope.isOtherCategorySelected;
    };

    $scope.selectCategory = function (categoryId) {
        var isCategorySelected = false;
        $scope.topCategories.forEach(function(category){
            if(category.Id == categoryId){
                $scope.model.Category = category;
                isCategorySelected = true;
            }
        });
        $scope.isOtherCategorySelected = false;
        $scope.categoryToggleBtnText = "Select...";
        return isCategorySelected;
    }

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
            if (!$scope.selectCategory(category.Id)) {
                $scope.model.Category = category;
                $scope.categoryToggleBtnText = category.Name;
                $scope.isOtherCategorySelected = true;
            }
        });
    }

    $scope.categoryPopup = function () {
        $scope.loadingCategories = true;
        categoryService.getCategories().then(function (result) {
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
        $scope.$parent.isLoading = true;
        categoryService.getTopCategories().then(function (result) {
            $scope.$parent.isLoading = false;
            $scope.topCategories = result.data;
        });

        if (params.dialogMode == 'add') {
            $scope.model.Date = new Date();
        } else {
            $scope.model.Amount = params.expense.Amount;
            $scope.model.Comment = params.expense.Comment;
            $scope.model.Date = params.expense.Date;
            $scope.model.Id = params.expense.Id;
        }
    })();

    $scope.showAllCategories = false;
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