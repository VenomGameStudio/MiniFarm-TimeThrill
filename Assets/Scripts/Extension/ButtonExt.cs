using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public static class ButtonExt
{
    public static void OnClick(this Button button, UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }
}