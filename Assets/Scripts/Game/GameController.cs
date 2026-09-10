using System.Reflection.Emit;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public partial class GameController : MonoBehaviour
{
    public readonly TransparentWindow windowSettings = new TransparentWindow();

    [PublicReadonly]
    public readonly GameView gameView0;

    [SerializeField]
    [PublicReadonly]
    private Button tertstLabel = null;

    [PublicReadonly]
    private EventSystem eventSystem;

    [PublicReadonly]
    private ShaderGraphRequirements shaderGraphRequirements;

    [PublicReadonly]
    private SerializedObject a;

    void Start()
    {
        windowSettings.Execute();
    }

    void Update() { }
}
