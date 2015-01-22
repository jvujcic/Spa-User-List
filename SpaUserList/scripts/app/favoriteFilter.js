/// <reference path="../angular.js" />

angular.module('userList')
.filter('favoriteFilter', function () {
    return function (users, favoriteFlag) {
        if (favoriteFlag == false) {
            return users;
        } else {
            return users.filter(function (user) {
                return user.favorite;
            });
       }
    }
})