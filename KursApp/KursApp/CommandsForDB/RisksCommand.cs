using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System;
using System.IO;

namespace KursApp
{
    public class RisksCommand
    {
        SqlConnection sqlConnect;

        public RisksCommand()
        {
            string pathToDb = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database.mdf");
            pathToDb = Trimer(ref pathToDb);
            string key = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + pathToDb + "Database.mdf;Integrated Security=False;Trusted_Connection=True";
            sqlConnect = new SqlConnection(key);
        }
        private string Trimer(ref string pathToDb)
        {
            int count = 0;
            int capacity = 0;
            string line = "";
            for (int i = pathToDb.Length - 1; i > 0; i--)
            {
                if (pathToDb[i] == '\\')
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
        /// метод выдающий все риски по типу проекта
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<List<Risk>> GiveRiskInTypeProject(string type)
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
                        lst.Add(new Risk(Convert.ToInt32(sqlReader["Id"]),
                            Convert.ToString(sqlReader["Risk Name"]),
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
                sqlConnect.Close();

            }
        }
        private string Parsing(string l)
        {
            string ret = "";
            for (int i = 0; i < l.Length; i++)
            {
                if (l[i] == '.') ret += ',';
                else
                {
                    ret += l[i];
                }
            }
            return ret;
        }

        /// <summary>
        /// выдает все риски проекта
        /// </summary>
        /// <returns></returns>
        public async Task<List<Risk>> GiveAllRisks()
        {
            List<Risk> lst = new List<Risk>();
            SqlDataReader sqlReader = null;
            await sqlConnect.OpenAsync();
            SqlCommand command = new SqlCommand("SELECT * FROM[Risks]", sqlConnect);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                        lst.Add(new Risk(Convert.ToInt32(sqlReader["Id"]),
                            Convert.ToString(sqlReader["Risk Name"]), Convert.ToString(sqlReader["Sourse of Risk"]),
                        Convert.ToString(sqlReader["Effects"]), Convert.ToString(sqlReader["Descriprion"]),
                        Convert.ToString(sqlReader["Type of Project"])));
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
        /// выдает все источники рисков
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GiveRisksSourse()
        {
            List<string> lst = new List<string>();
            SqlDataReader sqlReader = null;
            await sqlConnect.OpenAsync();
            SqlCommand command = new SqlCommand("SELECT * FROM[Risks]", sqlConnect);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    if (EntryCheck(Convert.ToString(sqlReader["Sourse of Risk"]),lst))
                    {
                        lst.Add(Convert.ToString(sqlReader["Sourse of Risk"]));
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

        public async Task<List<Risk>> GiveRisksInTypeSourse(string sourse,string type)
        {
            if (sourse == "") throw new FormatException("Wrong in type Of Project");//catch this exception
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
                    if (sourse == Convert.ToString(sqlReader["Sourse of Risk"]) && 
                        (type == Convert.ToString(sqlReader["Type of project"]) || "default" == Convert.ToString(sqlReader["Type of project"])))
                    {
                        lst.Add(new Risk(Convert.ToInt32(sqlReader["Id"]),Convert.ToString(sqlReader["Risk Name"]),
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
                sqlConnect.Close();

            }
        }

        private bool EntryCheck(string l, List<string> lst)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                if (l == lst[i]) return false;
            }
            return true;
        }
    }
}
