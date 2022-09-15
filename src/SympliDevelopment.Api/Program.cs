using Microsoft.OpenApi.Models;
using SympliDevelopment.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<ISearchEngineResponse, SearchEngineResponseWeb>();
//builder.Services.AddSingleton<ISearchEngineResponse, SearchEngineResponseFile>();

//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SimplyDevelopmentApiCache", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimplyDevelopmentApiCache v1"));
    app.UseDeveloperExceptionPage();
}
// not implemented
//else
//{
//    app.UseExceptionHandler("/Error");
//    app.UseStatusCodePagesWithReExecute("/Error/{0}");
//}

app.UseAuthorization();
app.MapControllers();
app.Run();
