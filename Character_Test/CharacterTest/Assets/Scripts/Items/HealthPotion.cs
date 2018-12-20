using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/HealthPotion", order = 1)]
public class HealthPotion : Item, IUseable
{
    [SerializeField]
    private int health;

    [SerializeField]
    private string itemDescription;

    public void Use()
    {
        if (Player.MyInstance.MyHealth.MyCurrentValue < Player.MyInstance.MyHealth.MyMaxValue)
        {
            Remove();

            Player.MyInstance.MyHealth.MyCurrentValue += health;

            InventoryScript.MyInstance.AddEmptyBottle();
        }
    }

    public override string GetDescription()
    {   
        
        return base.GetDescription() + "\n" + itemDescription;
    }
}
