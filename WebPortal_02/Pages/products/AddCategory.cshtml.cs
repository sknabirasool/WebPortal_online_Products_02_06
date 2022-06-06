using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using WebPortal_02.Models;

namespace WebPortal_02.Pages.products
{

    
    public class AddCategoryModel : PageModel
    {
        public string userName;
        public string ErrorMessage;
        public Categories cats = new Categories();
        
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

        public void OnPost()
        {
            cats.category_name = Request.Form["category"];
            DateTime today= DateTime.Now;
            cats.created_at = today;

            try
            {
                String con = "Data Source=.;Initial Catalog=Registration_db;Integrated Security=True";

                using (SqlConnection cs = new SqlConnection(con))
                {
                    cs.Open();
                    string sqlquery = "insert into tbl_category(category_name,created_at) values(@category_name,@created_at)";
                  
                       
                    using (SqlCommand command = new SqlCommand(sqlquery, cs))
                    {
                        command.Parameters.AddWithValue("@category_name", cats.category_name);
                        command.Parameters.AddWithValue("@created_at", cats.created_at);
                        command.ExecuteNonQuery();
                        Response.Redirect("../admin/welcome");
                           
                    }
                }
                
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }
        }


    }
}
