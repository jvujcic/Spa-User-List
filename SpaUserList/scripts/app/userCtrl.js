﻿/// <reference path="../angular.js" />

angular.module('userList')
.controller('UserCtrl', ['$scope', 'userDataService', function ($scope, userDataService) {
    $scope.query = "";
    $scope.favorite = false;

    $scope.init = function () {
        $scope.getUsers($scope.query);
        $scope.userToEditId = -1;
        $scope.userToUpdate = {};
        $scope.showAddUser = false;
    }

    $scope.getUsers = function (query) {
        userDataService.getUsers(query, function (data) {
            $scope.userList = data;
        });
    }

    $scope.editUser = function (id) {
        userDataService.getUser(id, function (data) {
            $scope.userToUpdate = data;
            $scope.userToUpdate.emails.push({ 'emailAddress': "" });
            $scope.userToUpdate.tags.push({ 'name': "" });
            $scope.userToUpdate.telephoneNumbers.push({ 'number': "" });
        })
        $scope.userToEditId = id;
        $scope.showAddUserForm(false);
    }

    $scope.cancelEditing = function () {
        $scope.userToEditId = -1;
        $scope.userToUpdate = {};
    }

    $scope.updateUser = function () {
        userDataService.updateUser($scope.userToUpdate, function (data) {
            $scope.init();
        });
    }

    $scope.addUser = function () {
        userDataService.addUser($scope.userToUpdate, function (data) {
            $scope.init();
        });
    }

    $scope.deleteUser = function (id) {
        userDataService.deleteUser(id, function (data) {
            $scope.init();
        });
    }

    $scope.addRow = function (elements, index, type) {
        if (index == elements.length - 1) {
            if (type == "email") elements.push({ 'emailAddress': "" });
            if (type == "tag") elements.push({ 'name': "" });
            if (type == 'tel') elements.push({ 'number' : "" });
        }
    }

    $scope.showAddUserForm = function (show) {
        if (show) {
            $scope.userToUpdate = {
                'emails': [{ 'emailAddress': "" }],
                'tags': [{ 'name': "" }],
                'telephoneNumbers': [{ 'number': "" }],
                favorite : false
            };
            $scope.userToEditId = -1;
        } else {
            $scope.userToUpdate = {};
        }
        $scope.showAddUser = show;
    }

    $scope.searchUser = function () {
        $scope.init();
    }

    $scope.cancelSearch = function () {
        $scope.query = "";
        $scope.init();
    }

    

    $scope.init();
}])