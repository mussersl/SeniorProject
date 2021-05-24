import { error } from 'jquery';
import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { chatList: [], count: 0, ansIdList: [], lastQuestion: ""};
        this.askButton = this.askButton.bind(this);
        this.renderlog = this.renderlog.bind(this);
    }

    async componentDidMount() {
        this.state.chatList.push(new speech("Chatbot Starting Up. Please be patient", 0));
        this.setState({
            chatList: this.state.chatList,
            count: this.state.count + 1
        });

        const result = await fetch('ChatBot/Connect');
        console.log(result);
        const response = await result.text();

        this.state.chatList.splice(0, 1);
        this.state.chatList.push(new speech(response, 0));
        this.setState({
            chatList: this.state.chatList,
            count: this.state.count + 1
        });

        this.render();
    }

    // Function attached to ask button
    async askButton() {
        let question = document.getElementById("questioninput").value.trim();

        // add question to log
        this.state.chatList.push(new speech(question, 1));
        this.setState({
            chatList: this.state.chatList,
            count: this.state.count + 1,
            lastQuestion: question
        });

        if (question.toLowerCase() == "wrong" || question.toLowerCase() == "no") {
            for (let i = 0; i < this.state.ansIdList.length; i++) {
                let increment = await fetch('ChatBot/Decrement/' + this.state.ansIdList[Number(question) - 1] + '/' + this.state.lastQuestion);
                await increment.text();
            }
            this.state.chatList.push(new speech("Thank you for your feedback. Please try restating your question.",0));
            this.setState({
                chatList: this.state.chatList,
                count: this.state.count + 1,
                ansIdList: [],
                lastQuestion: ""
            });
            return;
        }

        if (Number(question) < this.state.ansIdList.length + 1 && Number(question) > 0) {
            for (let i = 0; i < this.state.ansIdList.length; i++) {
                let increment;
                if (i == Number(question) - 1) {
                    increment = await fetch('ChatBot/Increment/' + this.state.ansIdList[Number(question) - 1] + '/' + this.state.lastQuestion);
                } else {
                    increment = await fetch('ChatBot/Decrement/' + this.state.ansIdList[Number(question) - 1] + '/' + this.state.lastQuestion);
                }
                await increment.text();
            }
            this.state.chatList.push(new speech("Thank you for your feedback.",0));
            this.setState({
                chatList: this.state.chatList,
                count: this.state.count + 1,
                ansIdList: [],
                lastQuestion: ""
            });
            return;
        }

        // clear question from text input
        document.getElementById("questioninput").value = "";

        // Query chatBot
        const result = await fetch('ChatBot/Ask/' + question);
        const response = await result.text();

        var responseParsed = response.split(' ');
        this.state.ansIdList = [];
        const index = Number(responseParsed[0]);
        for (let i = 0; i < index; i++) {
            this.state.ansIdList.push(responseParsed[i+1]);
        }

        var returnResponse = "";
        for (let i = index + 1; i < responseParsed.length; i++) {
            if (responseParsed[i] == "\n") {
                if (returnResponse != "" && returnResponse != "\n" && returnResponse != null && returnResponse != " ") {
                    this.state.chatList.push(new speech(returnResponse, 0));
                    this.state.count++;
                    returnResponse = "";
                }
                continue;
            }
            returnResponse += responseParsed[i] + ' ';
        }

        if (returnResponse != "" && returnResponse != "\n" && returnResponse != null && returnResponse != " ") {
            this.state.chatList.push(new speech(returnResponse, 0));
        }
        this.setState({
            chatList: this.state.chatList,
            count: this.state.count + 1,
            ansIdList: this.state.ansIdList
        });
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
                  <div id="bot" class="col-4">
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
                      <button id="ask-button" class="btn btn-primary" type="submit" onClick={this.askButton.bind(this)}>Ask</button>
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