/// <reference path="../angular.js" />

angular.module('userList')
.service('userDataService', ['$http', function ($http) {
    webApiUrl = "/api/users/";

    this.getUsers = function (query, callback) {
        if (query == "") {
            $http.get(webApiUrl).success(function (response) {
                callback(response);
            }).error(function () {
            });
        }
        else {
            $http.get(webApiUrl + "search/" + query).success(function (response) {
                callback(response);
            }).error(function () {
            });
        }

    }
    
    this.getUser = function (id, callback) {
        $http.get(webApiUrl + id).success(function (response) {
            callback(response);
        }).error(function () {
        });
    }

    this.updateUser = function (user, callback) {
        $http.put(webApiUrl + user.userId, user).success(function (response) {
            callback(response);
        }).error(function () {
        });
    }

    this.addUser = function (user, callback) {
        $http.post(webApiUrl, user).success(function (response) {
            callback(response);
        }).error(function () {

        });
    }

    this.deleteUser = function (id, callback) {
        $http.delete(webApiUrl + id).success(function (response) {
            callback(response);
        }).error(function () {
        });
    }
}])