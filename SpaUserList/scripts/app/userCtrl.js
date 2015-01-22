/// <reference path="../angular.js" />

angular.module('userList')
.controller('UserCtrl', ['$scope', 'userDataService', function ($scope, userDataService) {
    $scope.query = "";
    $scope.favorite = false;

    var init = function () {
        $scope.getUsers($scope.query);
        $scope.userToUpdate = { 'userId' : -1 };
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
        $scope.showAddUserForm(false);
    }

    $scope.cancelEditing = function () {
        $scope.userToUpdate.userId = -1;
    }

    $scope.updateUser = function () {
        userDataService.updateUser(createUserForPost(), function (data) {
            init();
        });
    }

    $scope.addUser = function () {
        userDataService.addUser(createUserForPost(), function (data) {
            init();
        });
    }

    $scope.deleteUser = function (id) {
        userDataService.deleteUser(id, function (data) {
            init();
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
                'userId' : -1,
                favorite : false
            };
        } else {
            $scope.userToUpdate = { 'userId' : -1 };
        }
        $scope.showAddUser = show;
    }

    $scope.searchUser = function () {
        init();
    }

    $scope.cancelSearch = function () {
        $scope.query = "";
        init();
    }

    var createUserForPost = function() {
        newUser = JSON.parse(JSON.stringify($scope.userToUpdate));
        newUser.emails = newUser.emails.filter(function (email) {
            return email.emailAddress != "";
        });
        newUser.tags = newUser.tags.filter(function (tag) {
            return tag.name != "";
        });
        newUser.telephoneNumbers = newUser.telephoneNumbers.filter(function (tel) {
            return tel.number != "";
        });
        return newUser;
    }

    init();
}])