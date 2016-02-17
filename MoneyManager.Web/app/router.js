angular.module('moneyManager').config(function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise('/dashboard');

    $stateProvider
         .state('dashboard', {
             url: '/dashboard',
             controller: 'homeController',
             templateUrl: 'app/components/home/home.html'
         })
        .state('login', {
            url: '/login',
            controller: 'loginController',
            templateUrl: 'app/components/login/login.html'
        })
        .state('categories', {
            url: '/categories',
            controller: 'categoriesController',
            templateUrl: 'app/components/category/categories.html'
        })
        .state('category', {
            url: '/category',
            controller: 'categoryController',
            templateUrl: 'app/components/category/category.html'
        })
        .state('incomes', {
            url: '/incomes',
            controller: 'incomesController',
            templateUrl: 'app/components/income/incomes.html'
        })
        .state('expenses', {
            url: '/expenses',
            controller: 'expensesController',
            templateUrl: 'app/components/expense/expenses.html'
        });
});