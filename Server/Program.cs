using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Date;
using Server.Mapping;
using Server.Models.DTOs;
using Server.Models.Entities;
using Server.Repo.interfaces;
using Server.Repo.repositories;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularFrontend",
        builder => builder
            .WithOrigins( "https://localhost:4200", "http://localhost:4200") 
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

builder.Services.AddHttpClient();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Listen(System.Net.IPAddress.Parse("192.168.1.5"), 5116); // for .net
});


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;

});
#region Db Connection  
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

#endregion
//Configure Hangfire to use SQL Server for job storage
builder.Services.AddHangfire(
    config => config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180).
    UseSimpleAssemblyNameTypeSerializer().
    UseRecommendedSerializerSettings().
    UseSqlServerStorage(connectionString)
    );
// Add HangfireServer

builder.Services.AddHangfireServer();


#region Identity settings
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
//{
//    options.Password.RequireDigit = false;
//options.Password.RequireLowercase = false;
//options.Password.RequireNonAlphanumeric = false;
//options.Password.RequireUppercase = false;
//options.Password.RequiredLength = 6;
//})
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();// AddDefaultTokenProviders is
                                                                                                                             // to make GeneratePasswordResetTokenAsync Work

// Configuration of Password when Register
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
});
var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>(); // object that holds data in Jwt Section
builder.Services.AddSingleton(jwtOptions); // register while dependency injection in UserController in primary Constructor
builder.Services.AddScoped<JwtRepository>();

#endregion


#region  Scoped Services

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IHouseRepository, HouseRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IHistoriesRepository, HistoryRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IAlarmRepository, AlarmRepository>();
builder.Services.AddScoped<IEmailRepo, EmailRepo>();
builder.Services.AddScoped<IDailySummaryRepo, DailySummaryRepo>();
#endregion


#region Authentication settings

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // The Default Authentication is JWT not Cookies
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // if unauthorized return to him "unauthorized" not "Not Found"
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; // for any other Schema
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.SaveToken = true; // save Token String in AuthenticationProperties in case you needed it 
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtOptions.Audience,
        ValidateLifetime = true,
        
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)) // transform SingingKey
                                                                                                   // from string to Byte
    };
});

#endregion


#region Authorization settings

#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(MappingConfig).Assembly);
#endregion
var app = builder.Build();


// 6. Use Hangfire Dashboard (optional)
app.UseHangfireDashboard("/hangfire");

// 7. Schedule Recurring Job (Daily Summary Report)
RecurringJob.AddOrUpdate<IDailySummaryRepo>(
    "SendDailySummaryEmail",
    x => x.GenerateAndSendSummariesAsync(),
    Cron.Daily // every day at 00:00
    );



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHttpsRedirection();
}


app.UseStaticFiles();


app.UseCors("AllowAngularFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

