using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System;

namespace KursApp
{
    public class RisksCommand
    {
        SqlConnection sqlConnect;

        public RisksCommand()
        {
            string key = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\GitHub\Kursach\Kursach\KursApp\KursApp\Database.mdf;Integrated Security=True";
            sqlConnect = new SqlConnection(key);
        }
        public async Task<List<Risk>> GiveAllProjects(string type)
        {
            if (type == "") throw new FormatException("Wrong in type Of Project");//catch this exception
            List<Risk> lst = new List<Risk>();
            SqlDataReader sqlReader = null;
            await sqlConnect.OpenAsync();
            SqlCommand command = new SqlCommand("SELECT * FROM[Risks]", sqlConnect);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    if (type==Convert.ToString(sqlReader["Type of project"]))
                    {
                        lst.Add(new Risk(Convert.ToInt32(sqlReader["Id"]), Convert.ToString(sqlReader["Risk Name"]),
                            Convert.ToString(sqlReader["Sourse Of Risk"]), Convert.ToString(sqlReader["Effects"]), 
                            Convert.ToString(sqlReader["Descriprion"]), Convert.ToString(sqlReader["Type of project"])));
                    }
                }
                return lst;
            }
            catch (Exception)
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
