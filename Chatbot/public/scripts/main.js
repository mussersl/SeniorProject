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
	rhit.chatList.push(new speech("Hi", rhit.id.USER));
	rhit.displayChatLog();
};

rhit.displayChatLog = function () {
	rhit.chatList.forEach(dialogue => {
		if(dialogue.textId == rhit.id.BOT){
			$("#chatlog").append("<div class=\"bot-text\">"+dialogue.text+"</div>");
		} else {
			$("#chatlog").append("<div class=\"user-text\">"+dialogue.text+"</div>");
		}
	});
}


rhit.main();
