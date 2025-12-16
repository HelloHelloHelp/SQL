using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
namespace MoviesAPI.Repository
{
    public interface IserieRepository
    {
        List<Models.Serie> GetSeries();
    }
    public class serieRepository : IserieRepository
    {

        public List<Models.Serie> GetSeries()
        {
            string constring = "Data Source=localhost;Initial Catalog=series;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            using (SqlConnection connection = new SqlConnection(constring))
            {
                connection.Open();
                List<Models.Serie> series = new List<Models.Serie>();
                string sql = "SELECT * FROM serie_info";

                using SqlCommand command = new SqlCommand(sql, connection);
                {
                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    using SqlDataReader reader = sqlDataReader;
                    {
                        while (reader.Read())
                        {
                            Models.Serie serie = new Models.Serie();
                            serie.ID = (int)reader["ID"];
                            serie.Title = reader["Title"].ToString();
                            serie.ImdbID = reader["ImdbID"].ToString();
                            serie.Director = reader["Director"].ToString();
                            serie.Year = reader["Year"].ToString();
                            serie.Genre = reader["Genre"].ToString();
                            serie.TotalSeasons = reader["TotalSeasons"].ToString();
                            serie.Rated = reader["Rated"].ToString();
                            serie.Language = reader["Language"].ToString();
                            serie.Runtime = reader["Runtime"].ToString();
                            serie.Plot = reader["Plot"].ToString();
                            serie.Poster = reader["Poster"] == DBNull.Value ? null : (byte[])reader["Poster"];
                            serie.Watched = reader["Watched"].ToString();
                            serie.ImdbRating = reader["ImdbRating"].ToString();
                            series.Add(serie);
                        }
                    }

                    reader.Close();
                }


                return series;
            }
        }
    }
}
