using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proiect_MTP.Pages.Item;
using System.Data.SqlClient;
using System.Dynamic;

namespace Proiect_MTP.Pages
{
    public class RegisterModel : PageModel
    {
        public User user = new User();
        public String errorMessage = "";
        public String succesMessage = "";

        public void OnGet()
        {
        }

        public void OnPost() 
        {
            user.nume = Request.Form["nume"];
            user.email = Request.Form["email"];
            user.parola = Request.Form["parola"];
            
            string rpt_parola = Request.Form["rpt_parola"];

            if ((user.nume.Length == 0) || (user.email.Length == 0) || (user.parola.Length == 0) || (rpt_parola.Length ==0))
            {
                errorMessage = "All the fields are required!";
                return;
            }

			if (!user.email.Contains("@"))
			{
                errorMessage = "The email address is not valid";
                return;
			}

            if(rpt_parola != user.parola)
            {
                errorMessage = "Passwords does not match";
                return;
            }

			try
            {
                String connectionString = "Data Source=DESKTOP-GO07E3A;Initial Catalog=myCars;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Users" +
                                 "(nume, email, parola) VALUES " +
                                 "(@nume, @email, @parola);";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nume", user.nume);
                        command.Parameters.AddWithValue("@email", user.email);
                        command.Parameters.AddWithValue("@parola", user.parola);

                        command.ExecuteNonQuery();
                    }
                }


            }
            catch (Exception e)
            {
                errorMessage = "Email address already in use";
                return;
            }

            succesMessage = "New user added successfully";

			Response.Cookies.Append("currentUser", user.email);

			Response.Redirect("/Item/Index?user=" + user.email);

			

			user.nume = "";
            user.email = "";
            user.parola = "";
        }
    }

    public class User
    {
        public String nume;
        public String email;
        public String parola;
    }
}
