using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using WebPortal_02.Models;

namespace WebPortal_02.Pages
{

    

    public class IndexModel : PageModel
    {
        

        public string userName;
        public string ErrorMessage;

        public List<Categories> catlist= new List<Categories>();

        public List<Products> pl = new List<Products>();

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            QueryTest();
            try
            {
                String con = "Data Source=.;Initial Catalog=Registration_db;Integrated Security=True";

                using (SqlConnection cs = new SqlConnection(con))
                {
                    cs.Open();
                    string sqlquery = "Select * from tbl_products";
                    using (SqlCommand command = new SqlCommand(sqlquery, cs))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Products c = new Products();
                                c.id = reader.GetInt32(0);
                                c.product_name = reader.GetString(2);
                                c.description = reader.GetString(3);
                                c.price = reader.GetInt32(4);
                                c.created_at = reader.GetDateTime(5);
                                pl.Add(c);

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