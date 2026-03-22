using Microsoft.EntityFrameworkCore;
using Talabat.API.DTOs.Helpers;
using Talabat.Core.Repositories.Contracts;
using Talabat.Repository;
using Talabat.Repository.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(option=>option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddAutoMapper(typeof(MappingProfile));
//builder.Services.AddAutoMapper(m=>m.AddProfile(new MappingProfile));

#region Ask CLR To Create Object Explicitly
var app = builder.Build();
    var scope = app.Services.CreateScope();
    var serviceProvider = scope.ServiceProvider;
    var _dbContext = serviceProvider.GetRequiredService<AppDbContext>();
    await _dbContext.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(_dbContext);
#endregion
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
