var fs = require('fs');
var path = require('path');
var child_process = require('child_process');
scan('Xi', 2);
function scan(dir, type) {
	var files = fs.readdirSync(dir);
	files.forEach(function(f) {
		if (path.extname(f) === '.jpg')
			child_process.spawn('..\\ip\\bin\\Debug\\ip.exe', [path.join(dir, f), type]);
	});
}