using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private ArmorType armorType;

    private Armor armor;

    [SerializeField]
    private Image baseIcon;

    [SerializeField]
    private Image baseFrame;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Image rarityFrame;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (HandScript.MyInstance.MyMoveable is Armor)
            {
                Armor tmp = (Armor)HandScript.MyInstance.MyMoveable;

                if (tmp.MyArmorType == armorType)
                {
                    EquipArmor(tmp);
                }
            }
        }
    }

    public void EquipArmor(Armor armor)
    {
        baseIcon.enabled = false;
        baseFrame.enabled = true;
        icon.enabled = true;
        rarityFrame.enabled = true;
        icon.sprite = armor.MyIcon;
        SetRarityFrame(armor);
        this.armor = armor;  // A reference to the equipped armor
        HandScript.MyInstance.DeleteItem();
    }

    private void SetRarityFrame(Armor armor)
    {
        if (armor.MyQuality == Quality.Common)
        {            
            baseFrame.enabled = true;
            rarityFrame.enabled = false;
        }
        if (armor.MyQuality == Quality.Uncommon)
        {
            rarityFrame.color = new Color(83 / 255f, 255f, 0 / 255f, 36 / 255f);
        }

        if (armor.MyQuality == Quality.Rare)
        {
            rarityFrame.color = new Color(0 / 255f, 37 / 255f, 255, 68 / 255f);
        }

        if (armor.MyQuality == Quality.Epic)
        {
            rarityFrame.color = new Color(171 / 255f, 0 / 255f, 191, 36 / 255f);
        }

        if (armor.MyQuality == Quality.Legendary)
        {
            rarityFrame.color = new Color(255, 101 / 255f, 0, 68 / 255f);
        }

        if (armor.MyQuality == Quality.Mythical)
        {
            rarityFrame.color = new Color(255, 0, 0, 58 / 255f);
        }
    }
}
