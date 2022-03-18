using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RnAds.Bll.Options;
using RnAds.Bll.Service;
using RnAds.Core.Interfaces;
using RnAds.DAL;
using RnAds.DAL.Repositories;
using RnAds.InterFaces.Interfaces;
using RnAds.WEB.Infrastructure.Cors;
using RnAds.WEB.Models;
using System.Threading.Tasks;

namespace RnAds.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAdService, AdService>();
            services.AddScoped<IAdRepository, AdRepository>();
            services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<IUserAdService, UserAdService>();
            services.AddScoped<IExcelService, ExcelService>();
            services.AddScoped<IPictureRepository, PictureRepository>();
            services.AddScoped<IPictureService, PictureService>();
            services.AddScoped<IAvatarRepository, AvatarRepository>();
            services.AddScoped<IAvatarService, AvatarService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAccountRepository,AccountRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IDialogRepository, DialogRepository>();
            services.AddScoped<IDialogService, DialogService>();
            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

            services.AddDbContext<RnAdsContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews()
              .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
              .AddRazorRuntimeCompilation();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.RequireHttpsMetadata = true;
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                            // укзывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = true,
                            // строка, представляющая издателя
                            ValidIssuer = AuthOptions.ISSUER,

                            // будет ли валидироваться потребитель токена
                            ValidateAudience = true,
                            // установка потребителя токена
                            ValidAudience = AuthOptions.AUDIENCE,
                            // будет ли валидироваться время существования
                            ValidateLifetime = true,

                            // установка ключа безопасности
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true,
                       };
                       options.Events = new JwtBearerEvents
                       {
                           OnMessageReceived = context =>
                           {
                               var accessToken = context.Request.Query["access_token"];

                               // если запрос направлен хабу
                               var path = context.HttpContext.Request.Path;
                               if (!string.IsNullOrEmpty(accessToken) &&
                                   (path.StartsWithSegments("/chat")))
                               {
                                   // получаем токен из строки запроса
                                   context.Token = accessToken;
                               }
                               return Task.CompletedTask;
                           }
                       };
                   });
            services.ConfigureCors();
            services.AddSignalR();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            ///для фронта
            app.UseCors(CorsConfiguration.CorsPolicy);
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<ChatHub>("/chat");
            });

        }
    }
}
