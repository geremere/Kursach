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
                    new SqlCommand("INSERT INTO [RiskData] (DRiskName,DProbability,DInfluence,DProject,DSourseOfRisk,DEffects,DDescriprion,DTypeOfProject) VALUES(@DriskName,@Dprobability,@Dinfluence,@Dproject,@DsourseOfRisk,@Deffects,@Ddescriprion,@DtypeOfProject)", sqlConnect);

                command.Parameters.AddWithValue("DRiskName", risklst[i].RiskName);
                command.Parameters.AddWithValue("DProbability", risklst[i].Probability);
                command.Parameters.AddWithValue("DInfluence", risklst[i].Influence);
                command.Parameters.AddWithValue("DProject", ProjectName);
                command.Parameters.AddWithValue("DSourseOfRisk", risklst[i].SoursOfRisk);
                command.Parameters.AddWithValue("DEffects", risklst[i].Effects);
                command.Parameters.AddWithValue("DDescriprion", risklst[i].Description);
                command.Parameters.AddWithValue("DTypeOfProject", risklst[i].TypeOfProject);


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
                        lst.Add(new Risk(Convert.ToString(sqlReader["DRiskName"]),Convert.ToString(sqlReader["DSourseOfRisk"]),
                        Convert.ToString(sqlReader["DEffects"]),Convert.ToString(sqlReader["DDescriprion"]), 
                        Convert.ToString(sqlReader["DTypeOfProject"]), Convert.ToDouble(sqlReader["DProbability"]),
                        Convert.ToDouble(sqlReader["DInfluence"])));
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
    }
}
