import { Button } from 'bootstrap';
import React, { Component } from 'react';

export class Management extends Component {
    static displayName = Management.name;

    constructor(props) {
        super(props);
        this.state = { count: 0 };
    }

  render () {
      return (
          <div id="mainPage" class="container page-container">
              <div class="row">
                  <div class="col">
                        <button id="addAnswer" class="btn btn-primary" type="submit">Add Answer</button>
                  </div>
              </div>
              <div class="row">
                  <div class="col-12">
                      <div id="dataEntries">
                          <div class="row answerEntry" id="testAnswer">
                              <div class="dialog answer col-5">
                                  <div>Hello</div>
                              </div>
                              <div class="dialog question col-5">
                                  <div>Hello</div>
                              </div>
                              <div class="crud col-2">
                                  <div class="row crudButton">
                                      <button class="btn btn-primary editAnswer">Edit</button>
                                  </div>
                                  <div class="row crudButton">
                                      <button class="btn btn-primary deleteAnswer">Delete</button>
                                  </div>
                              </div>
                          </div>
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