var app = app || {};

(function FeedbackForm($, doc) {

    var FeedbackForm = {

        submit: function (theform) {
            var feedbackForm = $(theform).parents('form');
            var errorLabel = feedbackForm.find('.error');
            var action = feedbackForm.attr('action');
            theform.disabled = true;

            $.ajax({
                type: "POST",
                url: action,
                data: feedbackForm.serialize(),
                success: function (response) {
                    if (action == '/api/beta') {
                        // Join Beta
                        var content = $(theform).closest('.content');
                        content.animate({
                            opacity: 0
                        }, 500, function () {
                            content.hide();
                            content.prev('.title').css('background', '#20d330');
                            content.next('.success').fadeIn('slow');
                        });
                    } else if (action == '/api/feedback') {
                        // Feedback
                        var content = feedbackForm;
                        content.animate({
                            opacity: 0
                        }, 500, function () {
                            content.hide();
                            content.next('.success').fadeIn('slow');
                        });
                    } else {
                        // Newsletter
                        toastr['success'](response);
                    }

                    errorLabel.html('');
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    if (action == '/api/feedback') {
                        var errors = JSON.parse(XMLHttpRequest.responseText);
                        $('#first-name-error').html(errors['FirstName']);
                        $('#last-name-error').html(errors['LastName']);
                        $('#email-error').html(errors['Email']);
                    } else {
                        errorLabel.html(XMLHttpRequest.responseText);
                    }
                }
            });
        }
    };

    app.FeedbackForm = FeedbackForm;
})(jQuery, document);
