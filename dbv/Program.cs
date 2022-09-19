using dbv.DataAccess;
using dbv.Models;
using dbv.Services;
using dbv.Services.ScriptEnumerators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Setup dbv stuff
builder.Services.Configure<Settings>(options => builder.Configuration.GetSection(Settings.Section).Bind(options));

builder.Services.AddTransient<IConnectionFactory, ConnectionFactory>();
builder.Services.AddTransient<IApplyMigration, ApplyMigration>();
builder.Services.AddTransient<IScriptEnumerator, FileSystemScriptEnumerator>();
builder.Services.AddTransient<IScriptEnumerator, FileSystemScriptEnumerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var scriptsEnum = app.Services.GetRequiredService<IScriptEnumerator>();

await foreach (var script in scriptsEnum)
{
    Console.WriteLine(script.Path);
}


app.Run();
