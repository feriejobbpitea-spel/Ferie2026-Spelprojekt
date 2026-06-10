using System.Collections.Generic;
using UnityEngine;

public class UI_MenuManager : Singleton<UI_MenuManager>
{
    private UI_Menu[] menuList;

    protected override void Awake()
    {
        base.Awake();
        menuList = GameObject.FindObjectsByType<UI_Menu>();
    }

    public static void CloseAllMenus() => Instance.Internal_CloseAllMenus();

    protected void Internal_CloseAllMenus() 
    {
        foreach (UI_Menu menu in menuList)
        {
            menu.SetVisibility(false);
        }
    }
}
