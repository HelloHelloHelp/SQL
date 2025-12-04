using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
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
                            Srecommend.ID = reader.GetInt32("ID");
                            Srecommend.Titel = reader.GetString("Titel");
                            Srecommend.Date = reader.GetInt32("Date");
                            Srecommend.Genre = reader.GetString("Genre");
                            Srecommend.Restricting_age = reader.GetInt32("Restricting_age");
                            Srecommend.Platform = reader.GetString("Platform");
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
