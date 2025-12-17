using HtmlAgilityPack;
using Microsoft.Data.SqlClient;
using MoviesAPI.Models;
using MoviesAPI.Repository;
using MoviesAPI.Service;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Linq;


var builder = WebApplication.CreateBuilder(args);
LFM movie = null;
LFS serie = null;


byte[]? posterBytes = null;

List<LFS> seriesList = new();
List<LFM> moviesList = new();

using (var httpClient = new HttpClient())
{
    string[] titles = new string[]
  {
     "Shawshank Redemption", "The Godfather", "Inception", "Pulp Fiction", "Fight Club", "Interstellar", "The Dark Knight", "No Country for Old Men",
     "There Will Be Blood", "The Wolf of Wall Street", "Forrest Gump", "The Lord of the Rings: The Fellowship of the Ring", "Scarface", "Goodfellas", "World War Z", "The Matrix",
     "Blade Runner 2049", "Cast Away", "Gladiator", "Schindler's List", "The Queen", "Inside Man", "Parasite", "Rogue One: A Star Wars Story", "Children of Men",
     "Se7en", "The Departed", "Zodiac", "American Psycho", "The Green Mile", "The Untouchables", "Ex Machina", "Eternal Sunshine of the Spotless Mind","The Social Network",
"The Hunger Games", "Harry Potter and the Sorcerer's Stone", "Superbad", "Step Brothers", "The Hangover", "Crazy Rich Asians",
"Back to the Future", "Toy Story", "Jurassic Park", "Finding Nemo", "Spirited Away", "Her", "Django Unchained",
"The Silence of the Lambs", "The Prestige", "The Bourne Identity", "The Grand Budapest Hotel", "Die Hard", "Once Upon a Time in Hollywood", "Alien", "Saving Private Ryan", "Mystic River",
"Donnie Darko", "Heat", "Arrival", "Akira", "Your Name", "Spirited Away", "Dragon Ball Super: Broly", "A Silent Voice",
"The Lion King", "Shrek", "Wall-E", "Hot Fuzz", "City of God", "Whiplash",
"Birdman", "La La Land", "Joker", "Knives Out", "Dune", "Ready Player One", "Palm Springs", "The Edge of Seventeen",
"Logan", "Captain Marvel", "Spider-Man: No Way Home", "Black Panther", "Ant-Man and the Wasp", "Doctor Strange in the Multiverse of Madness"
};
    var random = new Random();
    var shuffledTitles = titles.OrderBy(_ => random.Next()).ToList();

    foreach (var title in shuffledTitles)
    {
        var endpoint = new Uri($"https://www.omdbapi.com/?apikey=3f124dfe&t={Uri.EscapeDataString(title)}&type=movie");
        HttpResponseMessage result;
        try
        {
            result = await httpClient.GetAsync(endpoint);
        }
        catch
        {
          
            continue;
        }

        if (!result.IsSuccessStatusCode)
            continue;

        var content = await result.Content.ReadAsStringAsync();
        movie = JsonConvert.DeserializeObject<LFM>(content);
        if (movie == null || string.IsNullOrEmpty(movie.Title))
            continue;


        if (!string.IsNullOrEmpty(movie.OmdbPosterUrl) && movie.OmdbPosterUrl != "N/A")
        {
            try
            {
                movie.Poster = await httpClient.GetByteArrayAsync(movie.OmdbPosterUrl);
                movie.PosterUrl = movie.OmdbPosterUrl;
            }
            catch
            {

            }
        }

        moviesList.Add(movie);

        // small delay to reduce chance of rate limiting (adjust as needed)
        await Task.Delay(200);
    }
}

