using API;
using API.Middleware;
using API.Modules.Catalog.Startup;
using API.Modules.Shopping.Startup;
using API.Modules.UserAccess.Startup;

var builder = WebApplication.CreateBuilder(args);

//API Services  
builder.Services.AddPresentation(builder.Configuration);

//Modules startup
builder.Services.AddUserAccess(builder.Configuration);
builder.Services.AddCatalog(builder.Configuration);
builder.Services.AddShopping(builder.Configuration);

var app = builder.Build();


app.UseSwagger();

app.UseSwaggerUI();

app.UseExceptionHandler("/Error");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.Run();