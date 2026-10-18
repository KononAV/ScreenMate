using System;
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

    [NonSerialized]
    public readonly TransparentWindow windowSettings = new TransparentWindow();
}
