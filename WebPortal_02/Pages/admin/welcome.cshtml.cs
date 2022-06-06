using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebPortal_02.Pages.admin
{
    public class welcomeModel : PageModel
    {
        public string userName;
        public void OnGet()
        {
            QueryTest();
        }

        public IActionResult QueryTest()
        {

            if (!String.IsNullOrEmpty(HttpContext.Request.Query["usName"]))
                userName = HttpContext.Request.Query["usName"];

            return Content("Name from query string: " + userName);
        }
    }
}
