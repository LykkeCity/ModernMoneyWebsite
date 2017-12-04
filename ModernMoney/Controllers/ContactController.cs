using Microsoft.AspNetCore.Mvc;
using ModernMoney.Models;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

namespace ModernMoney.Controllers
{
    [Route("api/contact")]
    public class ContactController : BaseController
    {
        public ContactController(IHostingEnvironment envrnmt) : base(envrnmt) { }

        // POST api/contact
        [HttpPost]
        public IActionResult Post([FromForm]ContactModel contact)
        {
            // This fields must not have any value (robots detection). 
            if (!string.IsNullOrEmpty(contact.Dummy) || !ModelState.IsValid)
            {
                var errors = ModelState.Where(s => s.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    .ToDictionary(
                    k => k.Key,
                    v => string.Join(" / ", v.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    ));

                return NotFound(errors);
            }

            EmailSender.SendContact(contact);
            AzureStorageHelper.Store(contact);

            return Ok(ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.Messages.Contact);
        }

    }
}
