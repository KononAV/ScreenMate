using DebugLogic;
using Unity.VisualScripting;

public class IdleChatGameState : BaseGameState
{
    public override void OnEnter(GameView view, GameData data)
    {
        // view.debug.windowSettings.Setup(
        //     minWidth: 200,
        //     minHeight: 100,
        //     maxWidth: 1000,
        //     maxHeight: 800
        // );
    }

    public override void OnExit(GameView view, GameData data) { }

    public override void Update(float dt, GameView view, GameData data)
    {
        ChatLogic.MakeStep(view, data);

        InputTest.OnGUI(view);
    }

    public override void OnGUI(GameView view, GameData data)
    {
        DEBUG.UpdateHistory(data.debugData);
        DEBUG.OnGUI(data.debugData);
    }
}
