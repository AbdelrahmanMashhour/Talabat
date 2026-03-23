using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.API.DTOs.Helpers;
using Talabat.API.Errors;
using Talabat.API.Extensions;
using Talabat.API.Middlewares;
using Talabat.Core.Repositories.Contracts;
using Talabat.Repository;
using Talabat.Repository.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerServices();

builder.Services.AddDbContext<AppDbContext>(option=>option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddApplicationServices();

#region Ask CLR To Create Object Explicitly
    var app = builder.Build();
        var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var _dbContext = serviceProvider.GetRequiredService<AppDbContext>();
        await _dbContext.Database.MigrateAsync();
        await StoreContextSeed.SeedAsync(_dbContext);
#endregion


// Add the custom exception handling middleware to the pipeline
app.UseMiddleware<ExceptionMiddleware>();

// Handle status code pages (like 404) by redirecting to a custom error page
app.UseStatusCodePagesWithRedirects("/errors/{0}");

app.UseSwaggerServices();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();
app.Run();
