import { Button } from 'bootstrap';
import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';

export class Management extends Component {
    static displayName = Management.name;

    constructor(props) {
        super(props);
        this.state = { count: 0, questions: [], answers: [], ids: [], edit: -1, adding: 0, loading: 0, verified: "Loading", got: 0, updateInfo: 0 };
    }

    async fetchVerification() {
        let certificate = sessionStorage.getItem("verificationIRPAChatbot");
        const result = await fetch('ChatBot/VerifyLogin/' + certificate);
        const response = await result.text();
        this.setState({ verified: response });
        return;
    }

    edit(i) {
        return function () {
            this.setState({
                edit: i
            });
            return;
        }
    }

    submit(i) {
        return async function () {
            this.setState({ loading: 1 });
            let question = document.getElementById("questionEdit").value;
            let answer = document.getElementById("answerEdit").value;
            let fetchThing = 'ChatBot/Edit/' + this.state.ids[i] + '/' + question + '/' + answer;
            console.log(fetchThing);
            const result = await fetch('ChatBot/Edit/' + this.state.ids[i] + '/' + encodeURIComponent(question) + '/' + encodeURIComponent(answer) + '/' + sessionStorage.getItem("verificationIRPAChatbot"));
            const response = await result.text();
            this.setState({ loading: 0 });
            this.setState({
                edit: -1,
                adding: 0,
                got: 0
            });
            return;
        }
    }

    addAnswer() {
        if (this.state.adding == 1) {
            return;
        }
        this.state.questions.push("");
        this.state.answers.push("");
        this.setState({
            questions: this.state.questions,
            answers: this.state.answers,
            edit: this.state.count,
            count: this.state.count + 1,
            adding: 1,
            got: 0
        });
    }

    cancel() {
        this.setState({
            edit: -1,
            adding: 0
        });
    }

    delete(i) {
        return async function () {
            this.setState({ loading:1 });
            const result = await fetch('ChatBot/Delete/' + this.state.ids[i] + '/' + sessionStorage.getItem("verificationIRPAChatbot"));
            const response = await result.text();
            this.setState({ loading: 0 });
            //this.state.questions.splice(i, 1);
            //this.state.answers.splice(i, 1);
            this.setState({
                questions: this.state.questions,
                answers: this.state.answers,
                count: this.state.count - 1,
                edit: -1,
                adding: 0,
                got: 0
            });
            return;
        }
    }

    async getanswers() {
        if (this.state.got != 0) {
            return;
        }
        const result = await fetch('ChatBot/GetAll');
        const response = await result.json();
        this.state.questions = [];
        this.state.answers = [];
        this.state.ids = [];
        this.state.count = 0;
        for (let i = 0; i < response.length; i = i + 1) {
            this.state.questions.push(response[i].question);
            this.state.answers.push(response[i].answer);
            this.state.ids.push(response[i].id);
            this.state.count++;
        }
        if (this.state.adding == 1) {
            this.state.count++;
        }
        this.setState({
            questions: this.state.questions,
            answers: this.state.answers,
            count: this.state.count,
            got: 1
        });
    }

    renderAnswers() {
        this.getanswers();
        let uitems = [];
        if (this.state.got == 0) {
            uitems.push(<div>Fetching answers from database</div>);
        }
        let i = this.state.count - 1;
        while (i > -1) {
            if (i == this.state.edit) {
                uitems.push(
                    <div class="row answerEntry" id="testAnswer">
                        <div class="dialog question col-5">
                            <textarea id="questionEdit" class="editBox">{this.state.questions[i]}</textarea>
                        </div>
                        <div class="dialog answer col-5">
                            <textarea id="answerEdit" class="editBox">{this.state.answers[i]}</textarea>
                        </div>
                        <div class="crud col-2">
                            <div class="row crudButton">
                                <button class="btn btn-primary editAnswer" onClick={this.submit(i).bind(this)} disabled={this.state.loading == 1}>Submit</button>
                            </div>
                            <div class="row crudButton">
                                <button class="btn btn-primary deleteAnswer" onClick={this.cancel.bind(this)} disabled={this.state.loading == 1}>Cancel</button>
                            </div>
                            <div class="row crudButton">
                                <button class="btn btn-primary deleteAnswer" onClick={this.delete(i).bind(this)} disabled={this.state.loading == 1}>Delete</button>
                            </div>
                        </div>
                    </div>);
            } else {
                uitems.push(
                    <div class="row answerEntry" id="testAnswer">
                        <div class="dialog question col-5">
                            <div>{this.state.questions[i]}</div>
                        </div>
                        <div class="dialog answer col-5">
                            <div>{this.state.answers[i]}</div>
                        </div>
                        <div class="crud col-2">
                            <div class="row crudButton">
                                <button class="btn btn-primary editAnswer" onClick={this.edit(i).bind(this)} disabled={this.state.loading == 1}>Edit</button>
                            </div>
                        </div>
                    </div>);
            }
            i--;
        }

        return uitems;
    }


