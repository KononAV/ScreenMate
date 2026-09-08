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
    public readonly GameView gameView;

    // [SerializeField]
    // [PublicReadonly]
    // private Button testLabel = null;

    // [PublicReadonly]
    // private EventSystem eventSystem;

    // [PublicReadonly]
    // private ShaderGraphRequirements shaderGraphRequirements;

    [PublicReadonly]
    private SerializedObject a;

    void Start()
    {
        windowSettings.Execute();
    }

    void Update() { }
}
