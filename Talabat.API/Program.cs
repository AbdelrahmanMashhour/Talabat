using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.API.DTOs.Helpers;
using Talabat.API.Errors;
using Talabat.API.Middlewares;
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
builder.Services.AddScoped<ExceptionMiddleware>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
//builder.Services.AddAutoMapper(m=>m.AddProfile(new MappingProfile));
#region Configuration Of BadRequest Response (this if found validation request error)
    builder.Services.Configure<ApiBehaviorOptions>(option =>
    {
        option.InvalidModelStateResponseFactory = (ActionContext context) =>
        {
            var errors = context.ModelState.Where(e => e.Value.Errors.Count > 0)
                .SelectMany(x => x.Value.Errors)
                .Select(x => x.ErrorMessage).ToArray();
            var errorResponse = new ApiValidationErrorResponse
            {
                Errors = errors
            };
            return new BadRequestObjectResult(errorResponse);
        };
    });
#endregion

#region Ask CLR To Create Object Explicitly
    var app = builder.Build();
        var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var _dbContext = serviceProvider.GetRequiredService<AppDbContext>();
        await _dbContext.Database.MigrateAsync();
        await StoreContextSeed.SeedAsync(_dbContext);
#endregion
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
//
app.UseAuthorization();

app.MapControllers();
app.Run();
