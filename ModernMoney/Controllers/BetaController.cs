using Core.Conversation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ModernMoney.Models;
using System.Linq;

namespace ModernMoney.Controllers
{
    [Route("api/beta")]
    public class BetaController : BaseController
    {
        private readonly IConversationRepository _conversationRepository;

        public BetaController(IHostingEnvironment envrnmt,
                        IConversationRepository conversationRepository) : base(envrnmt)
        {
            _conversationRepository = conversationRepository;
        }

        // POST api/beta
        [HttpPost]
        public IActionResult Post([FromForm]BetaModel model)
        {
            // This fields must not have any value (robots detection). 
            if (!string.IsNullOrEmpty(model.Dummy) || !ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return NotFound(errors);
            }

            EmailSender.SendBeta(_Env, model);

            _conversationRepository.CreateAsync(model.Create(model));

            EmailSender.SendBetaNotification(model);

            return Ok(ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.Messages.Beta);
        }
    }
}
