using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public partial class GameView : MonoBehaviour
{
    [SerializeField]
    public DebugView debug = new DebugView();

    void Update()
    {
        //Debug.Log(AnotherScroll1);
    }
}
