using MoviesAPI.Models;
using MoviesAPI.Repository;
using MoviesAPI.Service;


var builder = WebApplication.CreateBuilder(args);

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
