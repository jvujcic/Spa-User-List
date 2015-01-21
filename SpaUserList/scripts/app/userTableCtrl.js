/// <reference path="../angular.js" />

angular.module('userList')
.controller('UserTableCtrl', ['$scope', 'userDataService', function ($scope, userDataService) {

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
        $scope.userToEditId = -1;
        $scope.userToUpdate = {};
    }

    $scope.updateUser = function () {
        $scope.userToUpdate.emails.pop();
        $scope.userToUpdate.tags.pop();
        userDataService.updateUser($scope.userToUpdate, function (data) {
            $scope.init();
        });
    }

    $scope.deleteUser = function (id) {
        userDataService.deleteUser(id, function (data) {
            $scope.init();
        });
    }

    $scope.addRow = function (elements, index) {
        if (index == elements.length - 1) {
            elements.push({});
        }
    }

    $scope.init = function () {
        $scope.getAllUsers();
        $scope.userToEditId = -1;
        $scope.userToUpdate = {};
    }

    $scope.init();
}])