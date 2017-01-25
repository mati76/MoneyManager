angular.module('moneyManager').config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
    $urlRouterProvider.otherwise('/dashboard');
    $locationProvider.html5Mode(true);

      $stateProvider
         .state('dashboard', {
             url: '/dashboard?from&to',
             data: { requireLogin: true },
             views: {
                 'filters': {
                     templateUrl: 'app/components/home/filterTpl.html',
                     controller: function ($scope) { },
                 },
                 'content': {
                     controller: 'homeController',
                     templateUrl: 'app/components/home/home.html'
                 }
             },
             params: {
                 from: {
                     value: new Date(new Date().setDate(1)).yyyymmdd(),
                     squash: true
                 },
                 to: {
                     value: new Date(new Date().setDate(new Date(new Date().getYear(), new Date().getMonth() + 1, 0).getDate())).yyyymmdd(),
                     squash: true
                 }
             }
         })
        .state('login', {
            url: '/login',
            views: {
                'filters': {
                    controller: function ($scope) { },
                    templateUrl: 'app/components/category/filterTpl.html',
                },
                'content': {
                    controller: 'loginController',
                    templateUrl: 'app/components/login/login.html'
                }
            }
        })
        .state('categories', {
            url: '/categories',
            data: { requireLogin: true },
            views: {
                'filters': {
                    templateUrl: 'app/components/category/filterTpl.html',
                    controller: function ($scope) { },
                },
                'content': {
                    controller: 'categoriesController',
                    templateUrl: 'app/components/category/categories.html'
                }
            }
        })
        .state('incomes', {
            url: '/incomes?from&to&page&sort&asc',
            data: { requireLogin: true },
            views: {
                'filters': {
                    templateUrl: 'app/components/income/filterTpl.html',
                    controller: function ($scope) { },
                },
                'content': {
                    controller: 'incomesController',
                    templateUrl: 'app/components/income/incomes.html'
                }
            },
            params: {
                from: {
                    value: new Date(new Date().setDate(1)).yyyymmdd(),
                    squash: true
                },
                to: {
                    value: new Date(new Date().setDate(new Date(new Date().getYear(), new Date().getMonth() + 1, 0).getDate())).yyyymmdd(),
                    squash: true
                },
                page: {
                    value: '0',
                    squash: true
                },
                sort: {
                    value: 'Date',
                    squash: true
                },
                asc: {
                    value: 'desc',
                    squash: true
                }
            }
        })
        .state('expenses', {
            url: '/expenses?from&to&page&sort&asc',
            data: { requireLogin: true },
            views: {
                'filters': {
                    templateUrl: 'app/components/expense/filterTpl.html',
                    controller: function ($scope) { },
                },
                'content': {
                    controller: 'expensesController',
                    templateUrl: 'app/components/expense/expenses.html'
                }
            },
            params: {
                from:{
                    value: new Date(new Date().setDate(1)).yyyymmdd(),
                    squash: true
                },
                to: {
                    value: new Date(new Date().setDate(new Date(new Date().getYear(), new Date().getMonth() + 1, 0).getDate())).yyyymmdd(),
                    squash: true
                },
                page: {
                    value: '0',
                    squash: true
                },
                sort: {
                    value: 'Date',
                    squash: true
                },
                asc: {
                    value: 'desc',
                    squash: true
                }
            }
        });
});

angular.module('moneyManager').run(function ($rootScope, $state, $location, authService) {

    $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState) {

        var shouldLogin = toState.data !== undefined
                      && toState.data.requireLogin
                      && !authService.isLoggedIn();

        if (shouldLogin) {
            $state.go('login');
            event.preventDefault();
            return;
        }

        //if (authService.isLoggedIn()) {
        //    var shouldGoToMain = fromState.name === ""
        //                      && toState.name !== "dashboard";

        //    if (shouldGoToMain) {
        //        $state.go('dashboard');
        //        event.preventDefault();
        //    }
        //    return;
        //}
    });
});