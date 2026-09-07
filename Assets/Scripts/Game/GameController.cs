using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

public partial class GameController : MonoBehaviour
{
    public readonly TransparentWindow windowSettings = new TransparentWindow();
    public readonly GameView gameView;

    [SerializeField]
    [PublicReadonly]
    private Button testLabel = null;

    void Start()
    {
        windowSettings.Execute();
    }

    void Update() { }
}
