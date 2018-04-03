var app = angular.module('CRMApp', ['ui.router']);

app.config(function ($stateProvider, $urlRouterProvider) {

  // $urlRouterProvider.otherwise('/login'); // default page

  $stateProvider.state('home', {
    url: '/home',
    templateUrl: './views/home.html',
    controller: 'customerController'
  })
  .state('customer', {
    url: '/customer',
    templateUrl: './views/customer.html',
    controller: 'customerController'
  })
  .state('login', {
    url: '/login',
    templateUrl: './views/login.html',
    controller: 'userController'
  })
});
