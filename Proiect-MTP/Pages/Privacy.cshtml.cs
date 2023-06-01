using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proiect_MTP.Pages.Item;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;

namespace Proiect_MTP.Pages
{
    public class PrivacyModel : PageModel
	{ 
		private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }












		//public List<engineInfo> listEngines = new List<engineInfo>();
		//public string sql = "";
		//public string sql2 = "";
		//public string user_email = "";
		//public string engine_name = "";
		public string errorMessage = "";

        public void OnGet()
        {
			/*
			user_email = Request.Query["user"];

			if (user_email == null)
			{
				StreamReader fileReader = new StreamReader("C:\\Users\\drago\\source\\repos\\Proiect-MTP\\Proiect-MTP\\currentUser.txt");

				string currentEmail = fileReader.ReadLine();

				Response.Redirect("/Privacy?user=" + currentEmail);
			}

			try
			{
				String connectionString = "Data Source=DESKTOP-GO07E3A;Initial Catalog=myCars;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					sql = "SELECT * FROM engines WHERE [user]=@user";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@user", user_email);

						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								engineInfo engine = new engineInfo();

								engine.name = reader.GetString(0);
								engine.description = reader.GetString(1);
								engine.clicks = reader.GetInt32(2);

								listEngines.Add(engine);
							}

						}
					}
				}


			}
			catch (Exception e)
			{
				Console.WriteLine("THIS HAPPENED - Exception: " + e.ToString());
			}
			*/
		}

		
		public void OnPost() 
		{
			/*
			user_email = Request.Query["user"];
			engine_name = Request.Form["ename"];

			try
			{
				String connectionString = "Data Source=DESKTOP-GO07E3A;Initial Catalog=myCars;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					sql2 = "UPDATE engines SET clicks = clicks + 1 WHERE [user]=@user AND name=@engine_name";


					using (SqlCommand command = new SqlCommand(sql2, connection))
					{
						command.Parameters.AddWithValue("@user", user_email);
						command.Parameters.AddWithValue("@engine_name", engine_name);

						command.ExecuteNonQuery();
					}
				}


			}
			catch (Exception e)
			{
				errorMessage = e.Message;
			}

			//OnGet();
			*/
		}


























    }
		

	public class engineInfo
	{
		public string name;
		public string description;
		public int clicks;
	}
}