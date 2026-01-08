using System.Text.Json.Nodes;

namespace GameShelf.Libraries;

internal class LocalLibrary : Library
{
    protected override LibrarySource Source { get; } = LibrarySource.Local;

    private string SaveFile { get => SavePath + "/localgames.json"; }

    public override void Refresh()
    {
        Save();
        Clear();
        Load();
    }

    // These functions can be way more simplified - TODO
    public override void Load()
    {
        if (!File.Exists(SaveFile)) return;

        var json = File.ReadAllText(SaveFile);
        var games = JsonNode.Parse(json).AsArray();

        foreach (var game in games)
        {
            var gameObj = game.AsObject();

            string name = "";
            string icon = "";
            string coverArt = "";
            string path = "";
            string args = "";

            if (gameObj.TryGetPropertyValue("name", out var nameNode))
                name = nameNode.GetValue<string>();

            if (gameObj.TryGetPropertyValue("icon", out var iconNode))
                icon = iconNode.GetValue<string>();

            if (gameObj.TryGetPropertyValue("coverArt", out var coverArtNode))
                coverArt = coverArtNode.GetValue<string>();

            if (gameObj.TryGetPropertyValue("path", out var pathNode))
                path = pathNode.GetValue<string>();

            if (gameObj.TryGetPropertyValue("args", out var argsNode))
                args = argsNode.GetValue<string>();

            Shared.games.Add(new GameInfo()
            {
                Name = name,
                Source = Source,
                IconPath = icon,
                CoverArtPath = coverArt,
                LaunchPath = path,
                LaunchArgs = args,
            });
        }
    }

    public override void Save()
    {
        var localGames = Games.Where(x => x.Source == Source);

        if (localGames.Count() <= 0)
        {
            if (File.Exists(SaveFile))
                File.Delete(SaveFile);

            return;
        }

        var games = new JsonArray();

        foreach (var game in localGames)
        {
            var gameObj = new JsonObject();
            games.Add(gameObj);

            if (!string.IsNullOrWhiteSpace(game.Name))
            {
                var value = JsonValue.Create<string>(game.Name);
                gameObj.Add(new KeyValuePair<string, JsonNode?>("name", value));
            }

            if (!string.IsNullOrWhiteSpace(game.IconPath))
            {
                var value = JsonValue.Create<string>(game.IconPath);
                gameObj.Add(new KeyValuePair<string, JsonNode?>("icon", value));
            }

            if (!string.IsNullOrWhiteSpace(game.CoverArtPath))
            {
                var value = JsonValue.Create<string>(game.CoverArtPath);
                gameObj.Add(new KeyValuePair<string, JsonNode?>("coverArt", value));
            }

            if (!string.IsNullOrWhiteSpace(game.LaunchPath))
            {
                var value = JsonValue.Create<string>(game.LaunchPath);
                gameObj.Add(new KeyValuePair<string, JsonNode?>("path", value));
            }

            if (!string.IsNullOrWhiteSpace(game.LaunchArgs))
            {
                var value = JsonValue.Create<string>(game.LaunchArgs);
                gameObj.Add(new KeyValuePair<string, JsonNode?>("args", value));
            }
        }

        var json = games.ToJsonString(new System.Text.Json.JsonSerializerOptions()
        {
            WriteIndented = true,
        });

        if (!Directory.Exists(SavePath))
            Directory.CreateDirectory(SavePath);

        File.WriteAllText(SaveFile, json);
    }
}
