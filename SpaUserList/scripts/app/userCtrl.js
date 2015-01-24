/// <reference path="../angular.js" />

angular.module('userList')
.controller('UserCtrl', ['$scope', 'userDataService', function ($scope, userDataService) {
    $scope.status = {
        query: "",
        favorite: false,
        editId: -1,
        editing: false,
        newUser: false,
    }

    var init = function () {
        $scope.getUsers($scope.status.query);
        $scope.status.editUser = -1;
        $scope.status.newUser = false;
        $scope.status.editing = false;
        $scope.status.editId = -1;
    }

    $scope.getUsers = function (query) {
        userDataService.getUsers(query, function (data) {
            $scope.userList = data;
        });
    }

    $scope.editUser = function (id) {
        $scope.status.editId = id;
        $scope.status.editing = true;
        if (status.editId !== -1) {
            userDataService.getUser(id, function (data) {
                $scope.userToUpdate = data;
                $scope.userToUpdate.emails.push({ 'emailAddress': "" });
                $scope.userToUpdate.tags.push({ 'name': "" });
                $scope.userToUpdate.telephoneNumbers.push({ 'number': "" });
            });
        }
    }

    $scope.cancelEditing = function () {
        $scope.status.editId = -1;
        $scope.status.editing = false;
        if ($scope.status.newUser === true) {
            $scope.status.newUser = false;
            $scope.userList.pop();
        }
    }

    $scope.saveUser = function () {
        if ($scope.userToUpdate.userId === -1) {
            userDataService.addUser(createUserForPost($scope.userToUpdate), function (data) {
                init();
            });
        } else {
            userDataService.updateUser(createUserForPost($scope.userToUpdate), function (data) {
                init();
            });
        }
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

    $scope.showAddUserForm = function () {
        $scope.userList.push({
            userId: -1,
        });
        $scope.userToUpdate = {
            userId: -1,
            emails: [{ 'emailAddress': "" }],
            tags: [{ 'name': "" }],
            telephoneNumbers: [{ 'number': "" }],
            favorite: false
        };
        $scope.status.editing = true;
        $scope.status.newUser = true;
    }

    $scope.searchUser = function () {
        init();
    }

    $scope.cancelSearch = function () {
        $scope.status.query = "";
        init();
    }

    var createUserForPost = function(user) {
        newUser = JSON.parse(JSON.stringify(user));
        newUser.emails = newUser.emails.filter(function (email) {
            return email.emailAddress !== "";
        });
        newUser.tags = newUser.tags.filter(function (tag) {
            return tag.name !== "";
        });
        newUser.telephoneNumbers = newUser.telephoneNumbers.filter(function (tel) {
            return tel.number !== "";
        });
        return newUser;
    }

    init();
}])