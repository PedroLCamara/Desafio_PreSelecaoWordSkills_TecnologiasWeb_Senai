using alatech.Contexts;
using alatech.Interfaces;
using alatech.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace alatech
{
    public class Startup
    {
        public Startup(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
              .AddControllers()
              .AddNewtonsoftJson(options => {
                  options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                  options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
              });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                                builder =>
                                {
                                    builder.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "alatech.webAPI" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }
            ).AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("alatech-chave-seguro")),
                    ClockSkew = TimeSpan.FromHours(1),
                    ValidIssuer = "alatech.webAPI",
                    ValidAudience = "alatech.webAPI"
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = async (context) =>
                    {
                        context.Response.StatusCode = 403;
                        await context.HttpContext.Response.WriteAsJsonAsync(new { message = "Token inválido" });
                    },
                    OnChallenge = async (context) =>
                    {
                        context.HandleResponse();

                        // AuthenticateFailure property contains 
                        // the details about why the authentication has failed
                        if (context.AuthenticateFailure != null)
                        {
                            context.Response.StatusCode = 403;

                            await context.HttpContext.Response.WriteAsJsonAsync(new { message = "Token inválido" });
                        }
                        else
                        {
                            context.Response.StatusCode = 401;

                            await context.HttpContext.Response.WriteAsJsonAsync(new { message = "Necessário autenticação" });
                        }
                    }
                };
            });

            services.AddTransient<DbContext, alatechContext>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IPlacaMaeRepository, PlacaMaeRepository>();
            services.AddTransient<IProcessadorRepository, ProcessadorRepository>();
            services.AddTransient<IMemoriaRamRepository, MemoriaRamRepository>();
            services.AddTransient<IDispositivoArmazenamentoRepository, DispositivoArmazenamentoRepository>();
            services.AddTransient<IPlacaVideoRepository, PlacaVideoRepository>();
            services.AddTransient<IFonteRepository, FonteRepository>();
            services.AddTransient<IMaquinaRepository, MaquinaRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "alatech.webAPI");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
                RequestPath = "/StaticFiles"
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
