using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace KursApp
{
    public class UsersCommand
    {
        SqlConnection sqlConnect;
        User user = new User();

        public UsersCommand()
        {
            string key = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\радмир\source\repos\KursApp\KursApp\Database.mdf;Integrated Security=True";
            sqlConnect = new SqlConnection(key);
        }

        public async Task<bool> InLogin(string login, string password)
        {
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM[Users]", sqlConnect);
            await sqlConnect.OpenAsync();

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    //string login = Convert.ToString(sqlReader["Password"]);

                    if (login == Convert.ToString(sqlReader["Login"])
                        && password == Convert.ToString(sqlReader["Password"]))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
            return false;
        }
    }
}
