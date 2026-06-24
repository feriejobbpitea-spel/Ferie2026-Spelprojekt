using UnityEngine;

[RequireComponent(typeof(Player))]
public class WeaponBase : MonoBehaviour
{
    public Player Player {  get; private set; }
    public UI_AttackFeedback attackFeedback;
    
    protected virtual void Awake()
    {
        Player = GetComponentInParent<Player>();
    }


    public void UpdateUI() 
    {
            
    }
}
