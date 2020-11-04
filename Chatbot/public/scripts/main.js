var rhit = rhit || {};

/** globals */
rhit.chatList = [];

rhit.id = {
	BOT: 0,
	USER: 1
}

class speech {
	text;
	textId;

	constructor(string, identity) {
		this.text = string;
		this.textId = identity;
	}
}

/* Main */
rhit.main = function () {
	console.log("Ready");
	rhit.chatList.push(new speech("Hello", rhit.id.BOT));
	rhit.displayFullChatLog();
	$("#ask-button").click(rhit.askButton);
	$("#questioninput").keyup(rhit.enterInput);
};


// prints entire chat log into text box
rhit.displayFullChatLog = function () {
	rhit.chatList.forEach(dialogue => {
		if(dialogue.textId == rhit.id.BOT){
			$("#chatlog").append("<div class=\"bot-text\">"+dialogue.text+"</div>");
		} else {
			$("#chatlog").append("<div class=\"user-text\">"+dialogue.text+"</div>");
		}
	});
}

// display last dialogue in the chatList
rhit.displayLast = function () {
	let last = rhit.chatList[rhit.chatList.length - 1];

	// Assigns class text for display purposes
	if(last.textId == rhit.id.BOT){
		$("#chatlog").append("<div class=\"bot-text\">"+last.text+"</div>");
	} else {
		$("#chatlog").append("<div class=\"user-text\">"+last.text+"</div>");
	}
}

// Recognizes enter key press
rhit.enterInput = function (e) {
	if(e.which == 13){
		rhit.askButton();
	}
}


// Function attacched to ask button
rhit.askButton = function() {
	let question = document.getElementById("questioninput").value;

	// add question to log
	rhit.chatList.push(new speech(question, rhit.id.USER));
	rhit.displayLast();

	// clear question from text input
	document.getElementById("questioninput").value = "";

	// Query chatBot
	let response = rhit.chatBotAskQuestion(question);

	// add response to log
	rhit.chatList.push(new speech(response, rhit.id.BOT));
	rhit.displayLast();
}

// Calls chatbot code and returns chatbot response
rhit.chatBotAskQuestion = function (question) {
	return "Question recieved: " + question;
}

rhit.main();
