using GameStore.Api.DTOS;
const string GetGameEndpointName = "GetGame";
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
List <GameDto> games = [
    new (1,
    "GTA V",
    "Action",
    30.00M,
    new DateOnly(2013,6,23)),
     new (2,
    "Forza Horizon 5",
    "Racing",
    50.00M,
    new DateOnly(2023,2,23)),
     new (3,
    "Minecraft",
    "Creative",
    29.99M,
    new DateOnly(2009,5,27))
];
//GET /games
app.MapGet("/games", () => games);



//GET /games/1
app.MapGet("/games/{id}",(int id)=>games.Find(game=>game.Id==id))
.WithName(GetGameEndpointName);
// POST /games
app.MapPost("/games",(CreateGameDto newGame)=>
{
    GameDto game=new(
        games.Count+1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndpointName,new {id=game.Id},game);
});

//PUT /games/1
app.Run();
