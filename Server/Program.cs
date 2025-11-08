using Cozy_Chatter.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cozy_Chatter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();

            builder.Services.AddDbContext<ChatterContext>(options =>
                options.UseSqlServer("Server=ANDREY_PC\\SQLEXPRESS;Database=chatter;User Id=ccAdmin;Password=cozyAdmin9357;TrustServerCertificate=True;"));

            builder.Services.AddScoped<SMPostRepository>();
            builder.Services.AddScoped<ChatRepository>();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<EmojiRepository>();


            builder.Services.AddCors(option =>
            {
                option.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:5173");
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
            });

            builder.Services.AddOpenApi();
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}