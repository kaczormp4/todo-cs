using TodoApi;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyCustomReactApp", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
}
);


var app = builder.Build();
app.UseCors("AllowMyCustomReactApp");

var takeDb = MockStartUp.Initialize();

app.MapGet("/todos", () => takeDb.GetAllItems());

app.MapPost("/add", (string text) => takeDb.Add(text));

app.MapPut("/change-status", (int id) => takeDb.ChangeStatus(id));

app.Run();