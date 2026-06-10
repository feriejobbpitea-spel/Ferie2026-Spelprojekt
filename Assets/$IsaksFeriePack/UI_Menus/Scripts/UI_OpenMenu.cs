using System;
using UnityEngine;

[RequireComponent(typeof(ButtonWrapper))]
public class UI_OpenMenu : MonoBehaviour
{
    [SerializeField] private UI_Menu MenuToOpen;

    private ButtonWrapper buttonWrapper;
    private void Awake()
    {
        buttonWrapper = GetComponent<ButtonWrapper>();
    }

    private void OnEnable()
    {
        buttonWrapper.onClick.AddListener(TryOpenOrCloseMenu);
    }

    private void OnDisable()
    {
        buttonWrapper.onClick.RemoveListener(TryOpenOrCloseMenu);
    }

    private void TryOpenOrCloseMenu()
    {

        if (MenuToOpen.IsVisible)
            MenuToOpen.CloseMenu();

        // Make sure we can open the menu
        else if(CanOpenMenu())
            MenuToOpen.OpenMenu();
    }

    protected virtual bool CanOpenMenu() 
    {
        return true;
    }
}
