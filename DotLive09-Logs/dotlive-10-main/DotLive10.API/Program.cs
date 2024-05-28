using System.Data;
using DotLive10.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddApplicationInsightsTelemetry();

var connectionString = builder.Configuration.GetConnectionString("DotLive10");

builder.Services.AddDbContext<DotLive10DbContext>(o => o.UseSqlServer(connectionString));

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    Serilog.Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.MSSqlServer(connectionString,
        sinkOptions: new MSSqlServerSinkOptions
        {
            AutoCreateSqlTable = true,
            TableName = "Logs"
        },
        columnOptions: new ColumnOptions
        {
            AdditionalColumns = new List<SqlColumn>
            {
                new SqlColumn { DataType = SqlDbType.VarChar, ColumnName = "Payload", DataLength = -1, AllowNull = true },
                new SqlColumn { DataType = SqlDbType.VarChar, ColumnName = "CreatedBy", DataLength = 100, AllowNull = true }
            }
        })
        .CreateLogger();
}).UseSerilog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

