'use strict';

authInterceptorService.$inject = ['$q', '$location', '$sessionStorage'];
function authInterceptorService($q, $location, $sessionStorage) {

    var authInterceptorServiceFactory = {};

    var _request = function (config) {

        config.headers = config.headers || {};

        var authData = $sessionStorage.authorizationData;
        if (authData && authData.token) {
            config.headers.Authorization = 'Bearer ' + authData.token;
        } else if($location.path() != '/login' && $location.path() != '/regiser') {
            $location.path('/login');
        }

        return config;
    }

    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            $location.path('/login');
        }
        return $q.reject(rejection);
    }

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}

module.exports = authInterceptorService;

