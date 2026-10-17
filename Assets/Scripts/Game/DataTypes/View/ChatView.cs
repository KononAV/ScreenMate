using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class ChatView
{
    public TMP_Text text;
    public TMP_InputField input;
    public Button send;
    public Vector2 scroll;
    public ScrollRect answerScroll;

    [NonSerialized]
    public string lastAnswerText;

    public void AddSendButtonListener(UnityAction listener)
    {
        send.onClick.AddListener(listener);
    }

    public void RemoveSendButtonListener(UnityAction listener)
    {
        send.onClick.RemoveListener(listener);
    }

    public void RemoveAllSendButtonListeners()
    {
        send.onClick.RemoveAllListeners();
    }
}
