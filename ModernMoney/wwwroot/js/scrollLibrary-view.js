var app = app || {};

(function ScrollLibraryModule(doc, global) {

	app.ScrollLibrary = Object.create(app.BaseView);

	app.ScrollLibrary.name = "ScrollLibrary";


	/***
     * Start scroll functionality
     * returns void
     */
	app.ScrollLibrary.init = function () {
		this.cacheElements();
		this.bindEvents();
		// console.log(this.viewport);
	};

    // console.log(app.ScrollLibrary);

    //bind this
    app.ScrollLibrary.init = app.ScrollLibrary.init.bind(app.ScrollLibrary);


    app.ScrollLibrary.els.section = doc.querySelectorAll('[js-animation]');

	app.ScrollLibrary.viewport = {
			windowHeight: global.innerHeight
	};
	app.ScrollLibrary.previousScroll = 0;
	app.ScrollLibrary.position = 'down';
	app.ScrollLibrary.lastIndex = 0;
	app.ScrollLibrary.counter = 1;
	app.ScrollLibrary.obj = {};

	app.ScrollLibrary.bindEvents = function () {
		// console.log('ScrollLibrary binded');
		global.addEventListener('scroll', this.scrollFunc);
	};

	app.ScrollLibrary.cacheElements = function() {

		this.viewportWidth = this.getViewport();

		// console.log(this.viewport);
		for(var i = 0; i < this.els.section.length; i++) {

			this.obj[i] = {
				el: this.els.section[i],
				offsetTop: this.els.section[i].offsetTop,
				offsetHeight: this.els.section[i].offsetTop + this.els.section[i].offsetHeight,
				height: this.els.section[i].offsetHeight,
				animationEls: this.els.section[i].querySelectorAll('[data-type]')
			};
		}
		// console.log(this.obj[1].animationEls);
	};


	app.ScrollLibrary.scrollFunc = function() {
		var self = app.ScrollLibrary,
			visible = self.viewport.windowHeight / 2, //2,
			windowTopPosition = global.pageYOffset,
			windowBottomPosition = windowTopPosition + self.viewport.windowHeight;

		self.addActiveOnHeader();
		if (app.Scroll !== undefined) {
			app.Scroll.addActiveOnSubNavOnScroll();
		}
		//loop through animated parent elements
		for(var index in self.obj) {
			
			//get each parent element data
		    var element = self.obj[index].el,
		    	childElements = self.obj[index].animationEls,
		    	parallax = self.obj[index].parallax,
		    	elementHeight = element.offsetHeight,
		    	elementTopPosition = element.offsetTop,
		    	visibleElHight = self.obj[index].height / 2,
		    	elementBottomPosition = (elementTopPosition + elementHeight);

		    //check to see if this current container is within viewport
		    if ((elementBottomPosition >= windowTopPosition + visibleElHight) && (elementTopPosition + visible <= windowBottomPosition)) {
		    	// console.log('in viewport', elementBottomPosition, windowTopPosition, visibleElHight, elementTopPosition, visible, windowBottomPosition);
		    	//add each parent in-view class when is in viewport
		    	element.classList.add('in-view');

				// console.log(self.viewportWidth);
		    	
		    	//check for child elements if not empty run their animations
		    	if (childElements.length > 0) {
		    		self.checkChildElements(childElements);
		    	}

		    // start animation from bottom
		    } else {
		    	if (element.hasAttribute('both-side-animation')) {
		    		self.bothSideAnimation(element, childElements);
		    	}
		    }
		}
	};

	app.ScrollLibrary.checkChildElements = function(childElements) {

		//get child elements
    	if (childElements.length > 0 && childElements !== 'undefined') {

    		//loop through child elemnts to start animation
    		for(var j = 0; j < childElements.length; j++) {

    			if (childElements[j].hasAttribute('one-by-one')) {
    				this.oneByOne(childElements[j], childElements[j].getAttribute('one-by-one'));

    			} else {
    				// console.log('in else');
    				childElements[j].classList.add(childElements[j].getAttribute('data-type'));
    			}
    		}

    	}
	};

	app.ScrollLibrary.oneByOne = function(el, time) {

		var elClass = el.getAttribute('data-type');

		var x = setTimeout(function() {
			// console.log(el);
			el.classList.add(elClass);
		}, 1000 * time);
	};

	app.ScrollLibrary.bothSideAnimation = function(parent, childElements) {

		parent.classList.remove('in-view');

		if (childElements.length >= 1 && childElements !== 'undefined') {
    		for(var j = 0; j < childElements.length; j++) {
    			childElements[j].classList.remove(childElements[j].getAttribute('data-type'));
    		}
    	}
	};

	app.ScrollLibrary.scrollPosition = function() {
		var self = app.ScrollLibrary,
			currentScroll = doc.body.scrollTop;

		if (currentScroll > self.previousScroll){
	   		// console.log('down');
	   		self.position = 'down';
		} else {
			// console.log('up');
			self.position = 'up';
		}

		if ((global.innerHeight + global.scrollY) >= doc.body.offsetHeight) {
			// console.log("bottom");
	   		self.position = 'bottom';
		}

		self.previousScroll = currentScroll;
	};

	app.ScrollLibrary.addActiveOnHeader = function() {

		var header = doc.querySelector('.header'),
			yOffset = window.pageYOffset;
			// console.log(yOffset);
			var contains = this.containsActive(header, 'active');

		if (!contains && yOffset > 1000) {
			this.addActive(header, 'active');
		} else if (contains && yOffset === 0) {
			this.removeActive(header, 'active');
		}
	};
    app.ScrollLibrary.init();
})(document, window);

// console.log(app);