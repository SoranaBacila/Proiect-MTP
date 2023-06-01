using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proiect_MTP.Pages.Item;
using System.Data.SqlClient;
using System.IO;


namespace Proiect_MTP.Pages
{
    public class LoginModel : PageModel
    {
        
        public User user = new User();
        public String errorMessage = "";
        public String succesMessage = "";
        public String parola = "";
        
        public void OnGet()
        {
            
        }

        public void OnPost()
        {
            user.email = Request.Form["email"];
            user.parola = Request.Form["parola"];

            if ((user.email.Length == 0) || (user.parola.Length == 0))
            {
                errorMessage = "All the fields are required!";
                return;
            }

            if (!user.email.Contains("@"))
            {
                errorMessage = "The email address is not valid";
                return;
            }

            try
            {
                String connectionString = "Data Source=LAPTOP-EL05O585;Initial Catalog=mtp;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT Parola FROM utilizatori WHERE email=@email";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@email", user.email);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                parola = reader.GetString(0);
                            }

                            if (parola == user.parola)
                            {
								Response.Cookies.Append("currentUser", user.email);

								Response.Redirect("/Item/Index?user=" + user.email);
                               
							}
                            else
                                errorMessage = "Email or password are wrong";

                        }
                    }


                }


            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }
    
        }
    }
}
