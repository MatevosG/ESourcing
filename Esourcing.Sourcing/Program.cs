using Esourcing.Sourcing.Data;
using Esourcing.Sourcing.Data.Interface;
using Esourcing.Sourcing.Repositories;
using Esourcing.Sourcing.Repositories.Interfaces;
using Esourcing.Sourcing.Settings;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<SourcingDatabaseSettings>(builder.Configuration.GetSection(nameof(SourcingDatabaseSettings)));

builder.Services.AddSingleton<ISourcingDatabaseSettings>(sp =>
               sp.GetRequiredService<IOptions<SourcingDatabaseSettings>>().Value);



#region Project Dependencies
builder.Services.AddTransient<ISourcingContext, SourcingContext>();
builder.Services.AddTransient<IAuctionRepository, AuctionRepository>();
builder.Services.AddTransient<IBidRepository, BidRepository>();

#region Swagger Dependencies
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "ESourcing.Sourcing",
            Version = "v1"
        }
        );
});
#endregion

//builder.Services.AddAutoMapper(typeof(Startup));
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sourcing API V1");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
