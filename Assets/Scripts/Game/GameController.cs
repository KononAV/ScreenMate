using DebugLogic;
using Unity.VisualScripting;
using UnityEngine;

public partial class GameController : MonoBehaviour
{
    [SerializeField]
    public GameView view;
    public readonly GameData data = new();
    private BaseGameState _state;

    public readonly TransparentWindow windowSettings = new TransparentWindow();

    void Start()
    {
        windowSettings.Execute();
        DEBUG.Subscribe(data.debugData);
        SetState(data.STATES.INIT);

        ChatLogicGUI.AddInputListener(view.chat, data.chat);
    }

    void OnDestroy()
    {
        DEBUG.UnSubscribe(data.debugData);
        _state?.OnExit(view, data);
    }

    void Update()
    {
        DebugLogic.DEBUG.Update(data.debugData);
        this._state.Update(Time.deltaTime, view, data);

        ChatLogic.MakeStep(view, data);

        InputTest.OnGUI(view);
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
        DebugLogic.DEBUG.UpdateHistory(data.debugData);
        DebugLogic.DEBUG.OnGUI(data.debugData);
    }
}
