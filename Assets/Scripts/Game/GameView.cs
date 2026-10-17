using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class GameView : MonoBehaviour
{
    [SerializeField]
    public DebugView debug = new DebugView();

    [SerializeField]
    public ChatView chat = new ChatView();

    [SerializeField]
    public InputTestView inputTest = new();

    void Update()
    {
        //Debug.Log(AnotherScroll1);
    }
}
