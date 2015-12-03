/**
 * Created by Scott on 10/7/2015.
 */

define(['jquery', 'views/AdminResultDisplay'], function ($, AdminResultDisplay) {

    function ClientCommunicationModule() {
    }

    //var output_display = new ResultDisplay();
    var admin_display = new AdminResultDisplay();
    var serverAddress = "https://localhost:32297";

    ClientCommunicationModule.prototype.onResultReceived = function(result){};

    ClientCommunicationModule.prototype.sendAnswers = function (answers) {
        var that = this;
        $.ajax({
            url: serverAddress,            // Location of the service
            type: "POST",                  //GET or POST or PUT or DELETE verb
            data: JSON.stringify(answers), //Data sent to server
            dataType: "text",
            success: function (in_data) {  //On Successful service call
                var result = JSON.parse(in_data);
                that.onResultReceived(result);
                //output_display.updateResultDisplay(result);
            },
            error: (function () {          //When Service call fails
                alert("Error: Question posting unavailable");
            })
        });
    };

    ClientCommunicationModule.prototype.getQuestions = function (onReceive) {
        $.ajax({
            url: serverAddress, 		// Location of the service
            type: "GET", 		//GET or POST or PUT or DELETE verb
            dataType: "text",
            success: function (in_data) {//On Successful service call
                // **** FOR DEBUGGING --- alert(in_data);
                var result = JSON.parse(in_data);
                onReceive(result);
            },
            error: (function () {
                alert("Error: Questions unretrievable");
            })
        })
    };

    ClientCommunicationModule.prototype.insertData = function (data) {
        //var out = JSON.stringify(data);
        //var in_data = JSON.parse(out);
        //admin_display.updateResultDisplay(in_data);
        $.ajax({
            url: serverAddress, 		// Location of the service
            type: "PUT", 		//GET or POST or PUT or DELETE verb
            data: JSON.stringify(this.appendCredentials(data)), //Data sent to server //"This is a test",
            dataType: "text",
            success: function (in_data) {//On Successful service call
                //alert("input = " + in_data);
                var result = JSON.parse(in_data);
                //                alert("result=" + result);

                var output = "";
                if (result.Result == "check username & password") {
                    output = output + "<p>Operation Failed - invalid credentials</p>";
                } else {
                    output = output + "<p><b>Added Element: </b><br//>";
                    output = output + "PlantId: " + result.PlantId + "<br//>";
                    output = output + "Name: " + result.Name + "<br//>";
                    output = output + "ImageURL: " + result.ImageURL + "<br//>";
                    output = output + "USState: " + result.USState + "<br//>";
                    output = output + "Type: " + result.Type + "<br//>";
                    output = output + "ColorFlower: " + result.ColorFlower + "<br//>";
                    output = output + "ColorFoliage: " + result.ColorFoliage + "<br//>";
                    output = output + "ColorFruitSeed: " + result.ColorFruitSeed + "<br//>";
                    output = output + "Shape: " + result.Shape + "<br//>";
                    output = output + "TextureFoliage: " + result.TextureFoliage + "<br//>";
                    output = output + "Pattern: " + result.Pattern + "<br//>";
                    output = output + "<//p>";
                }
                admin_display.updateResultDisplay(output);
            },
            error: (function () {
                alert("Error: data insertion failed");
            })	// When Service call fails
        });

    };

    ClientCommunicationModule.prototype.deleteData = function (data) {
        var check = window.confirm("Are you sure you want to delete " + data.PlantId + "?");
        if (check) {
            $.ajax({
                url: serverAddress, 		// Location of the service
                type: "DELETE", 		//GET or POST or PUT or DELETE verb
                data: JSON.stringify(this.appendCredentials(data)), //Data sent to server //"This is a test",
                dataType: "text",
                success: function (in_data) {//On Successful service call
                    //alert("input = " + in_data);
                    var result = JSON.parse(in_data);
                    //                alert("result=" + result);

                    var output = "";
                    output = output + "<p>";
                    output = output + "PlantId " + result.PlantId + ":  " + result.Result + "<br//>";
                    output = output + "<//p>";

                    admin_display.updateResultDisplay(output);
                },
                error: (function () {
                    alert("Error: data deletion failed");
                })	// When Service call fails
            });
        }
    };

    ClientCommunicationModule.prototype.readCookie = function(name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for(var i=0;i < ca.length;i++) {
            var c = ca[i];
            while (c.charAt(0)==' ') c = c.substring(1,c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
        }
        return null;
    }

    ClientCommunicationModule.prototype.readCredentials = function() {
        var credentials = {};
        var un = this.readCookie("username");
        var pw = this.readCookie("password");
        if (un != null){
            credentials["UserName"] = un;
        } else {
            credentials["UserName"] = ".";
        }
        if (pw != null){
            credentials["Password"] = pw;
        } else {
            credentials["Password"] = ".";
        }
        console.log("credentials: " + credentials["UserName"] + " " + credentials["Password"]);
        return credentials;
    }

    ClientCommunicationModule.prototype.appendCredentials = function(data) {
        var credentials = this.readCredentials();
        data["UserName"] = credentials["UserName"];
        data["Password"] = credentials["Password"];
        return data;
    }

    ClientCommunicationModule.prototype.createCookie = function(name,value,units) {
        if (units) {
            var date = new Date();
            date.setTime(date.getTime()+(units*30*60*1000));
            var expires = "; expires="+date.toGMTString();
        }
        else var expires = "";
        document.cookie = name+"="+value+expires+"; path=/";
    }

    ClientCommunicationModule.prototype.eraseCookie = function(name) {
        this.createCookie(name,"",-1);
    }

    ClientCommunicationModule.prototype.setCredentials = function(username,password) {
        this.createCookie("username",username,0);
        this.createCookie("password",password,0);
    }

    ClientCommunicationModule.prototype.checkCredentials = function() {
        $.ajax({
            url: serverAddress, 		// Location of the service
            type: "PUT", 		//GET or POST or PUT or DELETE verb
            data: JSON.stringify(this.readCredentials()), //Data sent to server //"This is a test",
            dataType: "text",
            success: function (in_data) {//On Successful service call
                var result = JSON.parse(in_data);
                if (!(result.Result == "Authenticated")) {
                    window.location = "./OWLLogin.html";
                }
            },
            error: (function () {
                alert("Error: credentials check failed");
            })	// When Service call fails
        });
    }

    ClientCommunicationModule.prototype.login = function (data) {
        var self = this;
       $.ajax({
            url: serverAddress, 		// Location of the service
            type: "PUT", 		//GET or POST or PUT or DELETE verb
            data: JSON.stringify(data), //Data sent to server //"This is a test",
            dataType: "text",
            success: function (in_data) {//On Successful service call
                //alert("input = " + in_data);
                var result = JSON.parse(in_data);
                var authenticated = result.Result == "Authenticated";
                console.log("credentialCheck=" + authenticated);
                    if (authenticated) {
//                        alert("Login succeeded.");
                        self.setCredentials(result.UserName, result.Password);
                        window.location = "./OWLAdmin.html";
                    } else {
                        alert("Login failed. Please check your username and password.");
                    }
            },
            error: (function () {
                alert("Error: login attempt failed");
            })	// When Service call fails
        });
    };

    ClientCommunicationModule.prototype.logout = function() {
        this.eraseCookie("username");
        this.eraseCookie("password");
        window.location = "./OWLLogin.html";
    };

    return ClientCommunicationModule;
});


