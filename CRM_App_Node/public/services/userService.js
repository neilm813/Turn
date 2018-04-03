angular.module("CRMApp").service("userService", function ($http) {

  // user service variables
  this.test = 'service works!';
  this.users = [];
  this.selectedUser = {};
  this.loggedInUser = {};

  // sets all users
  this.index = function (allUsers) {
    this.users = allUsers;
  }

  // sets all users
  this.setAllUsers = function (users) {
    this.users = users;
  }

  // returns all users
  this.getAllUsers = function () {
    return this.users;
  }

  // sets the selected User
  this.setSelectedUser = function (user) {
    this.selectedUser = user;
  }

  // returns the selected User
  this.getSelectedUser = function () {
    return this.selectedUser;
  }

  // sets the logged in user
  this.setLoggedInUser = function (user) {
    this.loggedInUser = user;
  }
  // returns the logged in user
  this.getLoggedInUser = function () {
    return this.loggedInUser;
  }

});