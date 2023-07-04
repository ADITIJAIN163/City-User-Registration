namespace MiniProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(Option =>
            {
                Option.IdleTimeout = TimeSpan.FromMinutes(2);
                Option.Cookie.HttpOnly = true; //whether client side script can access cookie. false means yes
                Option.Cookie.IsEssential = true;  //cookie will be always created if true
            });
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddMemoryCache();

            var app = builder.Build();
           


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=RegisteredUser}/{action=Register}/{id?}/{LoginName?}/");

            app.Run();
        }
    }
}