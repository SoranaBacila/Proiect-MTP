using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Proiect_MTP.Pages.Item
{
    public class EditModel : PageModel
    {
        public CatelusiInfo catelusiInfo = new CatelusiInfo();
        public String errorMessage = "";
        public String succesMessage = "";
        public String user_email = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            user_email = Request.Query["user"];


            try 
            {
                String connectionString = "Data Source=LAPTOP-EL05O585;Initial Catalog=mtp;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM catelusi WHERE id=@id";
                    using(SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        { 
                            if(reader.Read()) 
                            {
                                catelusiInfo.id = "" + reader.GetInt32(0);
                                catelusiInfo.nume_catelus = reader.GetString(2);
                                catelusiInfo.culoare = reader.GetString(3);
                                catelusiInfo.rasa = reader.GetString(4);
                                catelusiInfo.varsta = reader.GetString(5);
                                catelusiInfo.talie = reader.GetString(6);
                            }
                        }
                    }
                }


            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
        }

        public void OnPost() 
        {
            user_email = Request.Query["user"];

            catelusiInfo.id = Request.Form["id"];
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
                    String sql = "UPDATE catelusi " + 
                                 "SET nume_catelus=@nume_catelus, culoare=@culoare, rasa=@rasa, varsta=@varsta, talie=@talie" +
                                 " WHERE id=@id";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nume_catelus", catelusiInfo.nume_catelus);
                        command.Parameters.AddWithValue("@culoare", catelusiInfo.culoare);
                        command.Parameters.AddWithValue("@rasa", catelusiInfo.rasa);
                        command.Parameters.AddWithValue("@varsta", catelusiInfo.varsta);
                        command.Parameters.AddWithValue("@talie", catelusiInfo.talie);
                        command.Parameters.AddWithValue("@id", catelusiInfo.id);
                        

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }

            Response.Redirect("/Item/Index?user=" + user_email);    
        }
    }
}
