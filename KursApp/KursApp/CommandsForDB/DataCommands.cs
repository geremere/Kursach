using System;
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

        public async Task IsertNewRisks(List<Risk> risklst, string ProjectName)
        {
            for (int i = 0; i < risklst.Count; i++)
            {
                await sqlConnect.OpenAsync();
                SqlCommand command =
                    new SqlCommand("INSERT INTO [RiskData] (RiskName,Probability,Influence,Project,SourseOfRisk,Effects,Descriprion,TypeOfProject) VALUES(@riskName,@probability,@influence,@project,@sourseOfRisk,@effects,@descriprion,@typeOfProject)", sqlConnect);

                command.Parameters.AddWithValue("RiskName", risklst[i].RiskName);
                command.Parameters.AddWithValue("Probability", risklst[i].Probability);
                command.Parameters.AddWithValue("Influence", risklst[i].Influence);
                command.Parameters.AddWithValue("Project", ProjectName);
                command.Parameters.AddWithValue("SourseOfRisk", risklst[i].SoursOfRisk);
                command.Parameters.AddWithValue("Effects", risklst[i].Effects);
                command.Parameters.AddWithValue("Descriprion", risklst[i].Description);
                command.Parameters.AddWithValue("TypeOfProject", risklst[i].TypeOfProject);


                await command.ExecuteNonQueryAsync();
                sqlConnect.Close();

            }

        }

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
