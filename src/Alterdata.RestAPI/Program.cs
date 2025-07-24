using Alterdata.Infra.Persistence;
using Alterdata.RestAPI.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Alterdata.Infra.DI;
using Shared.Mediator;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    var dbPath = Path.Combine(AppContext.BaseDirectory, "dev-database.db");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite($"Data Source={dbPath}"));
}
else
{
    var connection = new SqliteConnection("DataSource=:memory:");
    connection.Open();
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(connection));
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddScoped<AppMediator>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();    
    db.Database.EnsureCreated();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddProjectEndpoints();
app.AddGetProjectEndpoints();
app.Run();