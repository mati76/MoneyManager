'use strict';

//angular.module('moneyManager.auth').factory('authService', ['$http', '$q', '$sessionStorage', 'API_END_POINT', function ($http, $q, $sessionStorage, API_END_POINT) {
authService.$inject = ['$http', '$q', '$sessionStorage', 'API_END_POINT'];

function authService($http, $q, $sessionStorage, API_END_POINT){

var uri = API_END_POINT + '/auth';

    var _loggedInUser = {
        userName: ''
    };

    return {
        register: register,
        login: login,
        logOut: logOut,
        isLoggedIn: isLoggedIn
    };

    function isLoggedIn() {
        return $sessionStorage.authorizationData != null;
    }

    function register(credentials) {
        return $http.post(API_END_POINT + '/api/account/register', JSON.stringify(credentials));
    }

    function login(credentials) {
        var data = "grant_type=password&username=" + credentials.userName + "&password=" + credentials.password;
        var deferred = $q.defer();
        $http.post(API_END_POINT + '/auth', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).then(function (response) {
            $sessionStorage.authorizationData = { token: response.data.access_token, userName: credentials.userName };

            deferred.resolve(response);
        }, function (err) {
            logOut();

            deferred.reject(err);
        });
        return deferred.promise;
    }

    function logOut() {
        $sessionStorage.authorizationData = null;
    }

}//]);

module.exports = authService;