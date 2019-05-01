using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace KursApp
{
    public class ProjectCommands
    {
        SqlConnection sqlConnect;
        public ProjectCommands()
        {
            string key = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\GitHub\Kursach\Kursach\KursApp\KursApp\Database.mdf;Integrated Security=True";
            sqlConnect = new SqlConnection(key);
        }

        public async Task<List<Project>> GiveAllProjects()
        {
            List<Project> lst = new List<Project>();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM[Projects]", sqlConnect);
            await sqlConnect.OpenAsync();

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    lst.Add(new Project(Convert.ToInt32(sqlReader["id"]),Convert.ToString(sqlReader["Name"]), Convert.ToString(sqlReader["Owner"]),Convert.ToString(sqlReader["Type"])));
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }
    }
}
