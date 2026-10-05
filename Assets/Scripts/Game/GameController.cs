using DebugLogic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class GameController : MonoBehaviour
{
    [SerializeField]
    public GameView view;
    public readonly TransparentWindow windowSettings = new TransparentWindow();

    public readonly GameData data = new();

    private BaseGameState _state;

    void Start()
    {
        windowSettings.Execute();
        DEBUG.Subscribe(data.debugData);
        SetState(data.STATES.INIT);
    }

    void OnDestroy()
    {
        DEBUG.UnSubscribe(data.debugData);
    }

    void Update()
    {
        DebugLogic.DEBUG.Update(data.debugData);
        this._state.Update(Time.deltaTime, view, data);
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
    }
}
