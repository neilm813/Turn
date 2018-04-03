angular.module('CRMApp').controller('userController', function ($scope, $http, $state, $stateParams, userService) {


    // check to make sure a user is signed in
    if (document.cookie == "") {
        $state.go('login')
    }
    // returns all of the users in the database
    $scope.getUsers = function () {
        $http.get('http://localhost:3000/users')
            .then(function (response) {
                userService.index(response.data.users);
            })
    }
    // users
    $scope.getUsers();
    $scope.userID = null;
    $scope.email - '';
    $scope.password = '';
    $scope.newFirstName = '';
    $scope.newLastName = '';
    $scope.newEmail = '';
    $scope.confirmEmail = '';
    $scope.newPassword = '';
    $scope.confirmPassword = '';
    $scope.recoverEmail = '';
    $scope.test = function () {
        alert(document.cookie)
    }


    // sets the logged in user based the cookie
    $scope.index = function () {
        if (document.cookie !== "") {
            $http.get('http://localhost:3000/getCookie')
                .then(function (response) {
                    var Id = response.data.user;
                    $http.get('http://localhost:3000/users/' + Id)
                        .then(function (response) {
                            userService.setLoggedInUser(response.data.user);
                            $state.go('home')
                        })
                })
        }
        else {
            $state.go('login')
        };
    }

    // login
    $scope.login = function () {
        $http.get(`http://localhost:3000/users?email=${$scope.email}&password=${$scope.password}`)
            .then(function (response) {
                if (response.data.userExists == true) {
                    userService.setLoggedInUser(response.data.user);
                    $http.get('http://localhost:3000/createCookie?Id=' + response.data.user.Id)
                        .then(function (response) {
                            $state.go('home');
                        })
                }
                else {
                    alert('Invalid Email or Password')
                }
            })
    };

    // create a new user
    $scope.newUser = function () {
        $http.post('http://localhost:3000/users', { firstName: $scope.newFirstName, lastName: $scope.newLastName, email: $scope.newEmail, password: $scope.newPassword })
            .then(function (response) {
                if (response.data.userCreated == true) {
                    userService.setLoggedInUser(response.data.user);
                    $http.get('http://localhost:3000/createCookie?Id=' + response.data.user.Id)
                        .then(function (response) {
                            $scope.getUsers();
                            $state.go('home');
                        })
                }
                else {
                    alert('One or more entries are invalid. Please check your information and try again.')
                }
            })
    };

    // opens the recover account modal
    $scope.recoverAccountModal = function () {
        $('#recoverAccountModal').modal();
    }

    // sends an email to user with their password
    $scope.recover = function () {
        $http.get(`http://localhost:3000/users?email=${$scope.recoverEmail}`)
            .then(function (response) {
                alert(response.data.message);
            })
    }
});