using (SqlConnection conn = new SqlConnection(
    "Data Source=localhost;Initial Catalog=LFMAS;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"))
{
    conn.Open();

    string query = @"
       INSERT INTO LFM (Title, ImdbID, Director, Year, Genre, Poster, PosterUrl, Plot, Rated, Language, Runtime, ImdbRating)
        VALUES (@Title ,  @ImdbID, @Director, @Year, @Genre, @Poster, @PosterUrl, @Plot,@Rated, @Language, @Runtime, @ImdbRating)";

    using (SqlCommand cmd = new SqlCommand(query, conn))
    {
        cmd.Parameters.AddWithValue("@Title", movie.Title ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@ImdbID", movie.ImdbID ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Director", movie.Director ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Year", movie.Year ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Genre", movie.Genre ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Poster", (object?)posterBytes ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@PosterUrl", movie.PosterUrl ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Plot", movie.Plot ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Rated", movie.Rated ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Language", movie.Language ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Runtime", movie.Runtime ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@ImdbRating", movie.ImdbRating ?? (object)DBNull.Value);

        foreach (var m in moviesList)
        {
            cmd.Parameters["@Title"].Value = m.Title ?? (object)DBNull.Value;
            cmd.Parameters["@ImdbID"].Value = m.ImdbID ?? (object)DBNull.Value;
            cmd.Parameters["@Director"].Value = m.Director ?? (object)DBNull.Value;
            cmd.Parameters["@Year"].Value = m.Year ?? (object)DBNull.Value;
            cmd.Parameters["@Genre"].Value = m.Genre ?? (object)DBNull.Value;
            cmd.Parameters["@Poster"].Value = (object?)m.Poster ?? DBNull.Value;
            cmd.Parameters["@PosterUrl"].Value = m.PosterUrl ?? (object)DBNull.Value;
            cmd.Parameters["@Plot"].Value = m.Plot ?? (object)DBNull.Value;
            cmd.Parameters["@Rated"].Value = m.Rated ?? (object)DBNull.Value;
            cmd.Parameters["@Language"].Value = m.Language ?? (object)DBNull.Value;
            cmd.Parameters["@Runtime"].Value = m.Runtime ?? (object)DBNull.Value;
            cmd.Parameters["@ImdbRating"].Value = m.ImdbRating ?? (object)DBNull.Value;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Insert failed for '{m.Title}': {ex.Message}");

            }
        }
    }
}


using (var httpClient = new HttpClient())
{
    string[] titles = new string[]
     {
       "Breaking Bad", "Game of Thrones", "Stranger Things", "The Wire", "The Sopranos", "Dark", "The Office", "Better Call Saul",
       "Ozark", "The Boys", "Friends", "House of the Dragon", "Narcos", "Peaky Blinders", "The Walking Dead", "Black Mirror",
       "Westworld", "Lost", "Vikings", "Chernobyl", "The Crown", "Money Heist", "Squid Game", "The Mandalorian", "The Last of Us",
       "True Detective", "Fargo", "Mindhunter", "Dexter", "Prison Break", "Boardwalk Empire", "Mr. Robot", "Severance","Succession",
       "The Handmaid's Tale", "The Witcher", "Brooklyn Nine-Nine", "Parks and Recreation", "How I Met Your Mother", "Modern Family",
       "The Big Bang Theory", "Rick and Morty", "South Park", "BoJack Horseman", "Arcane", "Better Things", "Yellowstone",
       "House", "Sherlock", "Luther", "The Umbrella Academy", "24", "Mad Men", "The X-Files", "Band of Brothers", "The Night Of",
       "The Leftovers", "The Americans", "The Expanse", "Naruto", "Attack on Titan", "One Piece", "Dragon Ball Z", "Death Note",
       "Fullmetal Alchemist: Brotherhood", "The Simpsons", "Family Guy", "Futurama", "The Office (UK)", "Top Boy", "The Bear",
       "Barry", "Euphoria", "Wednesday", "Only Murders in the Building", "Reacher", "Foundation", "Upload", "Sex Education",
       "The Punisher", "Jessica Jones", "Daredevil", "Luke Cage", "The Falcon and the Winter Soldier", "WandaVision"
   };

  
    var random = new Random();
    var shuffledTitles = titles.OrderBy(_ => random.Next()).ToList();

    foreach (var title in shuffledTitles)
    {
        var endpoint = new Uri($"https://www.omdbapi.com/?apikey=3f124dfe&t={Uri.EscapeDataString(title)}&type=series");
        HttpResponseMessage result;
        try
        {
            result = await httpClient.GetAsync(endpoint);
        }
        catch
        {
           
            continue;
        }

        if (!result.IsSuccessStatusCode)
            continue;

        var content = await result.Content.ReadAsStringAsync();
         serie = JsonConvert.DeserializeObject<LFS>(content);
        if (serie == null || string.IsNullOrEmpty(serie.Title))
            continue;

      
        if (!string.IsNullOrEmpty(serie.OmdbPosterUrl) && serie.OmdbPosterUrl != "N/A")
        {
            try
            {
                serie.Poster = await httpClient.GetByteArrayAsync(serie.OmdbPosterUrl);
                serie.PosterUrl = serie.OmdbPosterUrl;
            }
            catch
            {
                
            }
        }

        seriesList.Add(serie);

        // small delay to reduce chance of rate limiting (adjust as needed)
        await Task.Delay(200);
    }
}



