using System.Data;
using System.Net.Http;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using MoviesAPI.Models;
using static System.Net.WebRequestMethods;
using FluentNHibernate.Conventions.Inspections;
namespace MoviesAPI.Repository
{
    public interface ILFMRepository
    {
        List<Models.LFM> GetLFM();
       
    }
    public class LFMRepository : ILFMRepository
    {
        private readonly HttpClient _httpClient;

        public LFMRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;  
        }

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
                            Mrecommend.Title = reader["Title"].ToString();
                            Mrecommend.ImdbID = reader["ImdbID"].ToString();
                            Mrecommend.Director = reader["Director"].ToString();
                            Mrecommend.Year = reader["Year"].ToString();
                            Mrecommend.Genre = reader["Genre"].ToString();
                            Mrecommend.Rated = reader["Rated"].ToString();
                            Mrecommend.Language = reader["Language"].ToString();
                            Mrecommend.Plot = reader["Plot"].ToString();
                            Mrecommend.Runtime = reader["Runtime"].ToString();
                            if (!reader.IsDBNull(reader.GetOrdinal("Poster")))
                            {
                                Mrecommend.Poster = (byte[])reader["Poster"];
                            }
                            else
                            {
                                Mrecommend.Poster = null;
                            }

                            Mrecommend.ImdbRating = reader["ImdbRating"].ToString();
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
