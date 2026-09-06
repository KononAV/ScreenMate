using UnityEngine;

public class GameController : MonoBehaviour
{
    public readonly TransparentWindow windowSettings = new TransparentWindow();
    public readonly GameView gameView;

    void Start()
    {
        windowSettings.Execute();
        var a = 0;
    }

    void Update() { }
}
