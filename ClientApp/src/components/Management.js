import { Button } from 'bootstrap';
import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';

export class Management extends Component {
    static displayName = Management.name;

    constructor(props) {
        super(props);
        this.state = { count: 0, questions: [], answers: [], ids: [], edit: -1, adding: 0, loading: 0, verified: "Loading" };
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
            const result = await fetch('ChatBot/Edit/' + this.state.ids[i] + '/' + question + '/' + answer);
            const response = await result.text();
            this.setState({ loading: 0 });
            this.setState({
                edit: -1,
                adding: 0
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
            adding: 1
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
            const result = await fetch('ChatBot/Delete/' + this.state.ids[i]);
            const response = await result.text();
            this.setState({ loading: 0 });
            //this.state.questions.splice(i, 1);
            //this.state.answers.splice(i, 1);
            this.setState({
                questions: this.state.questions,
                answers: this.state.answers,
                count: this.state.count - 1,
                edit: -1,
                adding: 0
            });
            return;
        }
    }

    async getanswers() {
        const result = await fetch('ChatBot/GetAll');
        const response = await result.json();
        this.state.questions = [];
        this.state.answers = [];
        this.state.count = 0;
        for (let i = 0; i < response.length; i = i + 1) {
            this.state.questions.push(response[i].question);
            this.state.answers.push(response[i].answer);
            //this.state.ids.push(response[i].ID);
            this.state.count++;
        }
        if (this.state.adding == 1) {
            this.state.count++;
        }
        this.setState({
            questions: this.state.questions,
            answers: this.state.answers,
            count: this.state.count
        });
    }

    renderAnswers() {
        this.getanswers();
        let uitems = []
        let i = 0;
        while (i < this.state.count) {
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
            i++;
        }

        return uitems;
    }


    render() {
        this.fetchVerification();
        if (this.state.verified == "Loading") {
            return (<div>Loading. Please wait.</div>);
        }
        if (this.state.verified != "VerifiedCertificate") {
            return (<Redirect to='/Login' />);
        }
      return (
          <div id="mainPage" class="container page-container">
              <div class="row">
                  <div class="col">
                      <button id="addAnswer" class="btn btn-primary" type="submit" onClick={this.addAnswer.bind(this)}>Add Answer</button>
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
}