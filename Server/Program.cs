using Cozy_Chatter.Middleware;
using Cozy_Chatter.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace Cozy_Chatter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });

            var jwtKey = Environment.GetEnvironmentVariable("Jwt__Key")
             ?? builder.Configuration["Jwt:Key"]
             ?? throw new Exception("JWT Key not configured");
            var key = Encoding.ASCII.GetBytes(jwtKey);

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

            builder.Services.AddAuthorization();

            builder.Services.AddDbContext<ChatterContext>(options =>
                options.UseNpgsql(
                    Environment.GetEnvironmentVariable("PG_CONNECTION_STRING")
                    ?? builder.Configuration.GetConnectionString("DefaultConnection")
                    ?? throw new Exception("Connection string not configured")
                )
            );

            builder.Services.AddScoped<ChatRepository>();
            builder.Services.AddScoped<CredentialRepository>();
            builder.Services.AddScoped<EmojiRepository>();
            builder.Services.AddScoped<MessageRepository>();
            builder.Services.AddScoped<ProfilePictureRepository>();
            builder.Services.AddScoped<SMPostRepository>();
            builder.Services.AddScoped<UserRepository>();

            builder.Services.AddCors(option =>
            {
                option.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                          .WithMethods("GET", "POST", "PUT", "DELETE")
                          .WithHeaders("Content-Type", "Authorization");
                });
            });

            builder.Services.AddOpenApi();

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}