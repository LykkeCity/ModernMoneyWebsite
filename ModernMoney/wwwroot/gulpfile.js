var gulp  = require('gulp'),
    gutil = require('gulp-util'),

    jshint     = require('gulp-jshint'),
    sass       = require('gulp-sass'),
    concat     = require('gulp-concat'),
    sourcemaps = require('gulp-sourcemaps'),
    browserSync = require('browser-sync').create(),
    del = require('del'),

    input  = {
      'sass': 'scss/**/*.scss',
      'javascript': 'js/**/*.js',
      'jsorder': ['js/vendors/jquery-1.9.1.min.js', 'js/polyfill.js', 'js/events.js', 'js/templates.js', 'js/vendors/toastr.min.js', 'js/base-view.js', 'js/main.js', 'js/tabs-view.js', 'js/popups-view.js', 'js/scrollLibrary-view.js', 'js/navigation-view.js', 'js/market-view.js', 'js/feedback-form.js', , 'js/contact-form.js']

    },

    output = {
      'stylesheets': 'dist/css',
      'javascript': 'dist/js'
    };

/* run the watch task when gulp is called without arguments */
gulp.task('default', ['watch']);

gulp.task('clean', function(cb){
  del(['dist'], cb);
});

// process JS files and return the stream.
gulp.task('js', function () {
    return gulp.src('js/**/*.js')
        .pipe(browserify())
        .pipe(uglify())
        .pipe(gulp.dest(output.javascript));
});

/* run javascript through jshint */
gulp.task('jshint', function() {
  return gulp.src(input.jsorder)
    .pipe(jshint())
    .pipe(jshint.reporter('jshint-stylish'));
});

/* compile scss files */
gulp.task('build-css', function() {
  // return gulp.src('scss/**/*.scss')
  return gulp.src('scss/styles.scss')
    .pipe(sourcemaps.init())
      .pipe(sass())
    .pipe(sourcemaps.write())
    .pipe(gulp.dest(output.stylesheets));
});

/* concat javascript files, minify if --type production */
gulp.task('build-js', function() {
  return gulp.src(input.jsorder)
    .pipe(sourcemaps.init())
      .pipe(concat('bundle.js'))
      //only uglify if gulp is ran with '--type production'
      .pipe(gutil.env.type === 'production' ? uglify() : gutil.noop()) 
    .pipe(sourcemaps.write())
    .pipe(gulp.dest(output.javascript));
});

/* Watch these files for changes and run the task on update */
gulp.task('watch', function() {
  // gulp.watch(input.javascript, ['jshint', 'build-js', 'js-watch']);
  // gulp.watch(input.javascript, ['jshint', 'build-js']);
  gulp.watch(input.javascript, ['build-js']);
  gulp.watch(input.sass, ['build-css']);
});
