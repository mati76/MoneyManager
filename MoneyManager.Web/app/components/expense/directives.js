angular.module('moneyManager.expense').directive('expenseHeader', function () {
    return {
        restrict: 'A',
        replace: true,
        template: '<tr class="grid-header">' +
                '<th width = "10px"/>' +
                '<th width = "60px"><check-box is-checked=\"checkedAll\" click="checkAll({ isChecked: checked})"></th>' +
                '<th width = "120px" class="unselectable align-left"><sorting-header sorted="{{ isSortedBy(\'Date\') }}" on-sort = "sort({f: \'Date\', e: asc})">DATE</sorting-header></th>' +
                '<th width = "100px" class="unselectable align-right"><sorting-header sorted="{{ isSortedBy(\'Amount\') }}" on-sort = "sort({f: \'Amount\', e: asc})">AMOUNT</sorting-header></th>' +
                '<th width = "70%" class="unselectable" style="padding-left: 40px"><sorting-header sorted="{{ isSortedBy(\'Comment\') }}" on-sort = "sort({f: \'Comment\', e: asc})">DESCRIPTION</sorting-header></th>' +
                '<th width = "30%" class="unselectable align-right"><sorting-header sorted="{{ isSortedBy(\'Category.Name\') }}" on-sort = "sort({f: \'Category.Name\', e: asc})">CATEGORY</sorting-header> </th>' +
                '<th width = "60px"/>' +
                '</tr>',
        scope: false
    }
});

angular.module('moneyManager.expense').directive('item', function () {
    return {
        restrict: 'A',
        replace: true,
        template: '<tr class="grid-row" ng-class="{\'cursor-hand\': showHand && !override, \'grid-row-expanded\': item.isSelected}" ng-click="click({e: item})" ' +
            ' ng-mouseover="showHand = true" ng-mouseleave="showHand = false"> ' +
            '<td/>' +
            '<td ng-click="$event.stopPropagation()" ng-mouseover="override = true" ng-mouseleave="override = false"> ' +
                '<check-box is-checked="item.checked"/>' +
            '</td>' +
            '<td>{{ item.Date | date }}</td>' +
            '<td class="expense align-right">{{ ( item.Amount | currency : \'\') }}</td>' +
            '<td style="padding-left: 40px">{{ item.Comment }}</td>' +
            '<td style="text-align: right">{{ item.Category.Name }}</td>' +
            '<td/>' +
            '</tr>',
        scope: { item: '=', click: '&' },
        link: function (scope) {
            scope.item.isSelected = false;
        }
    }
});

angular.module('moneyManager.expense').directive('gridButtons', function () {
    return {
        restrict: 'A',
        replace: true,
        template: '<tr class="grid-row-expanded">' +
                '<td colspan="7">' +
                    '<div class="grid-buttons">' +
                        '<button class="grid-btn grid-btn-edit" ng-click="click({e: {action: \'edit\', expense: for}} )">EDIT</button>' +
                        '<button class="grid-btn grid-btn-split" ng-click="click({e: {action: \'split\', expense: for}} )")>SPLIT</button>' +
                        '<button class="grid-btn grid-btn-repeat" ng-click="click({e: {action: \'repeat\', expense: for}} )")>REPEAT</button>' +
                        '<button class="grid-btn grid-btn-delete" ng-click="click({e: {action: \'delete\', expense: for}} )")>DELETE</button>' +
                    '</div>' +
                '</td>' +
            '</tr>',
        scope: { for: '=', click: '&' }
    }
});

angular.module('moneyManager.expense').directive('grid', function () {
    return {
        restrict: 'E',
        template: '<div>' +
                    '<loading-panel is-loading="false" width="50" height="50"></loading-panel>' +
                    '<table id="transactions-grid" class="full-width">' +
                        '<tr expense-header>' +
                        '<tr ng-show="source.length > 0" ng-repeat-start="item in source track by item.Id" item="item" click="selectItem(e)" />' +
                        '<tr ng-repeat-end="" ng-if="item.isSelected" grid-buttons for="item" click="expenseAction(e)" />' +
                    '</table>' +
                    '<p class="nothing-label" ng-show="source.length == 0">NO TRANSACTIONS</p>' +
                    '</div>',
        scope: { source: '=', options: '=', sortColumn: '=', sortAscending: '=', sort: '&' },
        link: function (scope, element, attr) {

            scope.isSortedBy = function (field) {
                return scope.sortColumn == field ? scope.sortAscending : undefined;
            };

            scope.multipleChecked = function () {
                var cnt = 0;
                if ($scope.expenses == null) {
                    return false;
                }

                scope.source.forEach(function (item) {
                    if (item.checked) {
                        cnt++;
                    }
                    if (cnt > 1) {
                        return true;
                    }
                });
                scope.checkedAll = cnt == scope.expenses.length;
                return cnt > 1;
            };

            scope.checkAll = function (args) {
                scope.source.forEach(function (e) {
                    e.checked = args.isChecked;
                });
            };

            scope.selectItem = function (e) {
                e.isSelected = !e.isSelected;
                scope.source.forEach(function (item) {
                    if (item.Id != e.Id) {
                        item.isSelected = false;
                    }
                });
            };
        }
    };
});
