/// <reference path="../angular.js" />

angular.module('userList')
.controller('UserTableCtrl', ['$scope', 'userDataService', function ($scope, userDataService) {
    $scope.userList = [];
    $scope.userToEditId = -1;
    $scope.userToUpdate = {};

    userDataService.getAllUsers(function (data) {
        $scope.userList = data;
    });

    $scope.editUser = function (id) {
        userDataService.getUser(id, function (data) {
            $scope.userToUpdate = data;
        })

        $scope.userToEditId = id;
    }

    $scope.cancelEditing = function () { $scope.userToEditId = -1; }

    $scope.updateUser = function () {
        userDataService.updateUser($scope.userToUpdate, function (date) {
        });
    }
}])