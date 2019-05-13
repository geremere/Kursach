using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace KursApp
{
    class TreeCommands
    {
        SqlConnection sqlConnect;

        public TreeCommands()
        {
            string key = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\GitHub\Kursach\Kursach\KursApp\KursApp\Database.mdf;Integrated Security=True";
            sqlConnect = new SqlConnection(key);
        }
        public async Task IsertNewVertex(Vertexcs current, Vertexcs parent)
        {
            await sqlConnect.OpenAsync();
            SqlCommand command =
                new SqlCommand("INSERT INTO [RiskTree] (ParentId,Cost,Probability,X,Y,Row) VALUES(@ParentId,@Cost,@Probability,@X,@Y,@Row)", sqlConnect);

            command.Parameters.AddWithValue("ParentId", parent.Id);
            command.Parameters.AddWithValue("Cost", current.Cost);
            command.Parameters.AddWithValue("Probability", current.Probability);
            command.Parameters.AddWithValue("X", current.X);
            command.Parameters.AddWithValue("Y", current.Y);
            command.Parameters.AddWithValue("Row", current.Row);

            await command.ExecuteNonQueryAsync();
            sqlConnect.Close();

        }
    }
}
