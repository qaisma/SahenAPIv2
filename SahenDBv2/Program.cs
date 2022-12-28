using Microsoft.EntityFrameworkCore;
using SahenDBv2.DB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SahenDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("sahenDbConstr"))
);

builder.Services.AddMvc()
             .AddJsonOptions(opt =>
             {
                 opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                 opt.JsonSerializerOptions.MaxDepth = 10;
             });

//todo: specify a url for ui
//https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-6.0
var allOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*");
                      });
});


var app = builder.Build();

app.UseCors(allOrigins);

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
