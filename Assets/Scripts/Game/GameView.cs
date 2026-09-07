using System.Collections.Generic;
using UnityEngine;

public partial class GameView : MonoBehaviour
{
    [SerializeField]
    [PublicReadonly]
    private List<int> scrollBarPanel0;

    [SerializeField]
    [PublicReadonly]
    private GameObject[] anotherScrollBar;

    // public List<Vector3> ScrollBarPanel0 => scrollBarPanel0;

    void Start()
    {
        Debug.Log(12);
    }

    void Update() { }
}
