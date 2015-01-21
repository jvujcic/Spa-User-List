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
            $scope.userToUpdate.emails.push({ "emailAddress": "" });
        })
        $scope.userToEditId = id;
    }

    $scope.cancelEditing = function () {
        $scope.userToEditId = -1; $scope.userToUpdate = {};
    }

    $scope.updateUser = function () {
        for (var i = 0; i < $scope.userToUpdate.emails.length; i++) {
            if ($scope.userToUpdate.emails[i].emailAddress == "")
                delete $scope.userToUpdate.emails[i];
        }
        userDataService.updateUser($scope.userToUpdate, function (date) {
            $scope.getAllUsers();
            $scope.userToEditId = -1;
            $scope.userToUpdate = {};
        });
    }

    $scope.addRowEmail = function (emails, index) {
        if (index == emails.length - 1) {
            emails.push({ "emailAddress": "" });
        }
    }

    $scope.getAllUsers();
}])