﻿using System;
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
        public UsersCommand()
        {
            string key = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\GitHub\Kursach\Kursach\KursApp\KursApp\Database.mdf;Integrated Security=True";
            sqlConnect = new SqlConnection(key);
        }

        /// <summary>
        /// Аутентификация
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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
