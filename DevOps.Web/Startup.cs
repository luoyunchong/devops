using System;
using System.IO;
using AutoMapper;
using DevOps.Core.AutoMapper;
using FreeSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DevOps.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            IConfigurationSection configurationSection = Configuration.GetSection("ConnectionStrings:Default");
            Fsql = new FreeSqlBuilder()
                .UseConnectionString(DataType.MySql, configurationSection.Value)
                .UseAutoSyncStructure(true)
                .Build();

            //Fsql.CodeFirst.IsAutoSyncStructure = true;

            Fsql.Aop.CurdAfter = (s, e) =>
            {
                if (e.ElapsedMilliseconds > 200)
                {
                    //��¼��־
                    //���Ͷ��Ÿ�������
                }
            };
        }

        public IConfiguration Configuration { get; }
        public IFreeSql Fsql { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFreeSql>(Fsql);

            services.AddAutoMapper(typeof(BlogProfile).Assembly);

            //Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo() { Title = "DevOps.Web", Version = "v1" });
                var security = new OpenApiSecurityRequirement()
                {
                    { new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference()
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    }, Array.Empty<string>() }
                };
                options.AddSecurityRequirement(security);//���һ�������ȫ�ְ�ȫ��Ϣ����AddSecurityDefinition����ָ���ķ�������Ҫһ�£�������Bearer��
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) �����ṹ: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                    Type = SecuritySchemeType.ApiKey
                });

                string xmlPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml");
                options.IncludeXmlComments(xmlPath);

            });
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

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            //// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            //// specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LinCms");

                //c.RoutePrefix = string.Empty;
                //c.OAuthClientId("demo_api_swagger");//�ͷ�������
                //c.OAuthAppName("Demo API - Swagger-��ʾ"); // ����
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
