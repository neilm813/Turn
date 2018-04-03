var express = require('express');
var router = express.Router();

var userController = require('../controllers/userControllerNode');
var customerController = require('../controllers/customerControllerNode');
var noteController = require('../controllers/noteControllerNode');

router.get('/', function (req, res) {
  res.render('index')
});

// user routes
router.get('/users', userController.index);
router.get('/users/:id', userController.show);
router.post('/users', userController.create);
router.put('/users/:id', userController.update);
router.delete('/users/:id', userController.destroy);

// customer routes
router.get('/customers', customerController.index);
router.get('/customers/byEmail', customerController.searchFCByEmail);
router.get('/customers/byPhone', customerController.searchFCByPhone);
router.post('/customers', customerController.create);
router.put('/customers/:id', customerController.update);
router.delete('/customers/:id', customerController.destroy);

// note routes
router.get('/notes', noteController.index);
router.get('/noteEdits', noteController.showEditHistory);
router.post('/notes', noteController.create);
router.post('/noteEdits', noteController.createEditedNote);
router.put('/notes/:id', noteController.update);
router.delete('/notes/:id', noteController.destroy);
router.delete('/noteEdits', noteController.destroyNoteEditsForNote);

module.exports = router;