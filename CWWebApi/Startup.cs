using Core.Models;
using CWWebApi.Data;
using CWWebApi.Models;
using CWWebApi.Security;
using CWWebApi.SwaggerConfig;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CWWebApi
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



            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
                ).AddJwtBearer(b =>
                {
                    b.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context => Task.CompletedTask,


                    };
                    b.RequireHttpsMetadata = false;
                    b.SaveToken = true;
                    b.TokenValidationParameters = new TokenValidationParameters()
                    {

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = JwtSettings.GetSecurityKey(),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddDbContext<AppDBContext>();

            services.AddScoped<DbContext, AppDBContext>();


            services.AddScoped(typeof(IPropertyAccessEnumerableRepository<>), typeof(PropertyAccessEnumerableRepository<>));

            services.AddScoped(typeof(IEnumerableRepository<>), typeof(PropertyAccessEnumerableRepository<>));


            //OPEN GENERICS СПАСИБО ЛЕГЕНДА (жаль я поздно о них узнал)

            services.AddScoped<IEnumerableRepository<ApiUser>, ApiUserRepository>();

            services.AddScoped<IPropertyAccessEnumerableRepository<InputQuestion>, QuestionsRepository>();


            services.AddScoped<IPropertyAccessEnumerableRepository<TestCase>, TestCaseRepository>();


            services.AddScoped<IEnumerableRepository<User>, PropertyAccessEnumerableRepository<User>>();

            services.AddScoped<IPropertyAccessEnumerableRepository<TestResult>, ResultsRepository>();


            services.AddScoped<IUserManagerService, UserManagerService>();




            services.AddControllers().AddJsonOptions(
                configure =>
                {
                    configure.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    configure.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
                }
                );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("regexe", new OpenApiInfo { Title = "regexe", Version = "v1" });
                c.SchemaFilter<ApplyCustomSchemaFilters>();
                c.DocumentFilter<RemoveSchemasFilter>();


                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };

                c.AddSecurityRequirement(securityRequirement);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();




            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";

                c.InjectStylesheet("style.css");
                c.SwaggerEndpoint("/swagger/regexe/swagger.json", "Reg.exe WebApi v1");


            });



            app.UseHttpsRedirection();



            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {




                endpoints.MapControllers();
            });
        }
    }
}
