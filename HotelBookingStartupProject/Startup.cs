using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Models;
using HotelBooking.Data;
using HotelBooking.Data.Repositories;
using HotelBooking.BusinessLogic;
using HotelBookingStartupProject.Models;

namespace HotelBooking
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
            //services.AddDbContext<HotelBookingContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("HotelBookingContext")));

            services.AddDbContext<HotelBookingContext>(opt => opt.UseInMemoryDatabase("HotelBookingDb"));

            services.AddScoped<IRepository<Room>, RoomRepository>();
            services.AddScoped<IRepository<Customer>, CustomerRepository>();
            services.AddScoped<IRepository<Booking>, BookingRepository>();
            services.AddScoped<IBookingManager, BookingManager>();
            services.AddScoped<IBookingViewModel, BookingViewModel>();

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {                
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();

                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    // Get HotelBookingContext and initialize database
                    var dbContext = services.GetService<HotelBookingContext>();
                    DbInitializer.Initialize(dbContext);
                }
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Bookings}/{action=Index}/{id?}");
            });
        }
    }
}
