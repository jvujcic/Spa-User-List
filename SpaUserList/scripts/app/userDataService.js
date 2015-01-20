/// <reference path="../angular.js" />

angular.module('userList')
.service('userDataService', ['$http', function ($http) {
    webApiUrl = "/api/users/";

    this.getAllUsers = function (callback) {
        $http.get(webApiUrl).success(function (response) {
            callback(response);
        }).error(function () {
        });
    }
}])