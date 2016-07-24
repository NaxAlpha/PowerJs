/// File: io.js
//  Contains simplified APIs to handle standard input/output

// Print message
function out(msg) {
	System.Console.WriteLine(msg);
}

// Prompt Input
function inp(msg) {
	if (msg != null)
		System.Console.Write(msg);
	return System.Console.ReadLine();
}

// Waits for key to press
function wait() {
	System.Console.ReadKey(true);
}

