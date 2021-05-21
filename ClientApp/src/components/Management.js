import { Button } from 'bootstrap';
import React, { Component } from 'react';

export class Management extends Component {
    static displayName = Management.name;

    constructor(props) {
        super(props);
        //Call chatbot to populate list
        this.state = {index: 0, count: 0, questions: [], answers: [], edit: -1 };
        //this.state.questions.push("How many students are enrolled?");
        //this.state.answers.push("The number of students enrolled at Rose-Hulman in the year 2020 is 2038.");
        //this.state.count++;
        //this.state.questions.push("What does IRPA stand for?");
        //this.state.answers.push("IRPA stands for Institutional Research Planning and Assessment.");
        //this.state.count++;
        //this.state.questions.push("What qualifies an undergraduate for part time status?");
        //this.state.answers.push("Undergraduates that have fewer than 12 credit hours per quarter or fewer than 24 contact hours per quarter are considered part time.");
        //this.state.count++;
        //this.state.questions.push("What qualifies a graduate for part time status?");
        //this.state.answers.push("Graduates that are enrolled for fewer than 9 credit hours per quarter are considered part time.");
        //this.state.count++;
    }

    edit(index) {
        this.setState({
            edit: 0
        });
        return;
    }

    submit() {
        return;
    }

    addAnswer() {
        this.state.questions.push("");
        this.state.answers.push("");
        this.setState({
            questions: this.state.questions,
            answers: this.state.answers,
            edit: this.state.count,
            count: this.state.count + 1,
        });
    }

    delete() {
        this.state.questions.splice(0, 1);
        this.state.answers.splice(0, 1);
        this.setState({
            questions: this.state.questions,
            answers: this.state.answers,
            count: this.state.count - 1,
            edit: -1
        });
        this.render();
        return;
    }

    async getanswers() {
        const result = await fetch('ChatBot/GetAll');
        const response = await result.json();
        this.state.questions = [];
        this.state.answers = [];
        for (let i = 0; i < response.length; i = i + 1) {
            if (response[i].question == null) {
                break;
            }
            this.state.questions.push(response[i].question);
            this.state.answers.push(response[i].answer);
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
                            <textarea>{this.state.questions[i]}</textarea>
                        </div>
                        <div class="dialog answer col-5">
                            <textarea>{this.state.answers[i]}</textarea>
                        </div>
                        <div class="crud col-2">
                            <div class="row crudButton">
                                <button class="btn btn-primary editAnswer" onClick={this.submit.bind(this)}>Submit</button>
                            </div>
                            <div class="row crudButton">
                                <button class="btn btn-primary deleteAnswer" onClick={this.delete.bind(this)}>Delete</button>
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
                                <button class="btn btn-primary editAnswer" onClick={this.edit.bind(this)}>Edit</button>
                            </div>
                            <div class="row crudButton">
                                <button class="btn btn-primary deleteAnswer" onClick={this.delete.bind(this)}>Delete</button>
                            </div>
                        </div>
                    </div>);
            }
            i++;
        }

        return uitems;
    }


  render () {
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