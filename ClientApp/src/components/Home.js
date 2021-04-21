import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { chatList: [],count: 0 };
        this.askButton = this.askButton.bind(this);
        this.renderlog = this.renderlog.bind(this);
        this.chatBotAskQuestion = this.chatBotAskQuestion.bind(this);
    }

    /*id = {
        BOT: 0,
        USER: 1
    }*/

    // Function attacched to ask button
    askButton() {
        let question = document.getElementById("questioninput").value;

        // add question to log
        this.state.chatList.push(new speech(question, 1));
        this.setState({
            chatList: this.state.chatList,
            count: this.state.count + 1
        });

//        // clear question from text input
//        document.getElementById("questioninput").value = "";
//
        // Query chatBot

        let response = this.chatBotAskQuestion(question);
        this.state.chatList.push(new speech(response, 0));
        this.setState({
            chatList: this.state.chatList,
            count: this.state.count + 1
        });

        this.render();
    }

    chatBotAskQuestion(question) {
        if(question == "What does IRPA stand for?")
            return "IRPA stands for Institutional Research Planning and Assessment.";
        else
            return "The number of students enrolled at Rose-Hulman in the year 2020 is 2000."
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
                              <textarea class="form-control" id="questioninput" rows="3"></textarea>
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