using Cozy_Chatter.Data;
using Cozy_Chatter.Middleware;
using Cozy_Chatter.Repositories;
using Cozy_Chatter.Services;
using Cozy_Chatter.Services.Interfaces;
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
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
            builder.Services.AddSingleton<ITokenService, TokenService>();

            var jwtKey = Environment.GetEnvironmentVariable("Jwt__Key")
                ?? builder.Configuration["Jwt:Key"]
                ?? throw new Exception("JWT Key not configured");
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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey))
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

            builder.Services.AddScoped<IChatRepository, ChatRepository>();
            builder.Services.AddScoped<ICredentialRepository, CredentialRepository>();
            builder.Services.AddScoped<IEmojiRepository, EmojiRepository>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddScoped<IProfilePictureRepository, ProfilePictureRepository>();
            builder.Services.AddScoped<ISMPostRepository ,SMPostRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IChatService, ChatService>();
            builder.Services.AddScoped<ISMPostService, SMPostService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddCors(option =>
            {
                option.AddPolicy("AllowClient", builder =>
                {
                    builder
                        .WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
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
            else app.UseExceptionHandler("/error");

            app.UseHttpsRedirection();

            app.UseCors("AllowClient");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}