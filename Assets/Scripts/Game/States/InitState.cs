using DebugList;
using DebugLogic;

public class InitGameState : BaseGameState
{
    public override void OnEnter(GameView view, GameData data)
    {
        DebugLogic.DEBUG.InitDebugData(data, view);
        // view.debug.windowSettings.Setup(
        //     minWidth: 200,
        //     minHeight: 100,
        //     maxWidth: 1000,
        //     maxHeight: 800
        // );
    }

    public override void OnExit(GameView view, GameData data)
    {
        throw new System.NotImplementedException();
    }

    public override void Update(float dt, GameView view, GameData data)
    {
        //throw new System.NotImplementedException();
    }
}
