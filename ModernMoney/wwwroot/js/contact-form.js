var app = app || {};

(function ContactForm($, doc) {

    var ContactForm = {

        submit: function (theform) {
            if (grecaptcha.getResponse() == "") {
                return false;
            }

            var contactForm = $(theform).parents('form');
            var errorLabel = contactForm.find('.error');
            var action = contactForm.attr('action');

            $.ajax({
                type: "POST",
                url: action,
                data: contactForm.serialize(),
                success: function (response) {

                    var content = $(theform).closest('.content');
                    content.animate({
                        opacity: 0
                    }, 500, function () {
                        content.hide();
                        content.prev('.title').css('background', '#20d330');
                        content.next('.success').fadeIn('slow');
                    });

                    toastr['success'](response);
                    errorLabel.html('');
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    var errors = JSON.parse(XMLHttpRequest.responseText);
                    $('#full-name-error').html(errors['FullName']);
                    $('#email-error').html(errors['Email']);
                    $('#phone-number-error').html(errors['PhoneNumber']);
                }
            });
        },

        isValid: function (theInput) {

            var contactForm = $(theInput).parents('form');
            var errorLabel = $(theInput).parent().next('.error');


            if (!theInput.checkValidity()) {
                switch ($(theInput).attr('id')) {
                    case 'FullName':
                        errorLabel.html('Name is required.');
                        break;

                    case 'Email':
                        errorLabel.html('Please enter correct email address.');
                        break;

                    case 'PhoneNumber':
                        errorLabel.html('Please enter correct phone number.');
                        break;

                    default:
                        break;
                }

            } else {
                errorLabel.html('');
            }
        }

    };

    app.ContactForm = ContactForm;
})(jQuery, document);