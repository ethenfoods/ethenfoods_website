var gulp = require("gulp"),
    fs = require("fs"),
    less = require("gulp-less");

gulp.task("less", function () {
    return gulp.src('wwwroot/style/scss/*.scss')
        .pipe(less())
        .pipe(gulp.dest('wwwroot/style/css'));
});

// Default Task
gulp.task('default', ['build-css']);