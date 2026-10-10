using BreadOfLife.Components;
using BreadOfLife.Services;
using MongoDB.Driver;

DotNetEnv.Env.Load(".env");

var builder = WebApplication.CreateBuilder(args);

// 1. Fetch settings from appsettings.json
var mongoSettings = builder.Configuration.GetSection("MongoDbSettings");
var connectionString = mongoSettings["ConnectionString"];
var databaseName = mongoSettings["DatabaseName"];

// 2. Register IMongoClient as a Singleton
builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(connectionString));

// 3. Register IMongoDatabase scoped or singleton (optional but highly recommended)
builder.Services.AddScoped(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(databaseName);
});

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient();


builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CartService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapControllers();

app.Run();
