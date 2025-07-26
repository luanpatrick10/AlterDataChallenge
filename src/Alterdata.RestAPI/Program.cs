using Alterdata.Application;
using Alterdata.Infra.Persistence;
using Alterdata.RestAPI.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Alterdata.Infra.DI;
using Shared.Mediator;
using Alterdata.RestAPI;

var builder = WebApplication.CreateBuilder(args);

var tokens = new Dictionary<string, string>
{
    { "Manager", "Manager" },
    { "Member", "Member" }
};
builder.Services.AddSingleton(tokens);

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
builder.Services.AddSwaggerGen(options =>
{
    SwaggerTokenConfig.AddTokenHeader(options);
});
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

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<SimpleAuthMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.ConfigObject.AdditionalItems["persistAuthorization"] = true;
    });
}

app.AddProjectEndpoints();
app.Run();