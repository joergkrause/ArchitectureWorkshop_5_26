
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Reflection.Metadata.Ecma335;
using Workshop.DataAccessLayer;

namespace Workshop.RestServer
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      builder.Services.AddDbContext<WorkshopContext>(options =>
      {
#if DEBUG
        options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
#endif
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
      });
      builder.Services.AddManagers();

      builder.Services.AddControllers();
      // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
      builder.Services.AddOpenApi("v1", options =>
      {
        options.AddDocumentTransformer((doc, ctx, ct) =>
        {
          doc.Info.Title = "Workshop REST API";
          doc.Info.Version = "v1";
          doc.Info.Description = "A RESTful API for managing customers, products, and orders in the Workshop application.";

          return Task.CompletedTask;
        });
      });

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.MapOpenApi(); // json
        app.MapScalarApiReference();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();


      app.MapControllers();

      app.Run();
    }
  }
}
