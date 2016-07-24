/// File: fio.js
//  Contains File System simplified APIs

// Read File Text
function fread(name) {
	return System.IO.File.ReadAllText(name);
}

// Read File Binary
function freadb(name) {
	return System.IO.File.ReadAllBytes(name);
}

// Write File Text
function fwrite(name, txt) {
	System.IO.File.WriteAllText(name, txt);
}

// Write File Binary
function fwriteb(name, data) {
	System.IO.File.WriteAllBytes(name, data);
}