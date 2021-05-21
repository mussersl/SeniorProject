import { Button } from 'bootstrap';
import React, { Component } from 'react';

export class Management extends Component {
    static displayName = Management.name;

    constructor(props) {
        super(props);
        //Call chatbot to populate list
        this.state = {index: 0, count: 0, questions: [], answers: [], edit: -1 };
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
        return function () {
            this.setState({
                edit: -1
            });
            return;
        }
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

    delete(i) {
        return function () {
            this.state.questions.splice(i, 1);
            this.state.answers.splice(i, 1);
            this.setState({
                questions: this.state.questions,
                answers: this.state.answers,
                count: this.state.count - 1,
                edit: -1
            });
            this.render();
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
                                <button class="btn btn-primary editAnswer" onClick={this.submit(i).bind(this)}>Submit</button>
                            </div>
                            <div class="row crudButton">
                                <button class="btn btn-primary deleteAnswer" onClick={this.delete(i).bind(this)}>Delete</button>
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
                                <button class="btn btn-primary editAnswer" onClick={this.edit(i).bind(this)}>Edit</button>
                            </div>
                            <div class="row crudButton">
                                <button class="btn btn-primary deleteAnswer" onClick={this.delete(i).bind(this)}>Delete</button>
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