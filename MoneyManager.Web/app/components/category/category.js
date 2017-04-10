'use strict';

angular.module('moneyManager.category', ['moneyManager.shared'])
    .controller('categoriesController', require('./categoriesController'))
    .controller('categoryController', require('./categoryController'))
    .controller('categoryPickerController', require('./categoryPickerController'))
    .factory('categoryService', require('./categoryService'));

