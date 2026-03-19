using System;
using GameStore.Api.DTOS;

namespace GameStore.Api.EndPoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";
private static readonly List <GameDto> games = [
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


public static void MapGamesEndpoints(this  WebApplication app )
    {
        var group =app.MapGroup("/games");

//GET /games
group.MapGet("/", () => games);

//GET /games/1
group.MapGet("/games/{id}",(int id)=>
{
   var game= games.Find(game=>game.Id==id);
   return game is null ? Results.NotFound():Results.Ok(game);
})
.WithName(GetGameEndpointName);
// POST /games
group.MapPost("/",(CreateGameDto newGame)=>
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
group.MapPut("/{id}",(int id,UpdateGameDto updatedGame)=>
{
    var index=games.FindIndex(game=>game.Id==id);
    if (index==-1)
    {
        return Results.NotFound();
    }
    games[index]=new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );
    return Results.NoContent();
}
);
//DELETE /games/1
group.MapDelete("/{id}",(int id)=>
{
    games.RemoveAll(game=>game.Id==id);
    return Results.NoContent();
});
    }
}
