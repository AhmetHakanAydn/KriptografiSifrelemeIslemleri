using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Önce varsayılan wwwroot klasörü için statik dosyaları etkinleştir
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// React uygulaması için statik dosyaları serve et
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "dist")),
    RequestPath = ""
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// React uygulamasının index.html dosyasını serve et
app.MapFallback(context =>
{
    context.Response.ContentType = "text/html";
    var path = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "dist", "index.html");
    return context.Response.SendFileAsync(path);
});

app.Run();
