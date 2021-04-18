﻿using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReflectionIT.Mvc.Paging;
using System;
using System.Linq;
using WebApplication1.EfStuff;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Repositoryies.Airport;
using WebApplication1.Extensions;
using WebApplication1.Models;
using WebApplication1.Models.Airport;
using WebApplication1.ViewModels;
using WebApplication1.Services;
using WebApplication1.Profiles;
using Newtonsoft.Json;
using WebApplication1.Presentation;
using WebApplication1.Services.Education;

namespace WebApplication1
{
	public class Startup
    {
        public const string AuthMethod = "Smile";

        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNewtonsoftJson();
			services.AddOpenApiDocument();
			services.AddRazorPages()
				 .AddRazorRuntimeCompilation();

			var connectionString = Configuration.GetValue<string>("SpecialConnectionStrings");
            services.AddDbContext<KzDbContext>(option => option.UseSqlServer(connectionString));

            //AddDataToDB.AddData(connectionString);
          
            RegisterRepositories(services);

            services.AddScoped<UserService>(x =>
                new UserService(
                    x.GetService<CitizenRepository>(),
                    x.GetService<IHttpContextAccessor>())
                );

            services.AddScoped<CitizenPresentation>(x => 
                new CitizenPresentation(x.GetService<CitizenRepository>()));
                       
            services.AddPoliceServices(Configuration);
            RegisterAutoMapper(services);

            services.AddAuthentication(AuthMethod)
                .AddCookie(AuthMethod, config =>
                {
                    config.Cookie.Name = "Smile";
                    config.LoginPath = "/Citizen/Login";
                    config.AccessDeniedPath = "/Citizen/Login";
                });

            services.AddHttpContextAccessor();
            //services.AddPaging();
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<CitizenRepository>(x =>
                new CitizenRepository(x.GetService<KzDbContext>())
                );

            services.AddScoped<AdressRepository>(x =>
                new AdressRepository(x.GetService<KzDbContext>())
                );

            //--------------------------------------------------------

            services.AddScoped<UniversityRepository>(x =>
                new UniversityRepository(x.GetService<KzDbContext>())
                );

            services.AddScoped<StudentRepository>(x =>
                new StudentRepository(x.GetService<KzDbContext>())
                );
            services.AddScoped<IncomingFlightsRepository>(x =>
                new IncomingFlightsRepository(x.GetService<KzDbContext>())
                );
            services.AddScoped<DepartingFlightsRepository>(x =>
                new DepartingFlightsRepository(x.GetService<KzDbContext>())
                );
            services.AddScoped<PassengersRepository>(x =>
                new PassengersRepository(x.GetService<KzDbContext>())
                );
            services.AddScoped<FiremanRepository>(x =>
                 new FiremanRepository(x.GetService<KzDbContext>())
             );

            services.AddScoped<SchoolRepository>(x =>
                new SchoolRepository(x.GetService<KzDbContext>())
                );

            services.AddScoped<PupilRepository>(x =>
                new PupilRepository(x.GetService<KzDbContext>())
                );

            //--------------------------------------------------------

            services.AddScoped<BusRepository>(x =>
                new BusRepository(x.GetService<KzDbContext>())
                );

            services.AddScoped<TripRouteRepository>(x =>
                new TripRouteRepository(x.GetService<KzDbContext>())
                );


            services.AddScoped<SportComplexRepository>(x =>
                new SportComplexRepository(x.GetService<KzDbContext>())
                );
            services.AddScoped<SportEventRepository>(x =>
                new SportEventRepository(x.GetService<KzDbContext>())
                );
        }

        private void RegisterAutoMapper(IServiceCollection services)
        {
            var configurationExp = new MapperConfigurationExpression();

            configurationExp.CreateMap<Adress, AdressViewModel>()
                .ForMember(nameof(AdressViewModel.CitizenCount),
                    opt => opt.MapFrom(adress => adress.Citizens.Count()));
            configurationExp.CreateMap<AdressViewModel, Adress>();
            configurationExp.CreateMap<IncomingFlightInfo, IncomingFlightInfoViewModel>();
            configurationExp.CreateMap<IncomingFlightInfoViewModel, IncomingFlightInfo>();
            configurationExp.CreateMap<DepartingFlightInfo, DepartingFlightInfoViewModel>();
            configurationExp.CreateMap<DepartingFlightInfoViewModel, DepartingFlightInfo>();
            
            configurationExp.AddProfile<PoliceProfiles>();

            configurationExp.CreateMap<University, UniversityViewModel>()
              .ForMember(nameof(UniversityViewModel.StudentCount),
                  opt => opt.MapFrom(univer => univer.Students.Count()));
            configurationExp.CreateMap<UniversityViewModel, University>();
            configurationExp.CreateMap<School, SchoolViewModel>()
             .ForMember(nameof(SchoolViewModel.PupilCount),
                 opt => opt.MapFrom(school => school.Pupils.Count()));
            configurationExp.CreateMap<SchoolViewModel, School>();
            configurationExp.CreateMap<Student, StudentViewModel>();
            configurationExp.CreateMap<StudentViewModel, Student>();
            configurationExp.CreateMap<Pupil, PupilViewModel>();
            configurationExp.CreateMap<PupilViewModel, Pupil>();

            configurationExp.CreateMap<Fireman, FiremanViewModel>();
            configurationExp.CreateMap<FiremanViewModel, Fireman>();

            configurationExp.CreateMap<Fireman, FiremanShowViewModel>()
             .ForMember(nameof(FiremanShowViewModel.Name),
                    opt => opt.MapFrom(fireman => fireman.Citizen.Name))
            .ForMember(nameof(FiremanShowViewModel.Age),
                    opt => opt.MapFrom(fireman => fireman.Citizen.Age));

            configurationExp.CreateMap<FiremanShowViewModel, Fireman>();
            var config = new MapperConfiguration(configurationExp);
            var mapper = new Mapper(config);
            services.AddScoped<IMapper>(x => mapper);
            services.AddScoped<StudentPresentation>(x =>
              new StudentPresentation(x.GetService<StudentRepository>(), mapper));
            services.AddScoped<PupilPresentation>(x =>
              new PupilPresentation(x.GetService<PupilRepository>(), x.GetService<StudentRepository>(), mapper));
            /*
            var configurationExpNew = new MapperConfigurationExpression();

            configurationExpNew.CreateMap<Bus, BusParkViewModel>();
            configurationExpNew.CreateMap<BusParkViewModel, Bus>();

            var configNew = new MapperConfiguration(configurationExpNew);
            var mapperNew = new Mapper(configNew);
            services.AddScoped<IMapper>(x => mapperNew);

            var configurationExpTrip = new MapperConfigurationExpression();

            configurationExpTrip.CreateMap<TripRoute, TripViewModel>();
            configurationExpTrip.CreateMap<TripViewModel, TripRoute>();

            var configTrip = new MapperConfiguration(configurationExpTrip);
            var mapperTrip = new Mapper(configTrip);
            services.AddScoped<IMapper>(x => mapperTrip);*/
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

            app.UseRouting();

            app.UseAuthentication();

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
