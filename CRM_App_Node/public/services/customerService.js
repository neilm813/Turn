angular.module("CRMApp").service("customerService", function ($http) {

  // customer service variables
  this.test = 'service works!';
  this.customers = [];
  var _selectedCustomer = {};

  // set all customers
  this.index = function (allCustomers) {
    this.customers = allCustomers;
  }

  // set the selected Customer
  this.setSelectedCustomer = function (customer) {
    _selectedCustomer = customer;
  }

  // returns the selected Customer
  this.getSelectedCustomer = function () {
    return _selectedCustomer;
  }

});

