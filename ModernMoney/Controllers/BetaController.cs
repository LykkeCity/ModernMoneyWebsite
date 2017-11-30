using Microsoft.AspNetCore.Mvc;
using ModernMoney.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

namespace ModernMoney.Controllers
{
    [Route("api/[controller]")]
    public class BetaController : BaseController
    {

        public BetaController(IHostingEnvironment envrnmt) :base(envrnmt) {}

        // POST api/newsletter
        [HttpPost]
        public IActionResult Post([FromForm]BetaModel beta)
        {
            // This fields must not have any value (robots detection). 
            if (!string.IsNullOrEmpty(beta.Dummy) || !ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return NotFound(errors);
            }

            EmailSender.SendBeta(_Env, beta);
            AzureStorageHelper.Store(beta);
     
            return Ok(ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.Messages.Beta);
        }
    }
}
