using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : MonoBehaviour
{
    private static CharacterPanel instance;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private CharButton head, shoulders, chest, gloves, legs, feet,
                       back, ring1, ring2, neck, mainhand, offhand;

    public CharButton MySelectedButton { get; set; }

    public static CharacterPanel MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CharacterPanel>();
            }

            return instance;
        }        
    }

    public void OpenClose()
    {
        if (canvasGroup.alpha <= 0)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
        }
    }

    public void EquipArmor(Armor armor)
    {
        switch (armor.MyArmorType)
        {
            case ArmorType.Helmet:
                head.EquipArmor(armor);                
                break;
            case ArmorType.Shoulders:
                shoulders.EquipArmor(armor);                
                break;
            case ArmorType.Chest:
                chest.EquipArmor(armor);                
                break;
            case ArmorType.Gloves:
                gloves.EquipArmor(armor);                
                break;
            case ArmorType.Legs:
                legs.EquipArmor(armor);                
                break;
            case ArmorType.Feet:
                feet.EquipArmor(armor);                
                break;
            case ArmorType.Back:
                back.EquipArmor(armor);                
                break;
            case ArmorType.Ring:
                if (ring1.IsEmpty == true)
                {
                    ring1.EquipArmor(armor);
                    CharButton.MyInstance.MyEquippedItem = armor.name;
                    break;
                }
                else if (ring2.IsEmpty == true)
                {
                    ring2.EquipArmor(armor);
                    CharButton.MyInstance.MyEquippedItem = armor.name;
                    break;
                }

                else if (ring1.IsEmpty == false) 
                {
                    if (CharButton.MyInstance.CompareItemToEquippedItem(armor) == true)
                    {
                        Debug.Log("Same Item Detected");

                        if (CharacterPanel.MyInstance.ring2.CompareItemToEquippedItem(armor) == false)
                        {
                            Debug.Log("Equipping in ring2");
                            ring2.EquipArmor(armor);
                            CharacterPanel.MyInstance.ring2.MyEquippedItem = armor.name;
                            break;
                        }
                    }
                    else if (CharButton.MyInstance.CompareItemToEquippedItem(armor) == false)
                    {
                        ring1.EquipArmor(armor);
                        CharButton.MyInstance.MyEquippedItem = armor.name;
                        break;
                    }                                               
                }

                else if (ring1.IsEmpty == false && CharButton.MyInstance.CompareItemToEquippedItem(armor) == false)
                {
                    ring1.EquipArmor(armor);
                    CharButton.MyInstance.MyEquippedItem = armor.name;
                    break;
                }

                else if (ring2.IsEmpty == false && CharButton.MyInstance.CompareItemToEquippedItem(armor) == true)
                {
                    Debug.Log("Same Item Detected");
                    if (ring1.IsEmpty == false && CharButton.MyInstance.CompareItemToEquippedItem(armor) == false)
                    {
                        Debug.Log("Equipping in ring1");
                        ring1.EquipArmor(armor);
                        CharButton.MyInstance.MyEquippedItem = armor.name;
                        break;
                    }

                    break;
                }
                else if (ring2.IsEmpty == false && CharButton.MyInstance.CompareItemToEquippedItem(armor) == false)
                {
                    ring2.EquipArmor(armor);
                    CharButton.MyInstance.MyEquippedItem = armor.name;
                    break;
                }
                break;
            case ArmorType.Neck:
                neck.EquipArmor(armor);
                break;
            case ArmorType.MainHand:
                mainhand.EquipArmor(armor);
                break;
            case ArmorType.OffHand:
                offhand.EquipArmor(armor);
                break;
            default:
                break;
        }
    }
}
