var builder = WebApplication.CreateBuilder(args);

var wcors = "ReglasCors";

builder.Services.AddCors(option => option.AddPolicy(name: wcors,
    builder => { builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod(); }));



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(wcors);

app.UseAuthorization();

app.MapControllers();

app.Run();
