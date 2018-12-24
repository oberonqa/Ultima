using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler, IPointerExitHandler
{
    private ObservableStack<Item> items = new ObservableStack<Item>();

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Image rarityFrame;

    [SerializeField]
    private Text stackSize;

    // Reference to the bag that this slot belongs to
    public BagScript MyBag { get; set; }

    public bool IsEmpty
    {
        get
        {
            return MyItems.Count == 0;
        }
    }

    public Item MyItem
    {
        get
        {
            if (!IsEmpty)
            {
                return MyItems.Peek();
            }

            return null;
        }
    }

    public bool IsFull
    {
        get
        {
            if (IsEmpty || MyCount < MyItem.MyStackSize)
            {
                return false;
            }

            return true;
        }
    }

    public Image MyIcon
    {
        get
        {
            return icon;
        }

        set
        {
            icon = value;
        }
    }

    public int MyCount
    {
        get { return MyItems.Count; }
    }

    public Text MyStackText
    {
        get
        {
            return stackSize;
        }
    }

    public ObservableStack<Item> MyItems
    {
        get
        {
            return items;
        }
    }

    private void Awake()
    {
        MyItems.OnPop += new UpdateStackEvent(UpdateSlot);
        MyItems.OnPush += new UpdateStackEvent(UpdateSlot);
        MyItems.OnClear += new UpdateStackEvent(UpdateSlot);
    }

    public bool AddItem(Item item)
    {
        MyItems.Push(item);
        icon.sprite = item.MyIcon;
        icon.color = Color.white;        
        item.MySlot = this;
        SetRarityFrame(item);
        return true;
    }

    private void SetRarityFrame(Item item)
    {
        if (!IsEmpty)
        {
            if (item.MyQuality == Quality.Common)
            {
                rarityFrame.enabled = false; 
            }
            if (item.MyQuality == Quality.Uncommon)
            {
                rarityFrame.enabled = true;
                rarityFrame.color = new Color(83 / 255f, 255f, 0 / 255f, 36 / 255f);                
            }

            if (item.MyQuality == Quality.Rare)
            {
                rarityFrame.enabled = true;
                rarityFrame.color = new Color(0 / 255f, 37 / 255f, 255, 68 / 255f);
            }

            if (item.MyQuality == Quality.Epic)
            {
                rarityFrame.enabled = true;
                rarityFrame.color = new Color(171 / 255f, 0 / 255f, 191, 36 / 255f);
            }

            if (item.MyQuality == Quality.Legendary)
            {
                rarityFrame.enabled = true;
                rarityFrame.color = new Color(255, 101 / 255f, 0, 68 / 255f);
            }

            if (item.MyQuality == Quality.Mythical)
            {
                rarityFrame.enabled = true;
                rarityFrame.color = new Color(255, 0, 0, 58 / 255f);
            }         
        }   
    }

    public bool AddItems(ObservableStack<Item> newItems)
    {
        if (IsEmpty || newItems.Peek().GetType() == MyItem.GetType())
        {
            int count = newItems.Count;

            for (int i = 0; i < count; i++)
            {
                if (IsFull)
                {
                    return false;
                }

                AddItem(newItems.Pop());
            }

            return true;
        }

        return false;
    }

    public void RemoveItem(Item item)
    {
        if (!IsEmpty)
        {
            InventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
            rarityFrame.enabled = false;
        }
    }

    public void Clear()
    {
        if (MyItems.Count > 0)
        {
            InventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
            MyItems.Clear();
            rarityFrame.enabled = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (InventoryScript.MyInstance.FromSlot == null && !IsEmpty) // If we don't have something to move
            {
                if (HandScript.MyInstance.MyMoveable != null)
                {
                    if (HandScript.MyInstance.MyMoveable is Bag)
                    {
                        if (MyItem is Bag)
                        {
                            InventoryScript.MyInstance.SwapBags(HandScript.MyInstance.MyMoveable as Bag, MyItem as Bag);
                        }
                    }
                    else if (HandScript.MyInstance.MyMoveable is Armor)
                    {
                        if (MyItem is Armor && (MyItem as Armor).MyArmorType == (HandScript.MyInstance.MyMoveable as Armor).MyArmorType)
                        {
                            (MyItem as Armor).Equip();
                            
                            HandScript.MyInstance.Drop();
                        }
                    }
                }
                else
                {
                    HandScript.MyInstance.TakeMoveable(MyItem as IMoveable);
                    InventoryScript.MyInstance.FromSlot = this;
                }                
            }  
            else if (InventoryScript.MyInstance.FromSlot == null && IsEmpty)
            {
                if (HandScript.MyInstance.MyMoveable is Bag)
                {
                    // De-equip the bag from the inventory
                    Bag bag = (Bag)HandScript.MyInstance.MyMoveable;

                    if (bag.MyBagScript != MyBag && InventoryScript.MyInstance.MyEmptySlotCount - bag.Slots > 0)
                    {
                        AddItem(bag);
                        bag.MyBagButton.RemoveBag();
                        HandScript.MyInstance.Drop();
                    }
                }
                else if (HandScript.MyInstance.MyMoveable is Armor)
                {
                    Armor armor = (Armor)HandScript.MyInstance.MyMoveable;
                    AddItem(armor);
                    CharacterPanel.MyInstance.MySelectedButton.DequipArmor();
                    HandScript.MyInstance.Drop();
                }
            }
            else if (InventoryScript.MyInstance.FromSlot != null) // If we have something to move
            {
                if (PutItemBack() || MergeItems(InventoryScript.MyInstance.FromSlot) || SwapItems(InventoryScript.MyInstance.FromSlot) || AddItems(InventoryScript.MyInstance.FromSlot.MyItems))
                {
                    HandScript.MyInstance.Drop();                    
                    InventoryScript.MyInstance.FromSlot = null;
                }                
            }
            if (eventData.button == PointerEventData.InputButton.Right && HandScript.MyInstance.MyMoveable != null)
            {
                UseItem();
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
    }

    public void UseItem()
    {
        if (MyItem is IUseable)
        {
            (MyItem as IUseable).Use();
            SetRarityFrame(MyItem);
        } 
        else if (MyItem is Armor)
        {
            (MyItem as Armor).Equip();
        }
    }

    public bool StackItem(Item item)
    {
        if (!IsEmpty && item.name == MyItem.name && MyItems.Count < MyItem.MyStackSize)
        {
            MyItems.Push(item);
            item.MySlot = this;
            return true;
        }

        return false;
    }

    private bool PutItemBack()
    {
        if (InventoryScript.MyInstance.FromSlot == this)
        {
            InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            return true;
        }

        return false;
    }

    private bool SwapItems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }
        if (from.MyItem.GetType() != MyItem.GetType() || from.MyCount + MyCount > MyItem.MyStackSize)
        {
            // Copy all the items we need to swap from slot A
            ObservableStack<Item> tmpFrom = new ObservableStack<Item>(from.MyItems);

            // Clear slot A
            from.MyItems.Clear();
            from.rarityFrame.enabled = false;

            // Take all items from Slot B and copy them into slot A
            from.AddItems(MyItems);

            // Clear slot B
            MyItems.Clear();            

            // Move the items from Copy A to B
            AddItems(tmpFrom);            

            return true;
        }

        return false;
    }

    private bool MergeItems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }
        if (from.MyItem.GetType() == MyItem.GetType() && !IsFull)
        {
            // How many free slots do we have in the stack?
            int free = MyItem.MyStackSize - MyCount;

            for (int i = 0; i < free; i++)
            {
                AddItem(from.MyItems.Pop());
            }

            return true;
        }

        return false;
    }

    private void UpdateSlot()
    {        
        UIManager.MyInstance.UpdateStackSize(this);
        if (IsEmpty)
        {
            rarityFrame.enabled = false;
        }        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Show tooltip
        if (!IsEmpty)
        {
            UIManager.MyInstance.ShowTooltip("generic", new Vector2(1, 0), transform.position, MyItem);
        }        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData == null)
        {
            UIManager.MyInstance.MyToolTipFrame.sprite = UIManager.MyInstance.DefaultFrame;
        }
        
        UIManager.MyInstance.HideTooltip();        
    }
}
