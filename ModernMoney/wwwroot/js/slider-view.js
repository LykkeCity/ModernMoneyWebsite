var app = app || {};

(function activateSlider($, doc) {

	app.Slider = Object.create(app.BaseView);

	app.Slider.name = "Slider";

	/***
     * Start scroll functionality
     * returns void
     */
	app.Slider.init = function () {
		this.bindEvents();
		if (doc.querySelector('.carousel-init')) {
            this.events.notify(doc, 'activate:slider');
        }
	};


	//bind this
	app.Slider.init = app.Slider.init.bind(app.Slider);

	app.Slider.bindEvents = function () {

		// console.log('slider binded');
		this.sliderControls = this.sliderControls.bind(this);
	    this.events.on(doc, 'activate:slider', this.sliderControls);


	};

	app.Slider.sliderControls = function() {

		 var slide = $('.carousel-init');
		 var isApp = slide.attr('data-is-app');

	    slide.owlCarousel({
			items : 1,
			singleItem: true,
			lazyLoad : true,
			navigation : isApp ? true : false,
			navigationText: ['<button class="btn-md btn-blue-bg btn-circle-md" type="button"><i class="icon-asset-73"></i></button>', '<button class="btn-md btn-blue-bg btn-circle-md" type="button"><i class="icon-asset-73"></i></button>'],
			autoPlay: true,
			stopOnHover: true,
            paginationNumbers: isApp ? false : true
	    });

        // var paginationBtns = doc.querySelectorAll('.owl-page span');
        //
        // for(var i = 0; i < paginationBtns.length; i++) {
	     //    paginationBtns[i].innerHTML = i + 1;
        // }
	};

	app.Slider.init();
})(jQuery, document);