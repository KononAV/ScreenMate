using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public static class ChatLogicGUI
{
    public static void UpdateScroll(ChatView view)
    {
        // if (view.answerScroll)
        // {
        //     RectTransform frame = view.answerScroll.viewport;

        //     frame.SetSizeWithCurrentAnchors(
        //         RectTransform.Axis.Vertical,
        //         frame.rect.height + view.text.ToString().Length
        //     );

        //     Debug.Log(frame.rect.height);
        // }

        if (view == null || view.text == null)
            return;

        if (view.answerScroll == null)
            CreateAnswerScroll(view);

        var viewport = view.answerScroll.viewport;
        float width = viewport.rect.width;
        bool textChanged = view.lastAnswerText != view.text.text;
        if (
            !textChanged
            && Mathf.Approximately(view.lastAnswerWidth, width)
            && Mathf.Approximately(view.lastAnswerHeight, viewport.rect.height)
        )
            return;

        if (width <= 0)
            return;

        float height = view.text.GetPreferredValues(view.text.text, width, Mathf.Infinity).y;
        view.answerScroll.content.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Vertical,
            Mathf.Max(viewport.rect.height, height)
        );
        view.text.ForceMeshUpdate();
        Canvas.ForceUpdateCanvases();

        if (textChanged)
        {
            view.answerScroll.StopMovement();
            view.answerScroll.verticalNormalizedPosition = 1;
        }

        view.lastAnswerText = view.text.text;
        view.lastAnswerWidth = width;
        view.lastAnswerHeight = viewport.rect.height;
    }

    private static void CreateAnswerScroll(ChatView view)
    {
        var textRect = view.text.rectTransform;
        var viewportObject = new GameObject(
            "Answer Scroll",
            typeof(RectTransform),
            typeof(Image),
            typeof(RectMask2D),
            typeof(ScrollRect)
        );
        viewportObject.layer = textRect.gameObject.layer;
        var viewport = viewportObject.GetComponent<RectTransform>();
        viewport.SetParent(textRect.parent, false);
        viewport.SetSiblingIndex(textRect.GetSiblingIndex());

        viewport.anchorMin = textRect.anchorMin;
        viewport.anchorMax = textRect.anchorMax;
        viewport.pivot = textRect.pivot;
        viewport.sizeDelta = textRect.sizeDelta;
        viewport.anchoredPosition3D = textRect.anchoredPosition3D;
        viewport.localRotation = textRect.localRotation;
        viewport.localScale = textRect.localScale;

        var background = viewportObject.GetComponent<Image>();
        background.color = Color.clear;
        background.raycastTarget = true;

        textRect.SetParent(viewport, false);
        textRect.anchorMin = new Vector2(0, 1);
        textRect.anchorMax = new Vector2(1, 1);
        textRect.pivot = new Vector2(0.5f, 1);
        textRect.anchoredPosition3D = Vector3.zero;
        textRect.localScale = Vector3.one;
        textRect.localRotation = Quaternion.identity;
        textRect.sizeDelta = Vector2.zero;

        view.text.textWrappingMode = TextWrappingModes.Normal;
        view.text.overflowMode = TextOverflowModes.Overflow;
        view.text.alignment = TextAlignmentOptions.TopLeft;
        view.text.raycastTarget = true;

        var scroll = viewportObject.GetComponent<ScrollRect>();
        scroll.viewport = viewport;
        scroll.content = textRect;
        scroll.horizontal = false;
        scroll.vertical = true;
        scroll.movementType = ScrollRect.MovementType.Clamped;
        scroll.scrollSensitivity = 25;
        view.answerScroll = scroll;
    }
}
