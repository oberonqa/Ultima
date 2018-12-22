using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LootButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text title;

    private LootWindow lootWindow;

    public Image MyIcon
    {
        get
        {
            return icon;
        }
    }

    public Text MyTitle
    {
        get
        {
            return title;
        }
    }

    public Item MyLoot { get; set; }

    private void Awake()
    {
        lootWindow = GetComponentInParent<LootWindow>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // We need to loot the item right here   
        Item loot = Instantiate(MyLoot);
        if (InventoryScript.MyInstance.AddItem(loot))
        {
            gameObject.SetActive(false);
            lootWindow.TakeLoot(loot);
            UIManager.MyInstance.HideTooltip();
        }        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.MyInstance.ShowTooltip(transform.position, MyLoot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}
