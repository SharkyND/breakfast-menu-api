using BuberBreakfast.Db;
using BuberBreakfast.Middleware;
using BuberBreakfast.Services.Breakfasts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
  // Add services to the container.
  builder.Services.AddControllers();

  builder.Services.AddDbContext<BreakfastDbContext>(options =>
  {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlServerOptionsAction: sqlOptions =>
    {
      sqlOptions.EnableRetryOnFailure();
    });
  });
  builder.Services.AddScoped<IBreakfastService, BreakfastService>();
  // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen();
}
var app = builder.Build();
{
  // app.UseExceptionHandler("/error");
  app.UseMiddleware<ErrorHandlingMiddleware>();
  // Configure the HTTP request pipeline.
  if (app.Environment.IsDevelopment())
  {
    app.UseSwagger();
    app.UseSwaggerUI();
  }
  app.UseHttpsRedirection();
  // app.UseAuthorization();
  app.MapControllers();
  app.Run();
}