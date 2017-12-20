using Common.Log;
using Core.Conversation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ModernMoney.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ModernMoney.Controllers
{
    [Route("api/beta")]
    public class BetaController : BaseController
    {
        private readonly IConversationRepository _conversationRepository;
        protected readonly ILog _log;

        public BetaController(IHostingEnvironment envrnmt,
                        IConversationRepository conversationRepository, ILog log) : base(envrnmt)
        {
            _conversationRepository = conversationRepository;
            _log = log;
        }

        // POST api/beta
        /// <summary>
        /// Beta registration.
        /// </summary>
        [SwaggerOperation("BetaRegistration")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]BetaModel model)
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
                EmailSender.SendBeta(_Env, model);

                await _conversationRepository.CreateAsync(model.Create(model));
                EmailSender.SendBetaNotification(model);
            }
            catch (Exception ex)
            {
                await _log.WriteInfoAsync(nameof(BetaController), nameof(Post), model.Email, ex.ToString(), DateTime.Now);
            }

            return Ok(ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.Messages.Beta);

        }
    }
}
