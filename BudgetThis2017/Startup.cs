using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BudgetThis2017.Models;
using BudgetThis2017.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace BudgetThis2017
{
  public class Startup
  {
    private IHostingEnvironment _env;
    public IConfiguration Configuration { get; }

    public Startup(IHostingEnvironment env, IConfiguration configuration)
    {
      _env = env;
      var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
      Configuration = builder.Build();
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton(Configuration);

      services.AddDbContext<BudgetThisContext>();

      services.AddScoped<IBudgetThisRepository, BudgetThisRepository>();

      services.AddTransient<BudgetThisContextSeedData>();

      services.AddIdentity<BudgetUser, IdentityRole>(config =>
      {
        config.User.RequireUniqueEmail = true;
        config.Password.RequiredLength = 6;

      })
      .AddEntityFrameworkStores<BudgetThisContext>()
      .AddDefaultTokenProviders();

      services.ConfigureApplicationCookie(config =>
      {
        config.LoginPath = "/Auth/Login";
        config.Events = new CookieAuthenticationEvents()
        {
          OnRedirectToLogin = async ctx =>
          {
            if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
            {
              ctx.Response.StatusCode = 401;
            }
            else
            {
              ctx.Response.Redirect((ctx.RedirectUri));
            }
            await Task.Yield();
          }
        };
      });

      services.AddLogging();

      services.AddMvc(config =>
      {
        //if (_env.IsProduction()) config.Filters.Add(new RequireHttpsAttribute());
      }
        )
        .AddJsonOptions(config => config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, BudgetThisContextSeedData seeder)
    {
      Mapper.Initialize(
             config =>
             {
               config.CreateMap<BillViewModel, Bill>();
               config.CreateMap<Bill, BillViewModel>()
                       .ForMember(x => x.DueDaySuffix, opt => opt.Ignore())
                       //.ForMember(x => x.Paycheck, opt => opt.UseValue(null))
                       .ForMember(x => x.CustomBillCount, opt => opt.Ignore());

               config.CreateMap<PaycheckViewModel, Paycheck>();
               config.CreateMap<Paycheck, PaycheckViewModel>()
                       .ForMember(x => x.PayCheck, opt => opt.UseValue(new PaycheckViewModel()))
                       .ForMember(x => x.Exists, opt => opt.Ignore());

               config.CreateMap<CustomBillViewModel, CustomBill>().ReverseMap();

               config.CreateMap<SettingsViewModel, Setting>().ReverseMap();
             }
           );

      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      //if (env.IsDevelopment())
      //{
      app.UseDeveloperExceptionPage();
      app.UseBrowserLink();
      //}
      //else
      //{
      //  app.UseExceptionHandler("/Home/Error");
      //}

      app.UseStaticFiles();

      app.UseAuthentication();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
          name: "Default",
          template: "{controller}/{Action}/{id?}",
          defaults: new { controller = "App", Action = "Index" }
        );
      });

      seeder.EnsuresSeedData().Wait();
    }
  }
}
