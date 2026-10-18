using DebugLogic;
using GameControllerLogic;
using UnityEngine;

public partial class GameController : MonoBehaviour
{
    [SerializeField]
    public GameView view;
    public readonly GameData data = new();

    private BaseGameState _state;

    void Start()
    {
        InitLogic.InitGame(this);
        SetState(data.STATES.INIT);
    }

    void OnDestroy()
    {
        DEBUG.UnSubscribe(data.debugData);
        _state?.OnExit(view, data);
    }

    void Update()
    {
        DEBUG.Update(data.debugData);
        this._state.Update(Time.deltaTime, view, data);
    }

    void LateUpdate() { }

    void SetState(BaseGameState newState)
    {
        this._state?.OnExit(view, data);
        this._state = newState;
        this._state.OnEnter(view, data);
    }

    void OnGUI()
    {
        // if (Keyboard.current.enterKey.wasPressedThisFrame && data.debugData.showConsole)
        _state.OnGUI(view, data);
    }
}
