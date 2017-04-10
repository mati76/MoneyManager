'use strict';

angular.module('moneyManager.shared', [])
    .factory('messageBoxService', require('./messageBox/messageBoxService'))
    .factory('helperService', require('./helperService'))
    .factory('eventAggregatorService', require('./eventAggregatorService'))
    .factory('dateService', require('./dateService'))
    .factory('popupService', require('./popupService'))
    .directive('totalsLabel', require('./directives/totalsLabel'))
    .directive('checkBox', require('./directives/checkBox'))
    .directive('loadingPanel', require('./directives/loadingPanel'))
    .directive('nothingToDisplay', require('./directives/nothingToDisplay'))
    .directive('sortingHeader', require('./directives/sortingHeader'))
    .directive('gridHeader', require('./directives/gridHeader'))
    .directive('item', require('./directives/item'))
    .directive('gridButtons', require('./directives/gridButtons'))
    .directive('grid', require('./directives/grid'))
    .directive('multiSelectDropDown', require('./directives/multiSelectDropDown/multiSelectDropDown'))
    .directive('dropDown', require('./directives/dropDown/dropDown'))
    .directive('periodPicker', require('./directives/periodPicker/periodPicker'))
    .filter('dynamicFilter', require('./filters/dynamicFilter'))
    .filter('percentage', require('./filters/percentage'))
    .controller('messageBoxController', require('./messageBox/messageBoxController'));
    
