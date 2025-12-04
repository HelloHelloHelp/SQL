using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
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
                            Mrecommend.ID = reader.GetInt32("ID");
                            Mrecommend.Titel = reader.GetString("Titel");
                            Mrecommend.Date = reader.GetInt32("Date");
                            Mrecommend.Genre = reader.GetString("Genre");
                            Mrecommend.Restricting_age = reader.GetInt32("Restricting_age");
                            Mrecommend.Platform = reader.GetString("Platform");
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
