using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CourseApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace CourseApp
{
    public class Startup
    {
        public IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();  //core ilk açılışta mvc gereksinimlerini yüklemek için kullanılır

            services.AddDbContext<DataContext>(options => { options.UseSqlServer(Configuration.GetConnectionString("DataConnection"));
                options.EnableSensitiveDataLogging(true);
            });
            services.AddTransient<ICourseRepository, EfCourseRepository>();

            services.AddDbContext<UserContext>(options => options.UseSqlServer(Configuration.GetConnectionString("UserConnection")));
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IInstructorRepository, EfInstructorRepsitory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DataContext datacontext,UserContext usercontext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedDatabase.Seed(datacontext);
                SeedDatabase.Seed(usercontext);
            }

            app.UseStatusCodePages();  //404 yada server hata sonucunu bize gösterir

            app.UseStaticFiles();  //wwwroot statik dosyasını kullan
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider=new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),@"node_modules")),RequestPath=new PathString("/vendor")
            });
            
            app.UseMvcWithDefaultRoute(); //root işlemi için gerekli,örnek olarak controller/home/index/id
        }
    }
}
