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
    public class DataCommands
    {
        SqlConnection sqlConnect;

        public DataCommands()
        {
            string pathToDb = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database.mdf");
            pathToDb = pathToDb.Replace("\\bin\\Debug", "");
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
                new SqlCommand("INSERT INTO [RiskData] (RiskName,Probability,Influence,Project,SourseOfRisk,Effects,Descriprion,TypeOfProject,OwnerLogin,OwnerId) VALUES(@riskName,@probability,@influence,@project,@sourseOfRisk,@effects,@descriprion,@typeOfProject,@OwnerLogin,@OwnerId)", sqlConnect);

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

        public async Task UpdateRisks(Risk risk, string ProjectName, User Owner)
        {
            await sqlConnect.OpenAsync();
            SqlCommand command =
                new SqlCommand("UPDATE [RiskData] SET [Probability]= @Probability, [Influence]= @Influence, [OwnerLogin]= @OwnerLogin, [OwnerId]= @OwnerId WHERE id=@id", sqlConnect);

            command.Parameters.AddWithValue("Id", risk.Id);
            command.Parameters.AddWithValue("Probability", risk.Probability);
            command.Parameters.AddWithValue("Influence", risk.Influence);
            command.Parameters.AddWithValue("OwnerLogin", Owner.Login);
            command.Parameters.AddWithValue("OwnerId", Owner.Id);

            await command.ExecuteNonQueryAsync();
            sqlConnect.Close();

        }

        public async Task UpdateRisks(Risk risk, string ProjectName)
        {
            await sqlConnect.OpenAsync();
            SqlCommand command =
                new SqlCommand("UPDATE [RiskData] SET [Probability]= @Probability, [Influence]= @Influence WHERE id=@id", sqlConnect);

            command.Parameters.AddWithValue("Id", risk.Id);
            command.Parameters.AddWithValue("Probability", risk.Probability);
            command.Parameters.AddWithValue("Influence", risk.Influence);
            await command.ExecuteNonQueryAsync();
            sqlConnect.Close();

        }

        public async Task DeliteRisk(Risk risk)
        {
            try
            {
                await sqlConnect.OpenAsync();
                SqlCommand command = new SqlCommand("DELETE FROM [RiskData] WHERE [Id]=@Id", sqlConnect);

                command.Parameters.AddWithValue("Id", risk.Id);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
            }
            finally
            {
                sqlConnect.Close();
            }

        }

        /// <summary>
        /// выдает все риски проекта
        /// </summary>
        /// <param name="ProjectName"></param>
        /// <returns></returns>
        public async Task<List<Risk>> GiveAllRisks(Project project)
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
                    if(project.Name == Convert.ToString(sqlReader["Project"]))
                    {
                        lst.Add(new Risk(Convert.ToInt32(sqlReader["Id"]),Convert.ToString(sqlReader["RiskName"]),Convert.ToString(sqlReader["SourseOfRisk"]),
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

        /// <summary>
        /// выдает все риски ответвенного за проект
        /// </summary>
        /// <param name="project"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<List<Risk>> GiveOwnersRisks(Project project,User user)
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
                    if (project.Name == Convert.ToString(sqlReader["Project"]) && user.Id==Convert.ToInt32(sqlReader["OwnerId"]))
                    {
                        lst.Add(new Risk(Convert.ToInt32(sqlReader["Id"]), Convert.ToString(sqlReader["RiskName"]), Convert.ToString(sqlReader["SourseOfRisk"]),
                        Convert.ToString(sqlReader["Effects"]), Convert.ToString(sqlReader["Descriprion"]),
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

        /// <summary>
        /// выдает названия всех проектов
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<List<string>> GiveOwnersProjects(User user)
        {
            List<string> lst = new List<string>();
            SqlDataReader sqlReader = null;
            await sqlConnect.OpenAsync();
            SqlCommand command = new SqlCommand("SELECT * FROM[RiskData]", sqlConnect);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    if (user.Login == Convert.ToString(sqlReader["OwnerLogin"]))
                    {
                        if(CheckIn(lst,Convert.ToString(sqlReader["Project"])))
                        {
                            lst.Add(Convert.ToString(sqlReader["Project"]));
                        }
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

        private bool CheckIn(List<string> lst,string project)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                if (project == lst[i]) return false;
            }
            return true;
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
