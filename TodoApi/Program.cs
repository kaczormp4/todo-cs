using TodoApi;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var takeDb = MockStartUp.Initialize();

app.MapGet("/todos", () => takeDb.GetAllItems());

app.MapPost("/add", (string text) => takeDb.Add(text));

app.MapPut("/change-status",(int id) => takeDb.ChangeStatus(id));

app.Run();