using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler, IPointerExitHandler
{
    
    public IUseable MyUseable { get; set; }

    [SerializeField]
    private Text stackSize;

    private Stack<IUseable> useables = new Stack<IUseable>();

    private int count;

    private bool slottedSpell = false;
    private bool slottedItem = false;

    public Button MyButton { get; private set; }

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
        get
        {
            return count;
        }
    }

    public Text MyStackText
    {
        get
        {
            return stackSize;
        }
    }    

    [SerializeField]
    private Image icon;

    // Use this for initialization
    void Start ()
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OnClick);
        InventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(UpdateItemCount);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnClick()
    {
        if (HandScript.MyInstance.MyMoveable == null)
        {
            if (MyUseable != null)
            {
                MyUseable.Use();
            }
            if (useables != null && useables.Count > 0)
            {
                useables.Peek().Use();
            }
        }        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is IUseable)
            {
                if (SpellButton.MyInstance.isMovingSpell)
                {
                    if (MyButton.name == "ACT1" || MyButton.name == "ACT2" || MyButton.name == "ACT3")
                    {
                        SetUseable(HandScript.MyInstance.MyMoveable as IUseable);
                        SpellButton.MyInstance.isMovingSpell = false;
                        slottedSpell = true;
                    }
                }
                if (!SpellButton.MyInstance.isMovingSpell)
                {
                    if (MyButton.name == "ACT4" || MyButton.name == "ACT5" || MyButton.name == "ACT6" ||
                        MyButton.name == "ACT7" || MyButton.name == "ACT8" || MyButton.name == "ACT9")
                    {
                        SetUseable(HandScript.MyInstance.MyMoveable as IUseable);
                        slottedItem = true;
                    }
                }            
            }                        
        }
    }

    public void SetUseable(IUseable useable)
    {
        if (useable is Item)
        {
            useables = InventoryScript.MyInstance.GetUseables(useable);
            count = useables.Count;
            InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            InventoryScript.MyInstance.FromSlot = null;
        }
        else
        {
            this.MyUseable = useable;
        }        

        UpdateVisual();
    }

    public void UpdateVisual()
    {
        if (!slottedSpell)
        {
            MyIcon.sprite = HandScript.MyInstance.Put().MyIcon;
            MyIcon.color = Color.white;
        }
        
        if (count > 1)
        {
            UIManager.MyInstance.UpdateStackSize(this);
        }
    }

    public void UpdateItemCount(Item item)
    {
        if (item is IUseable && useables.Count > 0)
        {
            if (useables.Peek().GetType() == item.GetType())
            {
                useables = InventoryScript.MyInstance.GetUseables(item as IUseable);

                count = useables.Count;

                UIManager.MyInstance.UpdateStackSize(this);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IDescribable tmp = null;

        if (MyUseable != null && MyUseable is IDescribable)
        {
            tmp = (IDescribable)MyUseable;
            // Need to impliment!
            // UIManager.MyInstance.ShowTooltip(transform.position);
        }
        else if (useables.Count > 0)
        {
            // Need to impliment!
            // UIManager.MyInstance.ShowTooltip(transform.position);
        }
        if (tmp != null)
        {
            UIManager.MyInstance.MyToolTipFrame.sprite = UIManager.MyInstance.DefaultFrame;
            UIManager.MyInstance.ShowTooltip("generic", new Vector2(1,0), transform.position, tmp);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
        UIManager.MyInstance.MyToolTipFrame.sprite = UIManager.MyInstance.DefaultFrame;
    }
}
