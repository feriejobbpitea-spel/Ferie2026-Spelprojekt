using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class UI_Menu : MonoBehaviour
{
    private Canvas canvas;

    public bool IsVisible => canvas.enabled;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        SetVisibility(false);
    }

    public void OpenMenu() 
    {
        UI_MenuManager.CloseAllMenus();
        SetVisibility(true);
    }

    public void CloseMenu() 
    {
        SetVisibility(false);
    }

    public void SetVisibility(bool visible)
    {
        canvas.enabled = visible;
    }
}
