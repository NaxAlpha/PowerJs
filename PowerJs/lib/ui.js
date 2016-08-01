/// File: ui.js
//  Contains simplified API to create simple GUI

function form() {
	return new System.Windows.Forms.Form();
}

function run(frm) {
	System.Windows.Forms.Application.Run(frm);
}