using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.IO;
using System.Data.SqlTypes;
using System.Security.Policy;
using System.Collections.Generic;
using System.Text.Json;
using System.Dynamic;

namespace Proiect_MTP.Pages.Item
{
    public class IndexModel : PageModel
    {
        public List<CatelusiInfo> listCatelusi = new List<CatelusiInfo>();
		public string user_email = "";
        public String sql1 = "";
        public String sql2 = "";
        public bool gol = false;
        public string userEmail = "";
        public string searchRasa = "";
        public string searchResult = "";


		public void OnGet()
        {
            user_email = Request.Query["user"];

            if(user_email == null ) 
            {
				string currentEmail = Request.Cookies["currentUser"];

                Response.Redirect("/Item/Index?user=" + currentEmail);
			}
        
    
            try
            {
                String connectionString = "Data Source=LAPTOP-EL05O585;Initial Catalog=mtp;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    sql1 = "SELECT * FROM catelusi WHERE [utilizator]=@user";
                    sql2 = "SELECT nume FROM catelusi WHERE [utilizator]=@user";

                    using (SqlCommand command1 = new SqlCommand(sql1, connection))
                    {
                        SqlCommand command2 = new SqlCommand(sql2, connection);

                        command1.Parameters.AddWithValue("@user", user_email);
                        command2.Parameters.AddWithValue("@user", user_email);

                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CatelusiInfo catelusiInfo = new CatelusiInfo();

                                catelusiInfo.id = "" + reader.GetInt32(0);
                                catelusiInfo.utilizator = reader.GetString(1);
                                catelusiInfo.nume_catelus = reader.GetString(2);
                                catelusiInfo.culoare = "" + reader.GetString(3);
                                catelusiInfo.rasa = "" + reader.GetString(4);
                                catelusiInfo.varsta = "" + reader.GetString(5);
                                catelusiInfo.talie = "" + reader.GetString(6);

                                listCatelusi.Add(catelusiInfo);
                            }

                            if (listCatelusi.Count == 0)
                                gol = true;

                        }


                    }
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
			}

		}

        
        public void OnPost()
        {
            OnGet();
			searchRasa = Request.Form["Rasa"];
            listCatelusi = listCatelusi.Where(c => c.rasa.ToLower().Contains(searchRasa.ToLower())).ToList();
            if (listCatelusi.Count == 0)
                searchResult = "Rasa nu a fost gasita";
                
		}
        


	}

	public class CatelusiInfo
    {
        public String id = "";
        public String utilizator = "";
        public String nume_catelus = "";
        public String culoare = "";
        public String rasa = "";
        public String varsta = "";
        public String talie = "";
    }
}
