using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Data;
using Server.Infrastructure.Data.UnitOfWork;
using Server.Services;
using Server.Services.Interfaces.Interfaces;
using Server.Services.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            // requires
            // using Microsoft.AspNetCore.Identity.UI.Services;
            // using WebPWrecover.Services;
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IApiUsersService, ApiUsersService>();
            services.AddTransient<IBaseInfoService, BaseInfoService>();
            services.AddTransient<ICaloricInfoService, CaloricInfoService>();

            services.AddTransient<IIngredientsBaseInfoService, IngredientsBaseInfoService>();
            services.AddTransient<ICurrentIngredientsService, CurrentIngredientsService>();
            services.AddTransient<IForbiddenIngredientsService, ForbiddenIngredientsService>();
            services.AddTransient<IIngredientsStatisticService, IngredientsStatisticService>();
            services.AddTransient<IIngredientsExpensesService, IngredientsExpensesService>();

            services.AddTransient<IProductsBaseInfoService, ProductsBaseInfoService>();
            services.AddTransient<ICurrentProductsService, CurrentProductsService>();
            services.AddTransient<IForbiddenProductsService, ForbiddenProductsService>();
            services.AddTransient<IProductsStatisticService, ProductsStatisticService>();
            services.AddTransient<IProductsExpensesService, ProductsExpensesService>();

            services.AddTransient<IRecipesBaseInfoService, RecipesBaseInfoService>();
            services.AddTransient<IFavouriteRecipesService, FavouriteRecipesService>();

            services.AddRazorPages();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            options.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
