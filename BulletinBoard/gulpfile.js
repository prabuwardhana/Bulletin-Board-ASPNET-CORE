var gulp = require("gulp");
var npmDist = require("gulp-npm-dist");

// Copy dependencies to wwwroot/lib/
gulp.task("copynpm", function (done) {
	gulp.src(npmDist(), { base: "./node_modules" }).pipe(gulp.dest("./wwwroot/lib"));
	done();
});
