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
/** function and class syntax examples */
rhit.main = function () {
	console.log("Ready");
	rhit.chatList.push(new speech("Hello", rhit.id.BOT));
	rhit.displayFullChatLog();
	$("#ask-button").click(rhit.askButton);
};

rhit.displayFullChatLog = function () {
	rhit.chatList.forEach(dialogue => {
		if(dialogue.textId == rhit.id.BOT){
			$("#chatlog").append("<div class=\"bot-text\">"+dialogue.text+"</div>");
		} else {
			$("#chatlog").append("<div class=\"user-text\">"+dialogue.text+"</div>");
		}
	});
}

rhit.displayLast = function () {
	let last = rhit.chatList[rhit.chatList.length - 1];
	if(last.textId == rhit.id.BOT){
		$("#chatlog").append("<div class=\"bot-text\">"+last.text+"</div>");
	} else {
		$("#chatlog").append("<div class=\"user-text\">"+last.text+"</div>");
	}
}

rhit.askButton = function() {
	let question = document.getElementById("questioninput").value;
	rhit.chatList.push(new speech(question, rhit.id.USER));
	rhit.displayLast();
}


rhit.main();
