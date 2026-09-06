using UnityEngine;

public struct ScrollPanelLogic
{
    public void ShowScrollPanel(GameObject scrollObject)
    {
        scrollObject.SetActive(true);
    }

    public void HideScrollPanel(GameObject scrollObject)
    {
        scrollObject.SetActive(false);
    }
}
