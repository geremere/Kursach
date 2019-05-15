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
    class TreeCommands
    {
        SqlConnection sqlConnect;

        public TreeCommands()
        {
            string pathToDb = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database.mdf");
            pathToDb = Trimer(ref pathToDb);
            string key = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + pathToDb + "Database.mdf;Integrated Security=True;Trusted_Connection=True";
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

        public async Task IsertNewVertex(Vertexcs current, int Id)
        {
            await sqlConnect.OpenAsync();
            SqlCommand command =
                new SqlCommand("INSERT INTO [RiskTree] (ParentId,Description,Cost,Probability,X,Y) VALUES(@ParentId,@Description,@Cost,@Probability,@X,@Y)", sqlConnect);

            command.Parameters.AddWithValue("ParentId", Id);
            command.Parameters.AddWithValue("Description", current.Description);
            command.Parameters.AddWithValue("Cost", current.Cost);
            command.Parameters.AddWithValue("Probability", current.Probability);
            command.Parameters.AddWithValue("X", current.X);
            command.Parameters.AddWithValue("Y", current.Y);

            await command.ExecuteNonQueryAsync();
            sqlConnect.Close();

        }

        /// <summary>
        /// проверяет если данный элемент в бд
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<bool> Exist(int parentId)
        {
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM[RiskTree]", sqlConnect);
            await sqlConnect.OpenAsync();

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    if (parentId == Convert.ToInt32(sqlReader["ParentId"]) && Convert.ToDouble(Parsing(Convert.ToString(sqlReader["Probability"]))) == default(Double))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
                sqlConnect.Close();
            }
        }

        /// <summary>
        /// выдает вершину графа
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<Vertexcs> GiveFristVertex(int parentId)
        {
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM[RiskTree]", sqlConnect);
            await sqlConnect.OpenAsync();

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    if (parentId == Convert.ToInt32(sqlReader["ParentId"]) && Convert.ToDouble(Parsing(Convert.ToString(sqlReader["Probability"]))) == default(Double))
                    {

                        return new Vertexcs(Convert.ToInt32(sqlReader["Id"]),Convert.ToInt32(sqlReader["ParentId"]), Convert.ToString(sqlReader["Description"]), Convert.ToDouble(Parsing(Convert.ToString(sqlReader["X"]))), Convert.ToDouble(Parsing(Convert.ToString(sqlReader["Y"]))));
                    }
                }
                return null;
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

        public async Task<Vertexcs> GiveVertex(Vertexcs vertex)
        {
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM[RiskTree]", sqlConnect);
            await sqlConnect.OpenAsync();

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    if (vertex.ParentId == Convert.ToInt32(sqlReader["ParentId"]) &&
                        Convert.ToDouble(Parsing(Convert.ToString(sqlReader["Probability"]))) != default(Double) &&
                        Convert.ToDouble(Parsing(Convert.ToString(sqlReader["X"]))) == vertex.X &&
                        Convert.ToDouble(Parsing(Convert.ToString(sqlReader["Y"]))) == vertex.Y)
                    {

                        return new Vertexcs(Convert.ToInt32(sqlReader["Id"]), Convert.ToInt32(sqlReader["ParentId"]), Convert.ToString(sqlReader["Description"]), Convert.ToDouble(Parsing(Convert.ToString(sqlReader["Cost"]))),
                            Convert.ToDouble(Parsing(Convert.ToString(sqlReader["Probability"]))), Convert.ToDouble(Parsing(Convert.ToString(sqlReader["X"]))), Convert.ToDouble(Parsing(Convert.ToString(sqlReader["Y"]))));
                    }
                }
                return null;
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
        /// выдает все вершины
        /// </summary>
        /// <returns></returns>
        public async Task<List<Vertexcs>> GiveALlVertex()
        {
            List<Vertexcs> lstv = new List<Vertexcs>();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM[RiskTree]", sqlConnect);
            await sqlConnect.OpenAsync();

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    lstv.Add(new Vertexcs(Convert.ToInt32(sqlReader["Id"]),Convert.ToInt32(sqlReader["ParentId"]), Convert.ToString(sqlReader["Description"]), Convert.ToDouble(Parsing(Convert.ToString(sqlReader["Cost"]))), Convert.ToDouble(Parsing(Convert.ToString(sqlReader["Probability"]))), Convert.ToDouble(Parsing(Convert.ToString(sqlReader["X"]))), Convert.ToDouble(Parsing(Convert.ToString(sqlReader["Y"])))));
                }
                return lstv;
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
        public async Task DeliteVerTex(Vertexcs vertex)
        {
            try
            {
                await sqlConnect.OpenAsync();
                SqlCommand command = new SqlCommand("DELETE FROM [RiskTree] WHERE [Id]=@Id", sqlConnect);
                command.Parameters.AddWithValue("Id", vertex.Id);
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

    }
}
