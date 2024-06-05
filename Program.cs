using Microsoft.AspNetCore.Mvc.Versioning;
using ShoeSalesAPI.Models;
using ShoeSalesAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ShoeStoreDatabaseSettings>(
    builder.Configuration.GetSection("MongoWk1"));

builder.Services.AddSingleton<ShoeService>();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning(
    options =>
    {
        options.ReportApiVersions = true;
        options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;

        options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");

    });
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:7085");
        builder.WithHeaders("X-API-Version");
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.Run();
