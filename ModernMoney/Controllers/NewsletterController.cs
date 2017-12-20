using Microsoft.AspNetCore.Mvc;
using ModernMoney.Models;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Core.Conversation;

namespace ModernMoney.Controllers
{
    [Route("api/newsletter")]
    public class NewsletterController : BaseController
    {
        private readonly IConversationRepository _conversationRepository;

        public NewsletterController(IHostingEnvironment envrnmt,
                        IConversationRepository conversationRepository) : base(envrnmt)
        {
            _conversationRepository = conversationRepository;
        }

        // POST api/newsletter
        [HttpPost]
        public IActionResult Post([FromForm]NewsletterModel model)
        {
            // This fields must not have any value (robots detection). 
            if (!string.IsNullOrEmpty(model.Dummy) || !ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return NotFound(errors);
            }

            _conversationRepository.CreateAsync(model.Create(model));

            return Ok(ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.Messages.Newsletter);
        }
    }
}
