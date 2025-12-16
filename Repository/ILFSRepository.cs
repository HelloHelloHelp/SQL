using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using MoviesAPI.Models;
namespace MoviesAPI.Repository
{
    public interface ILFSRepository
    {
        List<Models.LFS> GetLFS();
    }
    public class LFSRepository : ILFSRepository
    {

        public List<Models.LFS> GetLFS()
        {
            string constring = "Data Source=localhost;Initial Catalog=LFMAS;Integrated Security=True;Trust Server Certificate=True";
            using (SqlConnection connection = new SqlConnection(constring))
            {
                connection.Open();
                List<Models.LFS> Srecommends = new List<Models.LFS>();
                string sql = "SELECT * FROM LFS";

                using SqlCommand command = new SqlCommand(sql, connection);
                {
                
                    using SqlDataReader reader = command.ExecuteReader();
                    {
                        while (reader.Read())
                        {
                            Models.LFS Srecommend = new Models.LFS();
                            Srecommend.ID = (int)reader["ID"];
                            Srecommend.Title = reader["Title"].ToString();
                            Srecommend.Year = reader["Year"].ToString();
                            Srecommend.Genre = reader["Genre"].ToString();
                            Srecommend.TotalSeasons = reader["TotalSeasons"].ToString();
                            Srecommend.Runtime = reader["Runtime"].ToString();
                            Srecommend.Plot = reader["Plot"].ToString();
                            Srecommend.Poster = reader["Poster"] == DBNull.Value ? null : (byte[])reader["Poster"];
                            Srecommend.ImdbRating = reader["ImdbRating"].ToString();
                            Srecommends.Add(Srecommend);
                        }
                    }

                    reader.Close();
                }

                return Srecommends;
            }
        }
    }
}
