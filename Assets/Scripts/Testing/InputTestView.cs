using System;
using TMPro;
using UnityEngine;

[Serializable]
public class InputTestView
{
    public TMP_InputField input;
    public SpriteRenderer sprite;

    public int lettersCount = 0;
    public float baseLength = 0.2f;
}
