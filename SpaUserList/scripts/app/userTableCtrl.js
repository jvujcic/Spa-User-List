/// <reference path="../angular.js" />

angular.module('userList')
.controller('UserTableCtrl', ['$scope', 'userDataService', function ($scope, userDataService) {
    $scope.userList = [];

    $scope.userList = userDataService.getAllUsers(function (data) {
        $scope.userList = data;
    });
}])