using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace KursApp
{
    public class UsersCommand
    {
        SqlConnection sqlConnect;
        public UsersCommand()
        {
            string pathToDb = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database.mdf");
            pathToDb = Trimer(ref pathToDb);
            //pathToDb = pathToDb.Replace("\\bin\\Debug", "");
            string key = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+pathToDb+"Database.mdf;Integrated Security=False;Trusted_Connection=True";
            sqlConnect = new SqlConnection(key);
        }

        private string Trimer(ref string pathToDb)
        {
            int count = 0;
            int capacity = 0;
            string line = "";
            for (int i = pathToDb.Length-1; i > 0; i--)
            {
                if(pathToDb[i]=='\\')
                {
                    count++;
                }
                if (count == 3)
                {
                    capacity = i + 1;
                    break;
                }
            }
            for (int i = 0; i < capacity; i++)
            {
                line += pathToDb[i];
            }
            return line;
        }


        /// <summary>
        /// Аутентификация
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns> возвращает 2 если менеджер, 1 если тестер, 0 если ввод неверный</returns>
        public async Task<int> InLogin(string login, string password)
        {
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM[Users]", sqlConnect);
            await sqlConnect.OpenAsync();

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {

                    if (login == Convert.ToString(sqlReader["Login"])
                        && password == Convert.ToString(sqlReader["Password"]))
                    {
                        if ("MainManager" == Convert.ToString(sqlReader["Position"]))
                        {
                            return 3;
                        }
                        else
                        {
                            if ("ProjectManager" == Convert.ToString(sqlReader["Position"]))
                            {
                                return 2;
                            }
                            return 1;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
                sqlConnect.Close();
            }
            return 0;
        }       
        public async Task<List<User>> GiveAllUsers()
        {
            List<User> lst = new List<User>();
            SqlDataReader sqlReader = null;
            await sqlConnect.OpenAsync();
            SqlCommand command = new SqlCommand("SELECT * FROM[Users]", sqlConnect);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    lst.Add(new User(Convert.ToInt32(sqlReader["id"]), Convert.ToString(sqlReader["Name"]),
                        Convert.ToString(sqlReader["Login"]), Convert.ToString(sqlReader["Password"]), Convert.ToString(sqlReader["Position"])));
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
                sqlConnect.Close();
            }
        }
        public async Task<User> GiveUser(string login, string password)
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
                        return (new User(Convert.ToInt32(sqlReader["id"]), Convert.ToString(sqlReader["Name"]),
                            Convert.ToString(sqlReader["Login"]), Convert.ToString(sqlReader["Password"]), Convert.ToString(sqlReader["Position"])));

                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
                sqlConnect.Close();
            }
            return null;
        }

        public async Task<User> GiveOwner(string login)
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

                    if (login == Convert.ToString(sqlReader["Login"]))
                    {
                        return (new User(Convert.ToInt32(sqlReader["id"]), Convert.ToString(sqlReader["Name"]),
                            Convert.ToString(sqlReader["Login"]), Convert.ToString(sqlReader["Password"]), Convert.ToString(sqlReader["Position"])));

                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
                sqlConnect.Close();
            }
            return null;
        }
    }
}
