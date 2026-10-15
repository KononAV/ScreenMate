using UnityEngine;

public partial class GameController : MonoBehaviour
{
    public readonly TransparentWindow windowSettings = new TransparentWindow();

    void Start()
    {
        windowSettings.Execute();
    }

<<<<<<< Updated upstream
    void Update() { }
=======
    void OnDestroy()
    {
        DEBUG.UnSubscribe(data.debugData);
    }

    void Update()
    {
        DebugLogic.DEBUG.Update(data.debugData);
        this._state.Update(Time.deltaTime, view, data);

        InputTest.OnGUI(view);
    }

    void SetState(BaseGameState newState)
    {
        this._state?.OnExit(view, data);
        this._state = newState;
        this._state.OnEnter(view, data);
    }

    void OnGUI()
    {
        // if (Keyboard.current.enterKey.wasPressedThisFrame && data.debugData.showConsole)
        DebugLogic.DEBUG.UpdateHistory(data.debugData);
        DebugLogic.DEBUG.OnGUI(data.debugData);

        ChatLogicGUI.UpdateScroll(view.chat);
    }
>>>>>>> Stashed changes
}
