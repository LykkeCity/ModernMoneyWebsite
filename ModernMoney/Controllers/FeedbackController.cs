using Microsoft.AspNetCore.Mvc;
using ModernMoney.Models;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Core.Conversation;

namespace ModernMoney.Controllers
{
    [Route("api/feedback")]
    public class FeedbackController : BaseController
    {
        private readonly IConversationRepository _conversationRepository;

        public FeedbackController(IHostingEnvironment envrnmt,
                           IConversationRepository conversationRepository) : base(envrnmt)
        {
            _conversationRepository = conversationRepository;
        }

        // POST api/feedback
        [HttpPost]
        public IActionResult Post([FromForm]FeedbackModel model)
        {
            // This fields must not have any value (robots detection). 
            if (!string.IsNullOrEmpty(model.Dummy) || !ModelState.IsValid)
            {
                var errors = ModelState.Where(s => s.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    .ToDictionary(
                    k => k.Key,
                    v => string.Join(" / ", v.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    ));


                return NotFound(errors);
            }

            EmailSender.SendFeedback(model);

            _conversationRepository.CreateAsync(model.Create(model));

            return Ok(ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.Messages.Feedback);
        }

    }
}
