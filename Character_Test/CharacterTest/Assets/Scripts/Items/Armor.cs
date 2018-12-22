using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ArmorType { Helmet, Shoulders, Chest, Gloves, Legs, Feet, Back, Ring, Neck, MainHand, OffHand, TwoHand }

[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor", order = 2)]
public class Armor : Item
{
    [SerializeField]
    private ArmorType armorType;

    [SerializeField]
    private int intelligence;

    [SerializeField]
    private int strength;

    [SerializeField]
    private int dexterity;

    internal ArmorType MyArmorType
    {
        get
        {
            return armorType;
        }
    }

    public override string GetDescription()
    {
        string stats = string.Empty;

        if (strength > 0)
        {
            stats += string.Format("\n + {0} Strength", strength);
        }
        if (dexterity > 0)
        {
            stats += string.Format("\n + {0} Dexterity", dexterity);
        }
        if (intelligence > 0)
        {
            stats += string.Format("\n + {0} Intelligence", intelligence);
        }        
        
        return base.GetDescription() + stats;
    }

}
