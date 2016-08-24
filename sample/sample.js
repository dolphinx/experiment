var fs = require('fs');
var path = require('path');
var child_process = require('child_process');
scan('SimSun');
scan('SimHei');
function scan(dir) {
	var files = fs.readdirSync(dir);
	files.forEach(function(f) {
		if (path.extname(f) === '.jpg')
			child_process.spawn('..\\ip\\bin\\Debug\\ip.exe', [path.join(dir, f)]);
	});
}