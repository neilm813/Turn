var axios = require('axios');

function index(req, res) {
    // get all notes if req.query is falsy
    if (!req.query.customerId) {
        axios.get('http://localhost:50313/api/notes')
            .then(function (response) {
                var notes = response.data;
                res.json({ notes: notes });
            })
            .catch(function (error) {
                res.json(error);
            });
    }
    // else get notes for a specified customer
    else {
        axios.get('http://localhost:50313/api/notes?customerId=' + req.query.customerId)
            .then(function (response) {
                var notes = response.data;
                res.json({ notes: notes });
            })
            .catch(function (error) {
                res.json(error);
            });
    }
}

function create(req, res) {
    axios.post('http://localhost:50313/api/notes', req.body)
        .then(function (response) {
            res.json(response.data);
        })
        .catch(function (error) {
            res.json(error);
        });
}

function createEditedNote(req, res) {
    axios.post('http://localhost:50313/api/NoteEdits', req.body)
        .then(function (response) {
            res.json(response.data);
        })
        .catch(function (error) {
            res.json(error);
        })
}

function showEditHistory(req, res) {
    axios.get('http://localhost:50313/api/NoteEdits?noteId=' + req.query.noteId)
        .then(function (response) {
            res.json(response.data);
        })
        .catch(function (error) {
            res.json(error);
        })
}

function update(req, res) {
    axios.put('http://localhost:50313/api/notes/' + req.body.Id, req.body)
        .then(function (response) {
            res.json(response.data);
        })
        .catch(function (error) {
            res.json(error);
        })
}

function destroy(req, res) {
    axios.delete('http://localhost:50313/api/notes/?id=' + req.params.id)
        .then(function (response) {
            res.json(response);
        })
        .catch(function (error) {
            res.json(error);
        });
}

function destroyNoteEditsForNote(req, res) {
    axios.delete('http://localhost:50313/api/noteedits?NoteId=' + req.query.NoteId)
        .then(function (response) {
            res.json(response);
        })
        .catch(function (error) {
            res.json(error);
        })
}

module.exports = {
    index: index,
    create: create,
    showEditHistory: showEditHistory,
    destroy: destroy,
    destroyNoteEditsForNote: destroyNoteEditsForNote,
    update: update,
    createEditedNote: createEditedNote
}