using (SqlConnection conn = new SqlConnection(
    "Data Source=localhost;Initial Catalog=LFMAS;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"))
{
    conn.Open();

    string query = @"
       INSERT INTO LFS (Title, ImdbID, Director, Year, Genre, TotalSeasons, Poster, PosterUrl, Plot, Rated, Language, Runtime, ImdbRating)
        VALUES (@Title , @ImdbID, @Director, @Year, @Genre, @TotalSeasons, @Poster, @PosterUrl, @Plot, @Rated, @Language, @Runtime, @ImdbRating)";

    using (SqlCommand cmd = new SqlCommand(query, conn))
    {
        cmd.Parameters.AddWithValue("@Title", serie.Title ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@ImdbID", serie.ImdbID ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Director", serie.Director ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Year", serie.Year ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Genre", serie.Genre ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@TotalSeasons", serie.TotalSeasons ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Poster", (object?)posterBytes ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@PosterUrl", serie.PosterUrl ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Plot", serie.Plot ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Rated", serie.Rated ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Language", serie.Language ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Runtime", serie.Runtime ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@ImdbRating", serie.ImdbRating ?? (object)DBNull.Value);

        foreach (var s in seriesList)
        {
            cmd.Parameters["@Title"].Value = s.Title ?? (object)DBNull.Value;
            cmd.Parameters["@ImdbID"].Value = s.ImdbID ?? (object)DBNull.Value;
            cmd.Parameters["@Director"].Value = s.Director ?? (object)DBNull.Value;
            cmd.Parameters["@Year"].Value = s.Year ?? (object)DBNull.Value;
            cmd.Parameters["@Genre"].Value = s.Genre ?? (object)DBNull.Value;
            cmd.Parameters["@TotalSeasons"].Value = s.TotalSeasons ?? (object)DBNull.Value;
            cmd.Parameters["@Poster"].Value = (object?)s.Poster ?? DBNull.Value;
            cmd.Parameters["@PosterUrl"].Value = s.PosterUrl ?? (object)DBNull.Value;
            cmd.Parameters["@Plot"].Value = s.Plot ?? (object)DBNull.Value;
            cmd.Parameters["@Rated"].Value = s.Rated ?? (object)DBNull.Value;
            cmd.Parameters["@Language"].Value = s.Language ?? (object)DBNull.Value;
            cmd.Parameters["@Runtime"].Value = s.Runtime ?? (object)DBNull.Value;
            cmd.Parameters["@ImdbRating"].Value = s.ImdbRating ?? (object)DBNull.Value;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Insert failed for '{s.Title}': {ex.Message}");
               
            }
        }
    }
}

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Repositories and Services registrations
builder.Services.AddScoped<ILFMRepository, LFMRepository>();
builder.Services.AddScoped<ILFSRepository, LFSRepository>();
builder.Services.AddScoped<ImovieRepository, movieRepository>();
builder.Services.AddScoped<IserieRepository, serieRepository>();
builder.Services.AddHttpClient<ILFMRepository, LFMRepository>();

builder.Services.AddScoped<ILFMService, LFMService>();
builder.Services.AddScoped<ILFSService, LFSService>();
builder.Services.AddScoped<ImovieService, movieService>();
builder.Services.AddScoped<IserieService, serieService>();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS before other middlewares
app.UseCors();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
