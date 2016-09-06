var fs = require('fs');
var path = require('path');
var child_process = require('child_process');
scan('SimSun');
function scan(dir, type) {
	var files = fs.readdirSync(dir);
	files.forEach(function(f) {
		if (path.extname(f) === '.jpg') {
			runip([path.join(dir, f), 1]);
			runip([path.join(dir, f), 2]);
			runip([path.join(dir, f), 3]);
			runip([path.join(dir, f), 4]);
		}
	});
}
function runip(a) {
	child_process.spawn('..\\ip\\bin\\Debug\\ip.exe', a);
}