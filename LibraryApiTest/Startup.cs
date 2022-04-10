using Api.Helpers;
using Api.Interfaces;
using Api.Services;
using Core.Entities;
using Infrastructure.Data.AppDbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibraryApiTest;

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        // Application Database Context
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly("Infrastructure")
            );
        });

        services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"])),
                       ValidIssuer = Configuration["Token:Issuer"],
                       ValidateIssuer = true,
                       ValidateAudience = false
                   };
               });

        // AutoMapper
        services.AddAutoMapper(typeof(MappingProfiles));

        // Repositories
        services.AddTransient<IAuthorsRepository, AuthorsRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
