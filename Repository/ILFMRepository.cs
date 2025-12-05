using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using MoviesAPI.Models;
namespace MoviesAPI.Repository
{
    public interface ILFMRepository
    {
        List<Models.LFM> GetLFM();
    }
    public class LFMRepository : ILFMRepository
    {

        public List<Models.LFM> GetLFM()
        {
            string constring = "Data Source=localhost;Initial Catalog=LFMAS;Integrated Security=True;Trust Server Certificate=True";
            using (SqlConnection connection = new SqlConnection(constring))
            {
                connection.Open();
                List<Models.LFM> Mrecommends = new List<Models.LFM>();
                string sql = "SELECT * FROM LFM";

                using SqlCommand command = new SqlCommand(sql, connection);
                {
                  
                    using SqlDataReader reader = command.ExecuteReader();
                    {
                        while (reader.Read())
                        {
                            Models.LFM Mrecommend = new Models.LFM();
                            Mrecommend.ID = (int)reader["ID"];
                            Mrecommend.Titel = reader["Titel"].ToString();
                            Mrecommend.Date = (int)reader["Date"];
                            Mrecommend.Genre = reader["Genre"].ToString();
                            Mrecommend.Restricting_age = reader["Restricting_age"] == DBNull.Value ? null : (int)reader["Restricting_age"];
                            Mrecommend.Platform = reader["Platform"].ToString();
                            Mrecommend.Rating = reader.IsDBNull(reader.GetOrdinal("Rating")) ? 0 : reader.GetInt32(reader.GetOrdinal("Rating"));
                            Mrecommends.Add(Mrecommend);
                        }
                    }

                    reader.Close();
                }


                return Mrecommends;
            }
        }
    }
}
