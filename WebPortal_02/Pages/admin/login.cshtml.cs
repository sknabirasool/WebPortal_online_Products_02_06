using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebPortal_02.Pages.admin
{
    public class loginModel : PageModel
    {
       public logininfo log = new logininfo();

        public string ErrorMessage = "";
        public string SuccessMessage = "";
        public string UserName;


        public void OnGet()
        {
            string EMAIL = Request.Query["email"];
            string PASSWORD = Request.Query["password"];                            

            try
            {
                String con = "Data Source=.;Initial Catalog=Registration_db;Integrated Security=True";

                using (SqlConnection cs = new SqlConnection(con))
                {
                    cs.Open();
                    string sqlquery = "Select * from tbl_users where email=@email and password=@password";
                    using (SqlCommand command = new SqlCommand(sqlquery, cs))
                    {
                        command.Parameters.AddWithValue("@email",EMAIL);
                        command.Parameters.AddWithValue("@password",PASSWORD);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            //int count = reader.FieldCount();
                            if (reader.HasRows)
                            {
                                if(reader["user_name"]!=null)
                                {
                                    UserName = reader["user_name"].ToString();
                                }
                                Response.Redirect("welcome?usName="+UserName);



                            }
                            else
                            {

                                ErrorMessage = "Invalid Login Details";
                                return;

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

        public void OnPost()
        {
            log.email = Request.Form["email"];
            log.password = Request.Form["password"];
        }

    }

    public class logininfo
    {
        public string email;
        public string password;

    }





}
