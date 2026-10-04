using UnityEngine;

public partial class GameController : MonoBehaviour
{
    [SerializeField]
    public GameView view;
    public readonly TransparentWindow windowSettings = new TransparentWindow();

    public readonly GameData data = new();

    private BaseGameState _state;

    void Start()
    {
        DebugLogic.DEBUG.Subscribe(view.debug.consoleText);

        windowSettings.Execute();
        SetState(data.STATES.INIT);
    }

    void OnDestroy()
    {
        DebugLogic.DEBUG.UnSubscribe(view.debug.consoleText);
    }

    void Update()
    {
        this._state.Update(Time.deltaTime, view, data);
    }

    void SetState(BaseGameState newState)
    {
        this._state?.OnExit(view, data);
        this._state = newState;
        this._state.OnEnter(view, data);
    }
}
