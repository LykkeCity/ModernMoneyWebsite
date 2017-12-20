using Microsoft.AspNetCore.Mvc;
using ModernMoney.Models;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Core.Conversation;
using Common.Log;
using System;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;

namespace ModernMoney.Controllers
{
    [Route("api/newsletter")]
    public class NewsletterController : BaseController
    {
        private readonly IConversationRepository _conversationRepository;
        protected readonly ILog _log;

        public NewsletterController(IHostingEnvironment envrnmt,
                        IConversationRepository conversationRepository, ILog log) : base(envrnmt)
        {
            _conversationRepository = conversationRepository;
            _log = log;
        }

        // POST api/newsletter
        /// <summary>
        /// Newsletter subscription.
        /// </summary>
        [SwaggerOperation("Newsletter")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]NewsletterModel model)
        {
            // This fields must not have any value (robots detection). 
            if (!string.IsNullOrEmpty(model.Dummy) || !ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return NotFound(errors);
            }

            try
            {
                await _conversationRepository.CreateAsync(model.Create(model));
            }
            catch (Exception ex)
            {
                await _log.WriteInfoAsync(nameof(NewsletterController), nameof(Post), model.Email, ex.ToString(), DateTime.Now);
            }

            return Ok(ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.Messages.Newsletter);
        }
    }
}
