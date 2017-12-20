using Microsoft.AspNetCore.Mvc;
using ModernMoney.Models;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Core.Conversation;

namespace ModernMoney.Controllers
{
    [Route("api/contact")]
    public class ContactController : BaseController
    {
        private readonly IConversationRepository _conversationRepository;

        public ContactController(IHostingEnvironment envrnmt,
                              IConversationRepository conversationRepository) : base(envrnmt)
        {
            _conversationRepository = conversationRepository;
        }

        // POST api/contact
        [HttpPost]
        public IActionResult Post([FromForm]ContactModel model)
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

            EmailSender.SendContact(model);

            _conversationRepository.CreateAsync(model.Create(model));

            return Ok(ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.Messages.Contact);
        }

    }
}
