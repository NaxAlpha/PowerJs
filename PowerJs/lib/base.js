/// File: base.js
//  Contains simplified API for handling language primitives

// Execute function async
function async(fx) {
	System.Threading.Tasks.Task.Run(fx);
}

// Exits current running application
function exit() {
	System.Windows.Forms.Application.Exit();
}

// Sleeps for milliseconds
function sleep(milliSeconds) {
	System.Threading.Thread.Sleep(milliSeconds);
}