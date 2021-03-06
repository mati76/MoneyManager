﻿'use strict';

//angular.module('moneyManager.expense').controller('expenseController', ['$scope', '$uibModal', '$uibModalInstance', 'params', 'categoryService',
expenseController.$inject = ['$scope', '$uibModal', '$uibModalInstance', 'params', 'categoryService'];

function expenseController($scope, $uibModal, $uibModalInstance, params, categoryService) {

    var mode = {
        add: 'Add New ',
        edit: 'Edit '
    };

    $scope.showAllCategories = false;
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
    $scope.saveFunc = params.saveFunc;

    $scope.categoryToggleBtnText = "Select...";
    $scope.isOtherCategorySelected = false;
    
    $scope.isSelected = function (categoryId) {
        return $scope.model.CategoryId != null &&
            $scope.model.CategoryId == categoryId && !$scope.isOtherCategorySelected;
    };

    $scope.selectCategory = function (categoryId) {
        var isCategorySelected = false;
        $scope.topCategories.forEach(function(category){
            if(category.Id == categoryId){
                $scope.model.CategoryId = category.Id;
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
                $scope.model.CategoryId = category.Id;
                $scope.categoryToggleBtnText = category.Name;
                $scope.isOtherCategorySelected = true;
            }
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

    $scope.save = function () {
        if ($scope.expenseForm.$valid) {
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

    //onLoad
    (function () {
        $scope.$parent.isLoading = true;
        categoryService.getTopExpenseCategories().then(function (result) {
            $scope.$parent.isLoading = false;
            $scope.topCategories = result.data;
        });

        if (params.dialogMode == 'add') {
            $scope.model.Date = new Date();
            $scope.label = 'ADD EXPENSE';
        } else {
            $scope.model.Amount = params.model.Amount;
            $scope.model.Comment = params.model.Comment;
            $scope.model.Date = Date.parse(params.model.Date);
            $scope.model.Id = params.model.Id;
            $scope.model.CategoryId = params.model.CategoryId;
            $scope.label = 'EDIT EXPENSE';
        }
    })();

    $scope.pickerPopup = {
        opened: false
    };

    $scope.openPicker = function () {
        $scope.pickerPopup.opened = true;
    }
}//]);

module.exports = expenseController;