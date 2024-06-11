using Mappy.Configurations;
using Mappy.Services;
using Mappy.Repos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<AccidentService>();

builder.Services.AddDbContext<DefaultDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

// JWT Configuration
builder.Services.AddJwt(builder.Configuration);

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
