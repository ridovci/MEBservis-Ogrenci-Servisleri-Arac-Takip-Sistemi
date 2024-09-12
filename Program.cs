using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MEBservis.Application.Mapping;
using MEBservis.Application.Services;
using MEBservis.Domain.Interfaces;
using MEBservis.Infrastructure.Data;
using MEBservis.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// AutoMapper'ý ekleyin
builder.Services.AddAutoMapper(typeof(MappingProfile)); // Profil türünü belirtin

// Hizmetleri konteynýra ekle
builder.Services.AddControllers();

// Baðýmlýlýk Enjeksiyonu
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITrackingSessionRepository, TrackingSessionRepository>();
builder.Services.AddScoped<ITrackingSessionService, TrackingSessionService>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

// Sorun: Çalýþma zamanýnda async eki kaldýrýlýr. Çözüm: Bu kod bloðunu ekledim.
builder.Services.AddControllers(
    options => {
        options.SuppressAsyncSuffixInActionNames = false;
    }
);

// Veritabaný baðlamýný yapýlandýrýn
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    var connStr = builder.Configuration.GetConnectionString("ApplicationConnection");
    opt.UseSqlServer(connStr);
});

// Swagger jeneratörünü kaydet, bir veya daha fazla Swagger belgesi tanýmla
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = "MEBservis.xml"; // XML dosyanýzýn adý
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MEBservis API", Version = "v1" });
});

// JWT doðrulama ve kimlik doðrulama hizmetlerini ekleyin
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        // JWT'nin verileceði adres
        var issuer = builder.Configuration["Jwt:Issuer"];
        // JWT imzalama anahtarý
        var key = builder.Configuration["Jwt:Key"];
        // Ýmzalama anahtarýný oluþturun
        var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        // JWT doðrulama parametrelerini ayarlayýn
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            // Geçerli issuer
            ValidIssuer = issuer,
            // Ýmzalama anahtarý
            IssuerSigningKey = issuerSigningKey,
            // Token yaþam süresi kontrolü
            ValidateLifetime = true,
            // Issuer kontrolü
            ValidateIssuer = true,
            // Ýmzalama anahtarý kontrolü
            ValidateIssuerSigningKey = true,
            // Audience kontrolü (burada devre dýþý)
            ValidateAudience = false,
            // Rol için claim tipi
            RoleClaimType = System.Security.Claims.ClaimTypes.Role,
            // Ýsim için claim tipi
            NameClaimType = System.Security.Claims.ClaimTypes.Name
        };

        // JWT olaylarýný yapýlandýrýn
        opt.Events = new JwtBearerEvents
        {
            // Kimlik doðrulama hatalarýný ele alma
            OnChallenge = context =>
            {
                // Ýstemciye hatalý token gönderildiðinde Authorization baþlýðýný döndür
                context.Response.Headers["Authorization"] = context.Request.Headers["Authorization"];
                return Task.CompletedTask;
            }
        };
    });

// Uygulama yapýlandýrmasýný oluþturun
var app = builder.Build();

// HTTP istek boru hattýný yapýlandýr
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MEBservis API V1");
        c.RoutePrefix = string.Empty; // Swagger UI kök URL'de yer alacak
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // API denetleyicilerini yönlendir

app.Run(); // Uygulamayý çalýþtýr
