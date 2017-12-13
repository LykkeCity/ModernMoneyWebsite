using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ModernMoney.Controllers
{

    [Route("[action]")]
    public class PageController : BaseController
    {
        private UTF8Encoding _Encoding;

        public PageController(IHostingEnvironment envrnmt) : base(envrnmt) {
            _Encoding = new System.Text.UTF8Encoding();
        }

        [HttpGet]
        [ActionName("prepaid-card")]
        public ContentResult DebitCard()
        {
            var page = _GetPage("prepaid-card.html");
            return Content(page);
        }

        [HttpGet]
        [ActionName("vault")]
        public ContentResult Vault()
        {
            var page = _GetPage("vault.html");
            return Content(page);
        }

        [HttpGet]
        [ActionName("feedback")]
        public ContentResult Feedback()
        {
            var page = _GetPage("feedback.html");
            return Content(page);
        }

        [HttpGet]
        [ActionName("how-to")]
        public ContentResult HowTo()
        {
            var page = _GetPage("how-to.html");
            return Content(page);
        }

        [HttpGet]
        [ActionName("pay-bills")]
        public ContentResult PayBills()
        {
            var page = _GetPage("pay-bills.html");
            return Content(page);
        }

        [HttpGet]
        [ActionName("for-partners")]
        public ContentResult ForPartners()
        {
            var page = _GetPage("for-partners.html");
            return Content(page);
        }

        private string _GetPage(string page)
        {
            var path = System.IO.Path.Combine(_Env.WebRootPath, page);
            var htm = System.IO.File.ReadAllText(path, _Encoding);
            HttpContext.Response.Headers.Add("Content-Type", "text/html");
            return htm;
        }
    }
}
