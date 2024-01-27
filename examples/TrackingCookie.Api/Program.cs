using FromCookie.ModelBinding;

namespace TrackingCookie.Api
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.            
            builder.Services
                .AddHttpContextAccessor()
                .AddLogging();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // attach controllers
            builder.Services.AddControllers();

            builder.Services.AddMvcCore(options => 
            {
                options.AddFromCookieBinder();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            } // end if

            app.UseHttpsRedirection();

            app.MapControllers();

            app.UseTrackingCookieMiddleware(setup => 
            {
                setup.Name = ApiConstants.COOKIE_NAME;
            });

            app.Run();
        } // end method
    } // end class
} // end namespace