    render() {
        if (this.state.verified == "Loading") {
            this.fetchVerification();
            return (<div>Loading. Please wait.</div>);
        }
        if (this.state.verified != "VerifiedCertificate") {
            return (<Redirect to='/Login' />);
        }
        if (this.state.updateInfo == 1) {
            return this.renderUpdateLogin();
        }
      return (
          <div id="mainPage" class="container page-container">
              <div class="row">
                  <div class="col">
                      <button id="addAnswer" class="btn btn-primary" type="submit" onClick={this.addAnswer.bind(this)} disabled={this.state.got == 0}>Add Answer</button>
                  </div>
                  <div class="col">
                      <button id="changeLogin" class="btn btn-primary" type="submit" onClick={this.setUpdate.bind(this)} disabled={this.state.got == 0}>Change Username or password</button>
                  </div>
              </div>
              <div class="row">
                  <div class="col-12">
                      <div id="dataEntries">
                          {this.renderAnswers() }

                      </div>
                  </div>
              </div>

              <script>
                  $("body").bootstrapMaterialDesign();
              </script>
          </div>
        );
    }

    setUpdate() {
        this.setState({ updateInfo: 1 });
        return;
    }

    renderUpdateLogin() {
        return (
            <div id="mainPage" class="container page-container">

                <div class="row justify-content-center">
                    <div class="col-5">
                        <div id="textbox">
                            <div>Username</div>
                            <div class="form-group">
                                <input id="usernameirpa" onKeyDown={this.handleKeyPress} class="form-control" placeholder="username" />
                            </div>
                        </div>
                        <div id="textbox">
                            <div>Password</div>
                            <div class="form-group">
                                <input id="passwordirpa" onKeyDown={this.handleKeyPress} class="form-control" type="password" placeholder="password" />
                            </div>
                        </div>
                        <div id="textbox">
                            <div>New Username (leave blank if you want it to remain unchanged)</div>
                            <div class="form-group">
                                <input id="newusernameirpa" onKeyDown={this.handleKeyPress} class="form-control" placeholder="username" />
                            </div>
                        </div>
                        <div id="textbox">
                            <div>New Password</div>
                            <div class="form-group">
                                <input id="newpasswordirpa" onKeyDown={this.handleKeyPress} class="form-control" type="password" placeholder="password" />
                            </div>
                        </div>
                        <div class="row">
                            <button id="login-button" class="btn btn-primary" type="submit" onClick={this.changeLogin.bind(this)}>Login</button>
                            <button class="btn btn-primary" type="submit" onClick={this.resetUpdate.bind(this)}>Cancel</button>
                        </div>
                        <div id="error"></div>
                    </div>
                </div>

                <script>
                    $("body").bootstrapMaterialDesign();
              </script>
            </div>
        );
    }

    resetUpdate() {
        this.setState({ updateInfo: 0 });
    }

    handleKeyPress = (event) => {
        if (event.key === 'Enter') {
            this.changeLogin();
            event.stopPropagation();
            event.preventDefault();
        }
        return;
    }

    async changeLogin() {
        let username = document.getElementById("usernameirpa").value;
        let password = document.getElementById("passwordirpa").value;
        let newusername = document.getElementById("newusernameirpa").value;
        let newpassword = document.getElementById("newpasswordirpa").value;
        let ct = sessionStorage.getItem("verificationIRPAChatbot");
        if (newusername != null) {
            const result = await fetch('ChatBot/changeUsername/' + newusername + '/' + password + '/'+ ct);
            if (await result.text() != "true") {
                document.getElementById("error").innerHTML = "Incorrect username or password.";
                return;
            }
            console.log(await result.text());
            username = newusername;
        }

        if (newpassword != null) {
            const result = await fetch('ChatBot/changePassword/' + username + '/' + newpassword + '/' + ct);
            if (await result.text() != "true") {
                document.getElementById("error").innerHTML = "Incorrect username or password.";
                return;
            }
        }
        this.setState({ updateInfo: 0 });
    }
}