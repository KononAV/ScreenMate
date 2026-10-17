public abstract class BaseGameState
{
    public abstract void OnEnter(GameView view, GameData data);

    public abstract void OnExit(GameView view, GameData data);
    public abstract void Update(float dt, GameView view, GameData data);
}
