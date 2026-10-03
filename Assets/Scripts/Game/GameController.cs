using UnityEngine;

public partial class GameController : MonoBehaviour
{
    public readonly TransparentWindow windowSettings = new TransparentWindow();

    void Start()
    {
        windowSettings.Execute();
    }

    void Update() { }
}
