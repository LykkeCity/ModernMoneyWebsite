var app = app || {};

(function BaseView(doc) {

	var BaseView = {
        els: {},
		// name of the view
		name: 'BaseView',

		events: {
			on: function(el, eventName, handler) {
				Events.subscribe(el, eventName, handler);
			},

			off: function(el, eventName, handler) {
				Events.unsubscribe(el, eventName, handler);
			},

			notify: function(el, eventName, params) {
				Events.publish(el, eventName, params);
			}
		},

		//Contains URL data
	    urlData: {

	    	//full URL data
	    	url: function() {
				return doc.URL;
			},

			// return URL host (localhost || domain)
			host: function() {

				var host = '';

				if (window.location.host === 'localhost') {
					host = 'localhost';
				}else{
					host = window.location.host;
				}

				return host;
			},

			// return pathname from url
			pathname: function() {
				return window.location.pathname;
			},

			// return url protocol (http: || https:)
			protocol: function() {
				return window.location.protocol;
			}
	    },

		/***
	     * Add active class (IE 9 support)
	   	 * return void
	     */
		addActive: function(el, elClass) {
		    el.className += ' ' + elClass;   
		},

		/***
	     * Remove active class (IE 9 support - not support classList)
	   	 * return void
	     */
		removeActive: function(el, removeClassName) {
			var elClass = el.className;
		    while(elClass.indexOf(removeClassName) != -1) {
		        elClass = elClass.replace(removeClassName, '');
		        elClass = elClass.trim();
		    }
		    el.className = elClass;
		},

		containsActive: function(el, className) {

			var elClass = el.className.split(' '),
				contains = false;
			
			for(var i = 0; i < elClass.length; i++) {
				if (elClass[i] === className) {	
					contains = true;
				}
			}

			return contains;
		},

        toggleActive: function (el, className) {
          if (this.containsActive(el, className)) {
              this.removeActive(el, className);
          } else {
              this.addActive(el, className);
          }
        },

		capitalizeFirstLetter: function(string) {
    		return string[0].toUpperCase() + string.slice(1);
		},

		getViewport: function() {
			return window.innerWidth;
		}

	};

	app.BaseView = BaseView;
})(document);
