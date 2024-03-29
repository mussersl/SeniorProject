﻿import { error } from 'jquery';
import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';

export class Login extends Component {
    static displayName = Login.name;

    constructor(props) {
        super(props);
        this.state = { verified: "Loading" };
        this.verifyLogin = this.verifyLogin.bind(this);
    }

    async fetchVerification() {
        let certificate = sessionStorage.getItem("verificationIRPAChatbot");
        const result = await fetch('ChatBot/VerifyLogin/' + certificate);
        const response = await result.text();
        this.setState({ verified: response });
        return;
    }

    async verifyLogin() {
        let username = document.getElementById("usernameirpa").value;
        let password = document.getElementById("passwordirpa").value;
        const result = await fetch('ChatBot/Login/' + username + '/' + password);
        const response = await result.text();
        sessionStorage.setItem("verificationIRPAChatbot", response);
        this.fetchVerification();
        document.getElementById("error").innerHTML = "Incorrect username or password.";
        return;
    }

    handleKeyPress = (event) => {
        if (event.key === 'Enter') {
            this.verifyLogin();
            event.stopPropagation();
            event.preventDefault();
        }
        return;
    }

    render() {
        if (this.state.verified == "Loading") {
            this.fetchVerification();
            return (<div>Loading. Please wait.</div>);
        }
        if (this.state.verified == "VerifiedCertificate") {
            return (<Redirect to='/Management' />);
        }
        return (
            <div id="mainPage" class="container page-container">

                <div class="row justify-content-center">
                    <div class="col-5">
                        <div id="textbox">
                            <div>Username</div>
                            <div class="form-group">
                                <input id="usernameirpa" onKeyDown={this.handleKeyPress} class="form-control" placeholder="username"/>
                            </div>
                        </div>
                        <div id="textbox">
                            <div>Password</div>
                            <div class="form-group">
                                <input id="passwordirpa" onKeyDown={this.handleKeyPress} class="form-control" type="password" placeholder="password"/>
                            </div>
                        </div>
                        <button id="login-button" class="btn btn-primary" type="submit" onClick={this.verifyLogin.bind(this)}>Login</button>
                        <div id="error"></div>
                    </div>
                </div>

                <script>
                    $("body").bootstrapMaterialDesign();
              </script>
            </div>
        );
    }



}