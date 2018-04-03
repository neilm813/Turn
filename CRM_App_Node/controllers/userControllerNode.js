var axios = require('axios');

function index(req, res) {
    // get all users
    if (!req.query.email && !req.query.password) {
        axios.get('http://localhost:50313/api/users')
            .then(function (response) {
                res.json({ users: response.data });
            })
            .catch(function (error) {
            });
    }
    // emails a user their account password
    else if (!req.query.password) {
        axios.get('http://localhost:50313/api/users?email=' + req.query.email)
            .then(function (Response) {
                res.json({ message: 'An Email has been sent to you with your password' })
            })
            .catch(function (response) {
                res.json({ message: 'ERROR No account found for that specific email.' })
            })
    }
    // login to user account
    else {
        axios.get('http://localhost:50313/api/users?email=' + req.query.email + '&password=' + req.query.password)
            .then(function (response) {
                res.json({ user: response.data, userExists: true });
            })
            .catch(function (error) {
                res.json({ userExists: false })
            });
    }
}

// get specific user
function show(req, res) {
    axios.get('http://localhost:50313/api/users/' + req.params.id)
        .then(function (response) {
            res.json({ user: response.data });
        })
        .catch(function (error) {
            res.json(error);
        });

}

// create new user
function create(req, res) {
    axios.post('http://localhost:50313/api/users', req.body)
        .then(function (response) {
            res.json({ user: response.data, userCreated: true })
        })
        .catch(function (error) {
            res.json({ userCreated: false })
        });
}

// update user
function update(req, res) {
    if (req.body.Email == '' || req.body.FirstName == '' || req.body.LastName == '') {
        res.json({ edited: false })
    }
    else {
        axios.put('http://localhost:50313/api/users/' + req.body.Id, req.body)
            .then(function (response) {
                res.json({ edited: true, user: response.data })
            })
            .catch(function (error) {
                res.json({ edited: false })
            })
    }
}

// delete user
function destroy(req, res) {
    var Id = req.params.id;
    axios.delete('http://localhost:50313/api/users/?id=' + Id)
        .then(function (response) {
            res.json({ deleted: true })
        })
        .catch(function (error) {
            res.json({ deleted: false })
        })
}
module.exports = {
    index: index,
    show: show,
    create: create,
    update: update,
    destroy: destroy
}