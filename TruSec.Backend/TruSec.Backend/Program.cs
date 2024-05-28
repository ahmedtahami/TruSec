using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TruSec.Backend.Hubs;
using TruSec.BLL.Configurations;
using TruSec.BLL.Interfaces;
using TruSec.BLL.Services;
using TruSec.DAL.DbContexts;
using TruSec.DAL.Entities;
using TruSec.DAL.Repositories;
using TruSec.DAL.Repositories.TruSec.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var migrationAssembly = typeof(ApplicationDbContext).Assembly.FullName;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.MigrationsAssembly(migrationAssembly)));


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ITruckService, TruckService>();
builder.Services.AddScoped<ITruckDataLogService, TruckDataLogService>();
builder.Services.AddScoped<ITruckSecretService, TruckSecretService>();
builder.Services.AddScoped<IUserCompanyService, UserCompanyService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
})
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed((hosts) => true));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CORSPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<ExpressionHub>("/expressionsHub");

app.Run();
