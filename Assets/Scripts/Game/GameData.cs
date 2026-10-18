using System;
using System.Net.Http;
using GameControllerLogic;

public class GameData
{
    public Listeners listeners;

    public readonly GameStatesCollection STATES = new();
    public DebugData debugData = new();

    public ChatData chat = new();
    public readonly HttpClient Client = new HttpClient { Timeout = TimeSpan.FromSeconds(90) };
}
