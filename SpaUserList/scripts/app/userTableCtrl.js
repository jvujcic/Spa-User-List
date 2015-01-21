/// <reference path="../angular.js" />

angular.module('userList')
.controller('UserTableCtrl', ['$scope', 'userDataService', function ($scope, userDataService) {
    $scope.userList = [];
    $scope.userToEditId = -1;
    $scope.userToUpdate = {};

    $scope.getAllUsers = function () {
        userDataService.getAllUsers(function (data) {
            $scope.userList = data;
        });
    }

    $scope.editUser = function (id) {
        userDataService.getUser(id, function (data) {
            $scope.userToUpdate = data;
            $scope.userToUpdate.emails.push({});
            $scope.userToUpdate.tags.push({});
        })
        $scope.userToEditId = id;
    }

    $scope.cancelEditing = function () {
        $scope.userToEditId = -1; $scope.userToUpdate = {};
    }

    $scope.updateUser = function () {
        $scope.userToUpdate.emails.pop();
        $scope.userToUpdate.tags.pop();
        userDataService.updateUser($scope.userToUpdate, function (date) {
            $scope.getAllUsers();
            $scope.userToEditId = -1;
            $scope.userToUpdate = {};
        });
    }

    $scope.addRowEmail = function (elements, index) {
        if (index == elements.length - 1) {
            elements.push({});
        }
    }

    $scope.getAllUsers();
}])