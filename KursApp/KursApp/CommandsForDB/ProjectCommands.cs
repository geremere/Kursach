﻿using System;
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

        /// <summary>
        /// Выдать все рпоекты в БД
        /// </summary>
        /// <returns></returns>
        public async Task<List<Project>> GiveAllProjects()
        {
            List<Project> lst = new List<Project>();
            SqlDataReader sqlReader = null;
            await sqlConnect.OpenAsync();
            SqlCommand command = new SqlCommand("SELECT * FROM[Projects]", sqlConnect);

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

        /// <summary>
        /// Записать проект
        /// </summary>
        /// <param name="name"></param>
        /// <param name="owner"></param>
        /// <param name="type"></param>
        public async Task IsertNewProject(string name, string owner, string type)
        {
            if (name == "")
                throw new FormatException("Поле Name заполнено некорректно");
                //return false;
                    
            await sqlConnect.OpenAsync();
            SqlCommand command = new SqlCommand("INSERT INTO [Projects] (Name,Owner,Type) VALUES(@Name,@OWner,@Type)", sqlConnect);
            command.Parameters.AddWithValue("Name", name);
            command.Parameters.AddWithValue("Owner", owner);
            command.Parameters.AddWithValue("Type", type);
            await command.ExecuteNonQueryAsync();
            //return true;
        }
    }
}