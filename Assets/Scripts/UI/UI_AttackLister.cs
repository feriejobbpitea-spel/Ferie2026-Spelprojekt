using System.Collections.Generic;
using UnityEngine;

public class UI_AttackLister : MonoBehaviour
{
    [SerializeField] private int PlayerID;
    [SerializeField] private UI_AttackFeedback Prefab;

    private void OnEnable()
    {
        Player.OnCreated += Generate_UI;
    }

    private void OnDisable()
    {
        Player.OnCreated -= Generate_UI;
    }

    public void Generate_UI(Player player) 
    {
        if (player.PlayerID != PlayerID)
            return;

        var meleeCombat = player.GetComponents<Meleecombat>();
        foreach (Meleecombat combat in meleeCombat)
        {
            var feedback = Instantiate(Prefab, transform);
            combat.UI = feedback;
        }

        var weapons = player.GetComponents<Weapon>();
        foreach (Weapon weapon in weapons)
        {
            var feedback = Instantiate(Prefab, transform);
            weapon.UI = feedback;
        }
    }
}
