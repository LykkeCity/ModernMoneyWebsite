var app = app || {};

(function MarketModule($, doc) {

    app.Market = Object.create(app.BaseView);

    app.Market.name = "Market";

    /***
     * Start market tabs functionality
     * returns void
     */
    app.Market.init = function () {
        var tablink = document.querySelector('.tablinks');
        var tabcontent = document.querySelector('.tabcontent');

        if (tablink && tabcontent) {
            app.Market.addActive(tablink, 'active');
            app.Market.addActive(tabcontent, 'active');
        }

        this.bindEvents();
    };

    //bind this
    app.Market.init = app.Market.init.bind(app.Market);

    app.Market.els.tabs = {
        allTabs: doc.querySelectorAll('.tablinks'),
    };

    app.Market.bindEvents = function () {

        if (this.els.tabs.allTabs.length > 0) {
            for (var i = 0; i < this.els.tabs.allTabs.length; i++) {
                this.events.on(this.els.tabs.allTabs[i], 'click', this.initTabs);
            }
        }
    };

    app.Market.initTabs = function () {

        var self = app.Market,
            clickedTabId = this.getAttribute('data-id'),
            clickedTabContent = doc.querySelector('#tab-' + clickedTabId),
            selectedTab = doc.querySelector('.tablinks.active'),
            selectedTabContent = doc.querySelector('.tabcontent.active')
            
        if (!selectedTab) {
            self.addActive(this, 'active');
            self.addActive(clickedTabContent, 'active');
        } else {
            self.removeActive(selectedTab, 'active');
            self.addActive(this, 'active');
            self.removeActive(selectedTabContent, 'active');
            self.addActive(clickedTabContent, 'active');
        }
    };

    app.Market.init();
})(jQuery, document);