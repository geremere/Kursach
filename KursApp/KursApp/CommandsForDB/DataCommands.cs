﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace KursApp
{
    public class DataCommands
    {
        SqlConnection sqlConnect;

        public DataCommands()
        {
            string key = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\GitHub\Kursach\Kursach\KursApp\KursApp\Database.mdf;Integrated Security=True";
            sqlConnect = new SqlConnection(key);
        }

        /// <summary>
        /// записывает новые риски в проект
        /// </summary>
        /// <param name="risklst"></param>
        /// <param name="ProjectName"></param>
        /// <returns></returns>
        public async Task IsertNewRisks(Risk risk, string ProjectName, User Owner)
        {
            await sqlConnect.OpenAsync();
            SqlCommand command =
                new SqlCommand("INSERT INTO [RiskData] (RiskName,Probability,Influence,Project,SourseOfRisk,Effects,Descriprion,TypeOfProject) VALUES(@riskName,@probability,@influence,@project,@sourseOfRisk,@effects,@descriprion,@typeOfProject)", sqlConnect);

            command.Parameters.AddWithValue("RiskName", risk.RiskName);
            command.Parameters.AddWithValue("Probability", risk.Probability);
            command.Parameters.AddWithValue("Influence", risk.Influence);
            command.Parameters.AddWithValue("Project", ProjectName);
            command.Parameters.AddWithValue("SourseOfRisk", risk.SoursOfRisk);
            command.Parameters.AddWithValue("Effects", risk.Effects);
            command.Parameters.AddWithValue("Descriprion", risk.Description);
            command.Parameters.AddWithValue("TypeOfProject", risk.TypeOfProject);
            command.Parameters.AddWithValue("OwnerLogin", Owner.Login);
            command.Parameters.AddWithValue("OwnerId", Owner.Id);

            await command.ExecuteNonQueryAsync();
                sqlConnect.Close();

        }

         /// <summary>
         /// выдает все риски проекта
         /// </summary>
         /// <param name="ProjectName"></param>
         /// <returns></returns>
        public async Task<List<Risk>> GiveAllRisks(string ProjectName)
        {
            List<Risk> lst = new List<Risk>();
            SqlDataReader sqlReader = null;
            await sqlConnect.OpenAsync();
            SqlCommand command = new SqlCommand("SELECT * FROM[RiskData]", sqlConnect);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {     
                    if(ProjectName== Convert.ToString(sqlReader["Project"]))
                    {
                        lst.Add(new Risk(Convert.ToString(sqlReader["RiskName"]),Convert.ToString(sqlReader["SourseOfRisk"]),
                        Convert.ToString(sqlReader["Effects"]),Convert.ToString(sqlReader["Descriprion"]), 
                        Convert.ToString(sqlReader["TypeOfProject"]), 
                        Convert.ToDouble(Parsing(Convert.ToString(sqlReader["Probability"]))),
                        Convert.ToDouble(Parsing(Convert.ToString(sqlReader["Influence"])))));
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
                sqlConnect.Close();

            }
        }

        private string Parsing(string l)
        {
            string ret="";
            for (int i = 0; i < l.Length; i++)
            {
                if (l[i] == '.') ret+= ',';
                else
                {
                    ret += l[i];
                }
            }
            return ret;
        }
    }
}
