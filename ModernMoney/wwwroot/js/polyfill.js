(function Polyfill(){

	var isModernIE = (navigator.userAgent.indexOf('MSIE') !== -1 || navigator.appVersion.indexOf('Trident/') > 0) ? true : false;

	/***
	 *	Custom function for new CustomEvent() method
	 */
	function ACustomEvent ( event, params ) {
	    params = params || { bubbles: false, cancelable: false, detail: undefined };
	    var evt = document.createEvent( 'CustomEvent' );
	    evt.initCustomEvent( event, params.bubbles, params.cancelable, params.detail );
	    return evt;
	}

	/***
	 * Internet Exploder 9+ has a CustomEvent object
	 * unfortunately it has no constructor and no means to use it
	 */
	if(isModernIE) {

	    window.CustomEvent = ACustomEvent;
	}else {

	    window.CustomEvent = window.CustomEvent || ACustomEvent;
	}
})();