import { error } from 'jquery';
import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { chatList: [], count: 0, dumbLearning: 0, askFunc: function (x) { return "F" } };
        this.askButton = this.askButton.bind(this);
        this.renderlog = this.renderlog.bind(this);
        this.chatBotAskQuestion = this.chatBotAskQuestion.bind(this);
    }

    async componentDidMount() {
        this.state.chatList.push(new speech("Chatbot Starting Up. Please be patient", 0));
        this.setState({
            chatList: this.state.chatList,
            count: this.state.count + 1
        });

        const result = await fetch('AskFunc');
        this.state.chatList.push(new speech(String(result.ok), 0));
        this.setState({
            chatList: this.state.chatList,
            count: this.state.count + 1
        });

        const ask = await result.json();
        this.state.setState({ askFunc: ask.ask });

        this.state.chatList.push(new speech("Chatbot Online.", 0));
        this.setState({
            chatList: this.state.chatList,
            count: this.state.count + 1
        });

        this.render();
    }

    // Function attached to ask button
    askButton() {
        let question = document.getElementById("questioninput").value;

        // add question to log
        this.state.chatList.push(new speech(question, 1));
        this.setState({
            chatList: this.state.chatList,
            count: this.state.count + 1
        });

        // clear question from text input
        document.getElementById("questioninput").value = "";

        // Query chatBot

        let response = this.state.askFunc(question);
        //let response = this.chatBotAskQuestion(question);
        this.state.chatList.push(new speech(response, 0));
        this.setState({
            chatList: this.state.chatList,
            count: this.state.count + 1
        });

        this.render();
    }

    chatBotAskQuestion(question) {
        if (question == "What does IRPA stand for?")
            return "IRPA stands for Institutional Research Planning and Assessment.";
        else if (question.includes("enroll"))
            return "The number of students enrolled at Rose-Hulman in the year 2020 is 2038.";
        else if (question.includes("graduate") && this.state.dumbLearning == 0)
            return "Undergraduates that have fewer than 12 credit hours per quarter or fewer than 24 contact hours per quarter are considered part time.";
        else if (question.includes("graduate") && this.state.dumbLearning == 1)
            return "Graduates that are enrolled for fewer than 9 credit hours per quarter are considered part time.";
        else if (question.includes("wrong")) {
            this.state.dumbLearning = 1;
            return "Thank you for your feedback. Is this what you are looking for?\n\nGraduates that are enrolled for fewer than 9 credit hours per quarter are considered part time.";
        }
        else
            return "My apologies, I don't understand the question.";
    }

    renderlog() {
        let uitems = [];
        let index = 0;
        let current = this.state.chatList[index];
        while (index < this.state.chatList.length) {
            current = this.state.chatList[index];
            if (current.textId == 0) {
                uitems.push(
                    <div class="bot-text">
                        {current.text}
                    </div>);
            } else {
                uitems.push(
                    <div class="user-text">
                        {current.text}
                    </div>);
            }
            index++;
        }
        return uitems;
    }

    handleKeyPress = (event) => {
        if (event.key === 'Enter') {
            this.askButton()
            event.stopPropagation();
            event.preventDefault();
        }
        return;
    }

  render () {
      return (
          <div id="mainPage" class="container page-container">

              <div class="row justify-content-center">
                  <div class="col-4">
                      <div id="chatlog-window">
                          <div class="row">
                              <div id="chatlog" class="col">
                                  {this.renderlog()}
                              </div>
                          </div>
                      </div>
                      <div id="textbox">
                          <div class="form-group">
                              <textarea onKeyDown={this.handleKeyPress} class="form-control" id="questioninput" rows="3"></textarea>
                          </div>
                      </div>
                      <button id="ask-button" class="btn btn-primary" type="submit" onClick={this.askButton}>Ask</button>
                  </div>
              </div>

              <script>
                  $("body").bootstrapMaterialDesign();
              </script>
          </div>
    );
  }
}

class speech {
    text;
    textId;

    constructor(string, identity) {
        this.text = string;
        this.textId = identity;
    }
}