using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class GameView : MonoBehaviour
{
<<<<<<< Updated upstream
=======
    [SerializeField]
    public DebugView debug = new DebugView();

    [SerializeField]
    public ChatView chat = new ChatView();

    [SerializeField]
    public InputTestView inputTest = new();

>>>>>>> Stashed changes
    void Update()
    {
        //Debug.Log(AnotherScroll1);
    }

    void LateUpdate()
    {
        ChatLogicGUI.UpdateScroll(chat);
    }
}
