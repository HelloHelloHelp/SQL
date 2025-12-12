using System.Net.Http;
using Azure;
using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using MoviesAPI.Models;
using MoviesAPI.Repository;
using MoviesAPI.Service;
using Newtonsoft.Json;


var builder = WebApplication.CreateBuilder(args);

LFM movie = null;
LFS serie = null;
byte[]? posterBytes = null; 

using (var httpClient = new HttpClient())
{
    var endpoint = new Uri("https://www.omdbapi.com/?apikey=3f124dfe&t=Mulan");
    var result = await httpClient.GetAsync(endpoint);

    if (!result.IsSuccessStatusCode)
        throw new Exception("Failed to fetch movie data.");

    var content = await result.Content.ReadAsStringAsync();
    movie = JsonConvert.DeserializeObject<LFM>(content)
            ?? throw new Exception("Movie API returned null.");


    if (!string.IsNullOrEmpty(movie.OmdbPosterUrl) && movie.OmdbPosterUrl != "N/A")
    {
        try
        {
            posterBytes = await httpClient.GetByteArrayAsync(movie.OmdbPosterUrl);

            movie.PosterUrl = movie.OmdbPosterUrl;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Poster download failed: " + ex.Message);
        }
    }


    movie.Poster = posterBytes;
}

using (SqlConnection conn = new SqlConnection(
    "Data Source=localhost;Initial Catalog=LFMAS;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"))
{
    conn.Open();

    string query = @"
       INSERT INTO LFM (Title, Year, Genre, Poster, PosterUrl, ImdbRating)
        VALUES (@Title , @Year, @Genre, @Poster, @PosterUrl, @ImdbRating)";

    using (SqlCommand cmd = new SqlCommand(query, conn))
    {
        cmd.Parameters.AddWithValue("@Title", movie.Title ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Year", movie.Year ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Genre", movie.Genre ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Poster", (object?)posterBytes ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@PosterUrl", movie.PosterUrl ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@ImdbRating", movie.ImdbRating ?? (object)DBNull.Value);

        cmd.ExecuteNonQuery();
    }
}



using (var httpClient = new HttpClient())
{
    var endpoint = new Uri("https://www.omdbapi.com/?apikey=3f124dfe&t=Pluribus");
    var result = await httpClient.GetAsync(endpoint);

    if (!result.IsSuccessStatusCode)
        throw new Exception("Failed to fetch movie data.");

    var content = await result.Content.ReadAsStringAsync();
    serie = JsonConvert.DeserializeObject<LFS>(content)
            ?? throw new Exception("Movie API returned null.");


    if (!string.IsNullOrEmpty(serie.OmdbPosterUrl) && serie.OmdbPosterUrl != "N/A")
    {
        try
        {
            posterBytes = await httpClient.GetByteArrayAsync(serie.OmdbPosterUrl);

            serie.PosterUrl = serie.OmdbPosterUrl;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Poster download failed: " + ex.Message);
        }
    }


    serie.Poster = posterBytes;
}

using (SqlConnection conn = new SqlConnection(
    "Data Source=localhost;Initial Catalog=LFMAS;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"))
{
    conn.Open();

    string query = @"
    INSERT INTO LFS (Title, Year, Genre, TotalSeasons, Poster, PosterUrl, ImdbRating)
    VALUES (@Title, @Year, @Genre, @TotalSeasons, @Poster, @PosterUrl, @ImdbRating)";
   


    using (SqlCommand cmd = new SqlCommand(query, conn))
    {
        cmd.Parameters.AddWithValue("@Title", serie.Title ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Year", serie.Year ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Genre", serie.Genre ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@TotalSeasons", serie.TotalSeasons ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Poster", (object?)posterBytes ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@PosterUrl", serie.PosterUrl ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@ImdbRating", serie.ImdbRating ?? (object)DBNull.Value);

        cmd.ExecuteNonQuery();
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
