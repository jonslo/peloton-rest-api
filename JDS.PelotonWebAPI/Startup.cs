using System;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using JDS.PelotonWebAPI.Domain;
using JDS.PelotonWebAPI.Domain.Repositories;
using JDS.PelotonWebAPI.Domain.Services;
using JDS.PelotonWebAPI.Domain.Wrappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace JDS.PelotonWebAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IContainer ApplicationContainer { get; private set; }

        public IConfigurationRoot Configuration { get; }

        // ConfigureServices is where you register dependencies. This gets
        // called by the runtime before the Configure method, below.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add services to the collection.
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Peloton AppFrame API", Version = "v1" });

                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "JDS.PelotonWebAPI.xml");
                c.IncludeXmlComments(filePath);
            });

            services.Configure<PelotonOptions>(Configuration.GetSection("Peloton"));

            // Create the container builder.
            var builder = new ContainerBuilder();

            // Register dependencies, populate the services from
            // the collection, and build the container. If you want
            // to dispose of the container at the end of the app,
            // be sure to keep a reference to it as a property or field.
            //
            // Note that Populate is basically a foreach to add things
            // into Autofac that are in the collection. If you register
            // things in Autofac BEFORE Populate then the stuff in the
            // ServiceCollection can override those things; if you register
            // AFTER Populate those registrations can override things
            // in the ServiceCollection. Mix and match as needed.
            builder.Populate(services);

            var pelotonOptions = Configuration.GetSection("Peloton").Get<PelotonOptions>();

            builder.RegisterType<RequestService>().As<IRequestService>();
            builder.RegisterType<DataModelRepository>().As<IDataModelRepository>().SingleInstance();
            builder.RegisterType<LibraryRepository>().As<ILibraryRepository>();
            builder.RegisterType<TableDataRecordRepository>().As<ITableDataRecordRepository>();
            builder.Register(e => new EngineService(new ObjectPool<IOEngine>(() =>
                new IOEngine(pelotonOptions.RootFolder + @"\system", pelotonOptions.RootFolder + @"\custom",
                    pelotonOptions.RootFolder + @"\user", pelotonOptions.ProfileName, pelotonOptions.ProfilePassword,
                    pelotonOptions.UnitSetName)))).As<IEngineService>().SingleInstance();

            ApplicationContainer = builder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // Configure is where you add middleware. This is called after
        // ConfigureServices. You can use IApplicationBuilder.ApplicationServices
        // here if you need to resolve things from the container.
        public void Configure(
            IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IOptions<PelotonOptions> pelotonOptions, IApplicationLifetime appLifetime)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();
            app.UseMvc();

            app.UseSwagger(c =>
                {
                    c.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Host = httpReq.Host.Value);
                });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Peloton AppFrame API V1");
                c.InjectStylesheet("/swagger-ui/custom.css");
                c.RoutePrefix = "docs";
                c.DocExpansion(DocExpansion.None);
                c.ValidatorUrl(null);
            });

            // If you want to dispose of resources that have been resolved in the
            // application container, register for the "ApplicationStopped" event.
            // You can only do this if you have a direct reference to the container,
            // so it won't work with the above ConfigureContainer mechanism.
            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;

            Assembly.LoadFrom(pelotonOptions.Value.RootFolder + @"\system\bin\Peloton.AppFrame.IO.dll");
            Assembly.LoadFrom(pelotonOptions.Value.RootFolder + @"\system\bin\Peloton.AppFrame.Library.dll");

            try
            {
                Assembly.LoadFrom(pelotonOptions.Value.DataModel);
            }
            catch (Exception e)
            {
                Console.WriteLine("Data model not loaded; proper names will not be used");
            }
            
        }

        private static Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            return Assembly.ReflectionOnlyLoad(args.Name);
        }
    }
}