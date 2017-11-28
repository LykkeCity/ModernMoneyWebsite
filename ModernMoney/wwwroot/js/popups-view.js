var app = app || {};

(function activateSlider($, doc) {

    app.Popups = Object.create(app.BaseView);

    app.Popups.name = "Popups";

    /***
     * Start popups functionality
     * returns void
     */
    app.Popups.init = function () {
        this.bindEvents();
    };

    app.Tabs.els.popup = {
        singlePopup: doc.querySelectorAll('.single-popup')
    };

    //bind this
    app.Popups.init = app.Popups.init.bind(app.Popups);

    app.Popups.bindEvents = function () {

        // console.log('popups binded');

        // this.initPopups = this.initPopups.bind(this);

        if (this.els.popup.singlePopup.length > 0) {
            for(var i = 0; i < this.els.popup.singlePopup.length; i++) {
                this.events.on(this.els.popup.singlePopup[i], 'click', this.initPopups);
            }
        }
    };

    app.Popups.initPopups = function () {
        var self = app.Popups,
            type = this.getAttribute('data-type'),
            popup = doc.createElement('div'),
            popupCertain = doc.createElement('div');
            // body = doc.querySelector('body');

        popup.className = "popup-main-wrapper";
        popupCertain.className = "popup-certain";
        popup.className += type === 'joinUs' ? ' join' : ' video';
        popup.innerHTML = type === 'joinUs' ? Templates.joinUs: Templates.videoYT;

        doc.body.appendChild(popupCertain);
        doc.body.appendChild(popup);
        // self.addActive(body, 'blur-popup');

        var certain = doc.querySelector('.popup-certain'),
            close = doc.querySelector('.popup-close');

        self.closePopup = self.closePopup.bind(self);
        self.events.on(certain, 'click', self.closePopup);
        self.events.on(close, 'click', self.closePopup);

    };

    app.Popups.closePopup = function () {

        var certain = doc.querySelector('.popup-certain'),
            close = doc.querySelector('.popup-close'),
            popupCertain = doc.querySelector('.popup-certain'),
            popupTemplate = doc.querySelector('.popup-main-wrapper');
            // body = doc.querySelector('body');

        this.events.off(certain, 'click', this.closePopup);
        this.events.off(close, 'click', this.closePopup);

        doc.body.removeChild(popupCertain);
        doc.body.removeChild(popupTemplate);
        // this.removeActive(body, 'blur-popup');
    };

    app.Popups.init();
})(jQuery, document);