using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Bag bag;

    [SerializeField]
    private Sprite full, empty;

    public Bag MyBag
    {
        get
        {
            return bag;
        }

        set
        {
            if (value != null)
            {
                GetComponent<Image>().sprite = full;
            }
            else
            {
                GetComponent<Image>().sprite = empty;
            }

            bag = value;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (InventoryScript.MyInstance.FromSlot != null && HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is Bag)
            {
                if (MyBag != null)
                {
                    InventoryScript.MyInstance.SwapBags(MyBag, HandScript.MyInstance.MyMoveable as Bag);
                }

                else
                {
                    Bag tmp = (Bag)HandScript.MyInstance.MyMoveable;
                    tmp.MyBagButton = this;
                    tmp.Use();
                    MyBag = tmp;
                    HandScript.MyInstance.Drop();
                    InventoryScript.MyInstance.FromSlot = null;
                }
            }

            else if (bag != null)
            {
                bag.MyBagScript.OpenClose();
            }
        }

        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            HandScript.MyInstance.TakeMoveable(MyBag);
        }        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Show tooltip
        if (bag != null)
        {
            UIManager.MyInstance.ShowTooltip("generic", new Vector2(1, 0), transform.position, MyBag);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }

    public void RemoveBag()
    {
        InventoryScript.MyInstance.RemoveBag(MyBag);
        MyBag.MyBagButton = null;

        foreach (Item item in MyBag.MyBagScript.GetItems())
        {
            InventoryScript.MyInstance.AddItem(item);
        }

        MyBag = null;
    }
}
