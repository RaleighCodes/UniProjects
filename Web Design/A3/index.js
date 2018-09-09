// James Raleigh 10150801
// Seng 513
// A3
// https://github.com/socketio/socket.io/tree/master/examples/chat

var express = require('express');
var app = express();
var path = require('path');
var http = require('http').createServer(app);
var cookie = require('cookie');
var io = require('socket.io')(http);

//http://www.99colors.net/
var fruits = ["Apple", "Banana", "Plum", "Peach", "Coconut", "Grape", "Orange", "Avocado", "Tomato", "Starfruit"]
var colors =["#F06292", "#0000FF", "#9575CD", "#7986CB", "#64B5F6", "#4FC3F7", "#4DD0E1", "#4DB6AC", "#81C784", "#AED581", "#DCE775", "#007FFF", "#FFD54F", "#FFB74D", "#FF8A65", "#A1887F"];
var randFruit = fruits[Math.floor(Math.random() * fruits.length)];
var randColor = colors[Math.floor(Math.random() * colors.length)];

// http://www.fullstacktraining.com/articles/how-to-serve-static-files-with-express
// load the index file
app.use(express.static(path.join(__dirname, 'public')));

http.listen(3000, function(){
    console.log('listening on *:' + 3000);
});

var maxUsers = 5;
var maxLogEntries = 10;
var colorIndex = 0;
var fruitIndex = 0;
var userIndex = 0;
var connectedUsers = {};
var chatLog = [];

io.on('connection', (socket) => {
    //https://www.w3schools.com/js/js_cookies.asp
    //https://stackoverflow.com/questions/4034190/cookie-is-not-a-function
    //https://stackoverflow.com/questions/4825683/how-do-i-create-and-read-a-value-from-cookie
    //Connect users to the server
    //New cookies created or old ones used based on check
    
    let user;
    try {
        // try to restore existing user
        cookieData = cookie.parse(socket.request.headers.cookie);
        user = new User(
            cookieData.id,
            cookieData.name,
            cookieData.color
        );
        let reactivated = (user.name == cookieData.name);
        console.log(user.name + ", welcome back!");

    } catch (e) {
        // create a new one if that fails
        user = new User();
        console.log(user.name + " has joined fruit basket!");
        console.log("COMMANDS: ");
        console.log("/nick changes your name");
        console.log("/nickcolor changes the nickname color")
    }

    socket.emit('connected', user);
    updateUserList();
    sendMessageHistory();

    //On message recieve
    //do some checking
    // https://stackoverflow.com/questions/16667329/special-character-validation
    socket.on('chat message', (txt) => {
        //check for emptry strings
        if ((/\S/.test(txt)) && !(/[^a-zA-Z0-9\-\/]/.test(txt))){
            msg = new Message(txt, user);
            console.log(msg.toString());
            io.emit('chat message', msg);
            chatLog.push(msg);
            chatLog.slice(chatLog.length - maxLogEntries);
        }
        //Name change
        if (txt.startsWith("/nick ")) {
            oldNick = user.name;
            newNick = txt.split(' ').slice(1).join(' ');
            if (user.changeNick(newNick)) {
                updateUserList();
                socket.emit('update', user);
                console.log(oldNick + " changes nickname to " + newNick);
            }
        } 
        //Color change
        else if (txt.startsWith("/nickcolor ")) {
            oldColor = user.color;
            newColor = txt.split(' ')[1];
            if (user.changeColor(newColor) && newColor.length == 6) {
                console.log(user.name + " changes nickname color from " + oldColor + " to #" + newColor);
            }
        }
    });

    //On disconnect
    socket.on('disconnect', () => {
        user.deactivate();
        updateUserList();
        console.log(user.name + " leaves the fruit basket.");
    });

    //Update
    function updateUserList() {
        io.emit('update users', Object.keys(connectedUsers).sort());
    }

    function sendMessageHistory() {
        chatLog.forEach((msg) => {
            socket.emit('chat message', msg);
        });
    }

});

//create a user class for color/id/name management
class User {
    //create user names with color and fruit name
    //randomly set names and colors
    constructor(id, name, color) {
        this.id = id || userIndex++;
        do {
            if (name) {
                this.name = name;
            } else {
                this.name = randFruit;
            }
            name = "";
          
            if (color) {
                this.color = color;
            } else {
                this.color = randColor;
            }
            color = "";
        }
        while (connectedUsers.hasOwnProperty(name));
        
        connectedUsers[this.name] = this;
        return this;
    }

    //change name 
    changeNick(name) {
        if (name != "" && !connectedUsers.hasOwnProperty(name)) {
            delete connectedUsers[this.name];
            this.name = name;
            connectedUsers[this.name] = this;
            return true;
        }
        return false;
    }
    
    //change color 
    changeColor(color) {
        let validHex = RegExp(/^[\dA-Fa-f]{6}$/);
        if (validHex.test(newColor)) {
            this.color = '#' + color;
            return true;
        }
        return false;
    }

    deactivate() {
        delete connectedUsers[this.name];
    }
}

//create a message class 
class Message {
    constructor(txt, user) {
        this.txt = txt;
        this.color = user.color;
        this.uname = user.name;
        this.uid = user.id;
        this.time = Date.now();
    }
}

//outputs the time messages are sent
Message.prototype.toString = function() {
    let date = new Date(msg.time);
    let time = ('0' + date.getHours()).substr(-2) + ':' + ('0' + date.getMinutes()).substr(-2) + ':' + ('0' + date.getSeconds()).substr(-2);
    return time + ' ' + this.uname + ': ' + this.txt;
}
