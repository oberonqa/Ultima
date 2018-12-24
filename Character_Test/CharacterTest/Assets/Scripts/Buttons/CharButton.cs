using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private static CharButton instance;
    public static CharButton MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CharButton>();
            }

            return instance;
        }
    }

    [SerializeField]
    private ArmorType armorType;

    private Armor equippedArmor;

    [SerializeField]
    private Image baseIcon;

    [SerializeField]
    private Image baseFrame;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Image rarityFrame;

    private bool isEmpty = true;

    private string equippedItem = "";

    public bool IsEmpty
    {
        get
        {
            return isEmpty;
        }

        set
        {
            isEmpty = value;
        }
    }

    public string MyEquippedItem
    {
        get
        {
            return equippedItem;
        }

        set
        {
            equippedItem = value;
        }
    }

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

                UIManager.MyInstance.RefreshTooltip(tmp);
            }

            else if (HandScript.MyInstance.MyMoveable == null && equippedArmor != null)
            {
                HandScript.MyInstance.TakeMoveable(equippedArmor);
                CharacterPanel.MyInstance.MySelectedButton = this;
                icon.color = Color.grey;
            }
        }        
    }

    public void EquipArmor(Armor armor)
    {
        if (MyEquippedItem == armor.name)
        {
            Debug.Log("Error!");            
        }
        else
        {
            armor.Remove();

            if (equippedArmor != null)
            {
                if (equippedArmor != armor)
                {
                    armor.MySlot.AddItem(equippedArmor);
                }
                
                UIManager.MyInstance.RefreshTooltip(equippedArmor);
            }
            else
            {
                UIManager.MyInstance.HideTooltip();
            }
            baseIcon.enabled = false;
            baseFrame.enabled = true;
            icon.enabled = true;
            rarityFrame.enabled = true;
            icon.sprite = armor.MyIcon;
            SetRarityFrame(armor);
            icon.color = Color.white;
            this.equippedArmor = armor;  // A reference to the equipped armor

            if (HandScript.MyInstance.MyMoveable == (armor as IMoveable))
            {
                HandScript.MyInstance.Drop();
            }

            isEmpty = false;
        }
    }

    public bool CompareItemToEquippedItem(Armor armor)
    {
        if (armor.name == equippedItem)
        {
            return true;
        }

        return false;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (equippedArmor != null)
        {
            UIManager.MyInstance.ShowTooltip("generic", new Vector2(0, 0), transform.position, equippedArmor);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }

    public void DequipArmor()
    {
        icon.color = Color.white;
        icon.enabled = false;
        equippedArmor = null;
        baseIcon.enabled = true;
        baseFrame.enabled = false;
        rarityFrame.enabled = false;
    }
}
