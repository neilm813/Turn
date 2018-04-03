var express = require('express');
var path = require('path');
var routes = require('./config/routes');
var bodyParser = require('body-parser');
var cookieParser = require('cookie-parser');
var session = require('express-session');
var morgan = require('morgan');
var mailGun = require('mailgun-js');
var PORT = process.env.Port || 3000;
var app = express();
var jwt = require('jsonwebtoken')
require('dotenv').load()

app.use(express.static(path.join(__dirname, 'public')));

app.use(morgan('dev'));
app.use(cookieParser());
app.use(session({secret: 'anystringoftext', saveUninitialized: true, resave: true}))
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

app.get('/createCookie', function(req,res){
    res.cookie('user' , req.query.Id).send('Cookie set');
});
app.get('/getCookie', function(req,res){
    cookieParser.JSONCookie(req.cookies);
    res.json({user: req.cookies.user})
    
});
app.get('/clearcookie', function(req,res){
     res.clearCookie('user').send('Cookie Cleared')
});

app.use('/' , routes);

app.listen(PORT, function() {
    console.log('Listening on Port 3000');
});

module.exports = app;