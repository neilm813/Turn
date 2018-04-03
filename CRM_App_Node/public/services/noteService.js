angular.module("CRMApp").service("noteService", function ($http) {

  // note service  variables
  var _customerNotes = [];
  var _editedNotes = [];

  /* SETS
  ********************/

  // sets the notes for the selected Customer
  this.setSelectedCustomerNotes = function (notes) {
    _customerNotes = notes;
  }

  this.setEditedNotes = function (notes) {
    _editedNotes = notes;
  }

  /* GETS
  ********************/
  // returns the notes
  this.getCustomerNotes = function () {
    return _customerNotes;
  }

  this.getEditedNotes = function () {
    return _editedNotes;
  }

});

