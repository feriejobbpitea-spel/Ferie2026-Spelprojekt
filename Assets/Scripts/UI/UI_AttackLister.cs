using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_AttackLister : MonoBehaviour
{
    [SerializeField] private int PlayerID;
    [SerializeField] private UI_AttackFeedback Prefab;
    private List<UI_AttackFeedback> spawnedUI = new();

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
            spawnedUI.Add(feedback);
        }

        var weapons = player.GetComponents<Weapon>();
        foreach (Weapon weapon in weapons)
        {
            var feedback = Instantiate(Prefab, transform);
            weapon.UI = feedback;
            spawnedUI.Add(feedback);
        }
       
        var blocks = player.GetComponents<PlayerBlock>();
        foreach (PlayerBlock block in blocks)
        {
            var feedback = Instantiate(Prefab, transform);
            block.attackFeedback = feedback;
            spawnedUI.Add(feedback);
        }
    }

    private void Update()
    {
        Dictionary<char, UI_AttackFeedback> keys = new();
        foreach (var item in spawnedUI)
        {
            var firstChar = item.KeyToPress.ToCharArray().FirstOrDefault();
            if(!keys.ContainsKey(firstChar))
                keys.Add(firstChar, item);
        }

        // Convert to unicode
        var sortedKeys = keys.OrderBy(x => (int)x.Key);
        foreach (var key in sortedKeys) 
        {
            key.Value.transform.SetSiblingIndex(key.Key);
        }
    }
}
