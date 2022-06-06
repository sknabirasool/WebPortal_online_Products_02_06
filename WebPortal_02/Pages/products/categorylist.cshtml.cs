using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using WebPortal_02.Models;

namespace WebPortal_02.Pages.products
{
    public class categorylistModel : PageModel
    {
        public string userName;
        public string ErrorMessage;
        public Categories cats = new Categories();

        public List<Categories> catlist = new List<Categories>();
        public void OnGet()
        {
            QueryTest();

            try
            {
                String con = "Data Source=.;Initial Catalog=Registration_db;Integrated Security=True";

                using (SqlConnection cs = new SqlConnection(con))
                {
                    cs.Open();
                    string sqlquery = "Select * from tbl_category";
                    using (SqlCommand command = new SqlCommand(sqlquery, cs))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                Categories c = new Categories();
                                c.id = reader.GetInt32(0);
                                c.category_name= reader.GetString(1);
                                c.created_at= reader.GetDateTime(2);
                                catlist.Add(c);

                            }
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }
        }

        public IActionResult QueryTest()
        {

            if (!String.IsNullOrEmpty(HttpContext.Request.Query["usName"]))
                userName = HttpContext.Request.Query["usName"];

            return Content("Name from query string: " + userName);
        }

    }
}
