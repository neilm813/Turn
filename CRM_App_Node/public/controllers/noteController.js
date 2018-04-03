angular.module('CRMApp').controller('noteController', function ($scope, $http, noteService, customerService) {
    //    checks to make sure a user is logged in
    if (document.cookie == "") {
        $state.go('login')
    }

    // notes
    $scope.noteService = noteService;
    $scope.notes = '';

    // returns all notes
    $scope.index = function () {
        $scope.notes = 'loading';

        $http.get('http://localhost:3000/notes')
            .then(function (response) {
                $scope.notes = response.data.message;
                noteService.index(response.data.message);
            })
    }

});