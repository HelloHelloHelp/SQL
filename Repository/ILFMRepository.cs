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
       Task<List<Models.LFM>> GetLFMFromApiAsync();
    }
    public class LFMRepository : ILFMRepository
    {
        private readonly HttpClient _httpClient;
        readonly Task<List<Models.LFM>> GetLFMFromApiAsync();
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
        /*Fetch async Task<List<Models.LFM>> GetLFMFromApiAsync()
        {
            List<Models.LFM> lfmList = new List<Models.LFM>();
            HttpResponseMessage response = await _httpClient.GetAsync("https://api.example.com/lfm");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<List<Models.LFM>>();
                lfmList = data;
            }
            return lfmList;
        }*/


    }
}
