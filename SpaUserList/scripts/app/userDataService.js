/// <reference path="../angular.js" />

angular.module('userList')
.service('userDataService', ['$http', 'toaster', function ($http, toaster) {
    webApiUrl = "/api/users/";

    this.getUsers = function (query, callback) {
        if (query == "") {
            $http.get(webApiUrl).success(function (response) {
                callback(response);
            }).error(function (response) {
                toaster.pop("error", "ERROR", "Can't retrieve users !!!");
            });
        }
        else {
            $http.get(webApiUrl + "search/" + query).success(function (response) {
                callback(response);
            }).error(function (response) {
                toaster.pop("error", "ERROR", "Can't retrieve users !!!");
            });
        }
    }
    
    this.getUser = function (id, callback) {
        $http.get(webApiUrl + id).success(function (response) {
            callback(response);
        }).error(function (respone) {
            toaster.pop("error", "ERROR", "Can't retrieve user !!!");
        });
    }

    this.updateUser = function (user, callback) {
        $http.put(webApiUrl + user.userId, user).success(function (response) {
            toaster.pop("success", "SUCCESS", "User updated !")
            callback(response);
        }).error(function (response, status) {
            toaster.pop("error", "ERROR", response);
        });
    }

    this.addUser = function (user, callback) {
        $http.post(webApiUrl, user).success(function (response) {
            toaster.pop("success", "SUCCESS", "Added new user !");
            callback(response);
        }).error(function (response) {
            toaster.pop("error", "ERROR", response);
        });
    }

    this.deleteUser = function (id, callback) {
        $http.delete(webApiUrl + id).success(function (response) {
            toaster.pop("success", "SUCCESS", "User deleted !");
            callback(response);
        }).error(function (response) {
            toaster.pop("success", "ERROR", response.message);
        });
    }
}])