using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using WebApi.Data;
using WebApi.Models;
using WebApi.Models.Person;

// Dapper 
// https://dotnetcorecentral.com/blog/asp-net-core-web-api-application-with-dapper-part-2/
// 
//https://dotnetcorecentral.com/blog/asp-net-core-web-api-application-with-dapper-part-1/
// https://medium.com/@maheshi.gunarathne1994/web-api-using-asp-net-core-2-0-and-entity-framework-core-with-mssql-59d30f33ff64
// devops deploy cd ci
// https://github.com/catenn/ToDoList/wiki/06.-VSTS-Continuous-Integration-(Build-Definition)
// migration in devops
// https://abelsquidhead.com/index.php/2017/07/31/deploying-dbs-in-your-cicd-pipeline-with-ef-core-code-first/

// Registration
//  https://www.youtube.com/watch?v=9WVG-tXl7XA
// login with EF
// https://www.youtube.com/watch?v=s2zJ_g-iQvg
// Role identity
// https://www.youtube.com/watch?v=MGCC2zTb0t4&list=PLjC4UKOOcfDQvZ4DKz4DxpqyCzZ9yylmM&index=3
// Swagger
// https://fullstackmark.com/post/19/jwt-authentication-flow-with-refresh-tokens-in-aspnet-core-web-api
// https://www.youtube.com/watch?v=qlEZE1K5BI4
// https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.0&tabs=visual-studio

namespace WebApi
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
            //Inject AppSettings
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            services.AddDbContext<AuthenticationContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            }
            );

            services.AddDefaultIdentity<ApplicationUser>()
                  .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationContext>();

            services.AddScoped<IVolunteerRepository, VolunteerRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            }
            );

            //Jwt Authentication

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddTransient<IPersonProvider, PersonProvider>();
            services.AddAutoMapper(typeof(VolunteerRepository).Assembly);
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1",
                        new Info { Title = "GCI Volunteer Attendance API", Description = "Swagger Core API" });
                   // var xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + @"WebApi.xml";
                   // c.IncludeXmlComments(xmlPath);
                    //
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                    //

                    c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                    {
                        In = "header",
                        Description = "Please insert JWT with Bearer into field",
                        Name = "Authorization",
                        Type = "apiKey"
                    });

                    c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                          {
                            { "Bearer", new string[] { } }
                          });
                }
                );
            /*camel casing db 
             * 
            .AddJsonOptions(options => {
                 var resolver = options.SerializerSettings.ContractResolver;
                 if (resolver != null)
                     (resolver as DefaultContractResolver).NamingStrategy = null;
             });
             */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            AuthenticationContext idbcontext
            )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            /*
               app.UseCors(options =>
            options.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());
 
             */
            idbcontext.Database.Migrate();
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API");

                }
                );
        }
    }
}
