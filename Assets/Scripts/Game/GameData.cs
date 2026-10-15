<<<<<<< Updated upstream
public class GameData { }
=======
using System;
using System.Net.Http;

public class GameData
{
    public readonly GameStatesCollection STATES = new();
    public DebugData debugData = new();
    public readonly HttpClient Client = new HttpClient { Timeout = TimeSpan.FromSeconds(90) };
}
>>>>>>> Stashed changes
