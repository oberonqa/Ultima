using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EmptyBottle", menuName = "Items/EmptyBottle", order = 2)]

public class EmptyBottle : Item, IUseable
{
    [SerializeField]
    private string itemDescription;

    public void Use()
    {
    }

    public override string GetDescription()
    {
        return base.GetDescription() + "\n" + itemDescription;
    }
}
