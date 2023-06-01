using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Proiect_MTP.Pages.Item
{
    public class CreateModel : PageModel
    {
        public CatelusiInfo catelusiInfo = new CatelusiInfo();
        public String errorMessage = "";
        public String succesMessage = "";
        public String user_email = "";
        public void OnGet()
        {
            user_email = Request.Query["user"];
        }

        public void OnPost() 
        {
            user_email = Request.Query["user"];
            catelusiInfo.nume_catelus = Request.Form["Nume_catelus"];
            catelusiInfo.culoare = Request.Form["Culoare"];
            catelusiInfo.rasa = Request.Form["Rasa"];
            catelusiInfo.varsta = Request.Form["Varsta"];
            catelusiInfo.talie = Request.Form["Talie"];


            try 
            {
                String connectionString = "Data Source=LAPTOP-EL05O585;Initial Catalog=mtp;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                { 
                    connection.Open();
                    String sql = "INSERT INTO catelusi" +
                                 "(nume_catelus, culoare, rasa, varsta, talie, [utilizator]) VALUES " +
                                 "(@nume_catelus, @culoare, @rasa, @varsta, @talie, @user);";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nume_catelus", catelusiInfo.nume_catelus);
                        command.Parameters.AddWithValue("@culoare", catelusiInfo.culoare);
                        command.Parameters.AddWithValue("@rasa", catelusiInfo.rasa);
                        command.Parameters.AddWithValue("@varsta", catelusiInfo.varsta);
                        command.Parameters.AddWithValue("@talie", catelusiInfo.talie);
                        command.Parameters.AddWithValue("@user", user_email);

                        command.ExecuteNonQuery();
                    }
                }


            }
            catch (Exception e) 
            {
                errorMessage = e.Message;
                return;
            }

            catelusiInfo.nume_catelus = "";
            catelusiInfo.culoare = "";
            catelusiInfo.rasa = "";
            catelusiInfo.varsta = "";
            catelusiInfo.talie = "";

            succesMessage = "Catelus adaugat cu succes.";
            Response.Redirect("/Item/Index?user=" + user_email);
        }
    }
}
