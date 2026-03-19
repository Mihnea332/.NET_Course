using GameStore.Api.Data;
using GameStore.Api.DTOS;
using GameStore.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var connsString="Data Source=GameStore.db";
builder.Services.AddSqlite<GameStoreContext>(connsString);
var app = builder.Build();
app.MapGamesEndpoints();
app.Run();
