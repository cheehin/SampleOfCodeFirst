using System.Data;
using System.Data.SqlClient;

namespace Nation.Models
{
    public class Procedure
    {

        //No problem
        public void ExeQuery(string query)
        {
            try
            {
                string conn = "Data Source=desktop-daefve4;Initial Catalog=DbSupplier;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    SqlCommand objCmd = new SqlCommand();
                    objCmd.Connection = connection;
                    objCmd.CommandText = query;
                    objCmd.ExecuteNonQuery();

                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        //No problem
        public DataTable SelectQuery(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                string conn = "Data Source=desktop-daefve4;Initial Catalog=DbSupplier;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    SqlCommand objCmd = new SqlCommand();
                    objCmd.Connection = connection;
                    objCmd.CommandText = query;
                    SqlDataAdapter sda = new SqlDataAdapter(objCmd);
                    sda.Fill(dt);

                    Console.WriteLine("No Issue");
                }
            }
            catch (Exception)
            {

                throw;
            }
            return dt;
        }
    }
}
