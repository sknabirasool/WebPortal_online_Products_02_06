using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using WebPortal_02.Models;

namespace WebPortal_02.Pages.products
{
    public class AddProductsModel : PageModel
    {
        public string userName;
        public string ErrorMessage;

        public Products pr = new Products();

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
                            while (reader.Read())
                            {
                                Categories c = new Categories();
                                c.id = reader.GetInt32(0);
                                c.category_name = reader.GetString(1);
                                c.created_at = reader.GetDateTime(2);
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



        public void OnPost()
        {
            pr.product_name = Request.Form["product_name"];

            string[] arg = new string[2];
            arg = pr.product_name.ToString().Split(',');
            pr.category_id = Convert.ToInt32(arg[0]);
            pr.product_name= arg[1];

            pr.description = Request.Form["product_desc"];
            pr.price = Convert.ToInt32(Request.Form["price"]);

            DateTime today = DateTime.Now;
            pr.created_at= today;


            try
            {
                String con = "Data Source=.;Initial Catalog=Registration_db;Integrated Security=True";

                using (SqlConnection cs = new SqlConnection(con))
                {
                    cs.Open();
                    string sqlquery = "insert into tbl_products(category_id,product_name,description,price,created_at) values(@category_id,@product_name,@description,@price,@created_at)";


                    using (SqlCommand command = new SqlCommand(sqlquery, cs))
                    {
                        command.Parameters.AddWithValue("@category_id", pr.category_id);
                        command.Parameters.AddWithValue("@product_name", pr.product_name);
                        command.Parameters.AddWithValue("@description", pr.description);
                        command.Parameters.AddWithValue("@price", pr.price);
                        command.Parameters.AddWithValue("@created_at",pr.created_at); 
                        command.ExecuteNonQuery();
                        Response.Redirect("../products/categorylist");

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
