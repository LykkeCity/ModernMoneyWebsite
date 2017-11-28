var app = app || {};

(function NavigationModule(doc) {

    app.Navigation = Object.create(app.BaseView);

    app.Navigation.name = "Navigation";

    /***
     * Start tabs functionality
     * returns void
     */
    app.Navigation.init = function () {
        this.bindEvents();
        this.addActiveOnNavLinks();
    };

    //bind this
    app.Navigation.init = app.Navigation.init.bind(app.Navigation);

    app.Navigation.els.navigation = {
        nav: doc.querySelector('.nav-btn'),
        navLinks: doc.querySelectorAll('.nav-link')
    };

    app.Navigation.bindEvents = function () {

        // console.log('tabs binded');

        if (this.els.navigation.nav) {
            this.events.on(this.els.navigation.nav, 'click', this.initNav);
        }
    };

    app.Navigation.initNav = function () {

        var self = app.Navigation,
            mobileNav = doc.querySelector('.mobile-nav-module'),
            mobileCurtain = doc.querySelector('.curtain');
        // body = doc.querySelector('body');

        // self.addActive(body, 'blur-nav');
        self.addActive(mobileNav, 'active');
        self.addActive(mobileCurtain, 'active');

        self.events.on(mobileCurtain, 'click', self.closeNav);
    };

    app.Navigation.closeNav = function () {

        var self = app.Navigation,
            mobileNav = doc.querySelector('.mobile-nav-module'),
            mobileCurtain = doc.querySelector('.curtain');
        // body = doc.querySelector('body');

        // self.removeActive(body, 'blur-nav');
        self.removeActive(mobileNav, 'active');
        self.removeActive(mobileCurtain, 'active');

        self.events.off(mobileCurtain, 'click', self.initNav);
    };

    app.Navigation.addActiveOnNavLinks = function () {

        var page = this.urlData.url().split('/').pop();
        page = page === '' ? 'index.html' : page;

        for (var i = 0; i < this.els.navigation.navLinks.length; i++) {
            if (this.els.navigation.navLinks[i].href) {
                if (this.els.navigation.navLinks[i].href.split('/').pop() === page) {
                    this.addActive(this.els.navigation.navLinks[i], 'active');
                }
            }
        }
    };

    app.Navigation.init();
})(document);