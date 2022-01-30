using CoreLayer.IServices;
using CoreLayer.Services;
using CoreLayer.Services.FileManager;
using DataLayer.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebCore
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
			services.AddRazorPages()
				.AddRazorRuntimeCompilation();
			services.AddRazorPages();
			services.AddControllersWithViews();
			services.AddTransient<IGroupProductService, GroupProductService>();
			services.AddTransient<IProductCommentService, ProductCommentService>();
			services.AddTransient<IProductService, ProductService>();
			services.AddScoped<IUserService, UserService>();
			services.AddTransient<IFileManager, FileManager>();
			services.AddTransient<ICompanyService, CompanyService>();
			services.AddTransient<IBannerService, BannerService>();
			services.AddScoped<IRecaptcha, Recaptcha>();
			services.AddScoped<IAmazing_SuggestService, Amazing_SuggestService>();
			services.AddHttpContextAccessor();
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			//services.AddDbContext<ShopContext>(option =>
			//{
			//	option.UseSqlServer(Configuration.GetConnectionString("Default"));
			//}); 
			services.AddDbContext<ShopContext>(option =>
			 option.UseSqlServer(Configuration.GetConnectionString("Default")),
			 ServiceLifetime.Transient
			 );
			services.AddAuthentication(option =>
			{
				option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			}).AddCookie(option =>
			{
				option.LoginPath = "/Auth/Login";
				option.LogoutPath = "/Auth/Logout";
				option.AccessDeniedPath = "/Auth/AccessDenied";
				option.ExpireTimeSpan = TimeSpan.FromDays(30);
			});

			services.Configure<IdentityOptions>(option =>
			{
				//UserSetting

				//option.User.AllowedUserNameCharacters = "abcd123";
				//option.User.RequireUniqueEmail = true;

				//Password Setting
				//option.Password.RequireDigit = false;
				//option.Password.RequireLowercase = false;
				//option.Password.RequireNonAlphanumeric = false;//!@#$%^&*()_+
				//option.Password.RequireUppercase = false;
				//option.Password.RequiredLength = 8;
				//option.Password.RequiredUniqueChars = 1;

				//Lokout Setting
				option.Lockout.MaxFailedAccessAttempts = 3;
				//option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);

				////SignIn Setting
				//option.SignIn.RequireConfirmedAccount = false;
				//option.SignIn.RequireConfirmedEmail = false;
				//option.SignIn.RequireConfirmedPhoneNumber = false;

			});
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
				app.UseExceptionHandler("/404");
				app.UseHsts();
			}
			app.Use(async (context, next) =>
			{
				await next();
				if (context.Response.StatusCode == 404)
				{
					context.Request.Path = "/404";
					await next();
				}
			});
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "Default",
					pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
					);
				endpoints.MapControllerRoute(
					name: "Admin",
					pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});
		}
	}
}
