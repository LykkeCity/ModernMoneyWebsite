using Microsoft.AspNetCore.Mvc;
using ModernMoney.Models;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Core.Conversation;
using Common.Log;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;
using System;
using System.Threading.Tasks;

namespace ModernMoney.Controllers
{
    [Route("api/feedback")]
    public class FeedbackController : BaseController
    {
        private readonly IConversationRepository _conversationRepository;
        protected readonly ILog _log;

        public FeedbackController(IHostingEnvironment envrnmt,
                           IConversationRepository conversationRepository, ILog log) : base(envrnmt)
        {
            _conversationRepository = conversationRepository;
            _log = log;
        }

        // POST api/feedback
        /// <summary>
        /// Send feedback information.
        /// </summary>
        [SwaggerOperation("FeedBack")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]FeedbackModel model)
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

            try
            {
                EmailSender.SendFeedback(model);
                await _conversationRepository.CreateAsync(model.Create(model));
            }
            catch (Exception ex)
            {
                await _log.WriteInfoAsync(nameof(FeedbackController), nameof(Post), model.Email, ex.ToString(), DateTime.Now);
            }

            return Ok(ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.Messages.Feedback);
        }

    }
}
