using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }

    [SerializeField]
    private ActionButton[] actionButtons;

    [SerializeField]
    private GameObject targetFrame;

    private Stat healthStat;

    [SerializeField]
    private Image portraitFrame;

    [SerializeField]
    private GameObject toolTip;

    private Text toolTipTitle;

    [SerializeField]
    private CanvasGroup keybindMenu;

    [SerializeField]
    private CanvasGroup spellBook;

    [SerializeField]
    private Animator spellBarAnim;

    private GameObject[] keybindButtons;

    public GameObject[] MyKeybindButtons
    { 
        get
        {
            return keybindButtons;
        }     
    }


    private void Awake()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
        spellBarAnim = GameObject.FindGameObjectWithTag("SpellBarAnim").GetComponent<Animator>();
        toolTipTitle = toolTip.GetComponentInChildren<Text>();
    }

    // Use this for initialization
    void Start ()
    {        
        healthStat = targetFrame.GetComponentInChildren<Stat>();
    }
	
	// Update is called once per frame
	void Update ()
    {        
        if (Input.GetKeyDown(KeybindManager.MyInstance.Keybinds["KEYBIND"]))
        {
            OpenClose(keybindMenu);
            Time.timeScale = Time.timeScale > 0 ? 0 : 1;
        }
        if (Input.GetKeyDown(KeybindManager.MyInstance.Keybinds["SPELLBOOK"]))
        {
            OpenClose(spellBook);
            Time.timeScale = Time.timeScale > 0 ? 0 : 1;
        }

        if (Input.GetKeyDown(KeybindManager.MyInstance.Keybinds["BAGS"]))
        {
            InventoryScript.MyInstance.OpenClose();
        }
    }

    public void ShowTargetFrame(NPC target)
    {
        targetFrame.SetActive(true);

        healthStat.Initialize(target.MyHealth.MyCurrentValue, target.MyHealth.MyMaxValue);

        portraitFrame.sprite = target.MyPortrait;

        // Listen to HealthChanged event to update target frame
        target.healthChanged += new HealthChanged(UpdateTargetFrame);

        // Hide target frame when target dies
        target.characterRemoved += new CharacterRemoved(HideTargetFrame);
    }

    public void HideTargetFrame()
    {
        targetFrame.SetActive(false);
    }

    public void UpdateTargetFrame(float health)
    {
        healthStat.MyCurrentValue = health;
    }

    public void ClickActionButton(string buttonName)
    {
        Array.Find(actionButtons, x => x.gameObject.name == buttonName).MyButton.onClick.Invoke();

    }

    public void SpeedDownSpellBarAnim()
    {
        float speed = spellBarAnim.speed;

        if (speed >= 0f)
        {
            spellBarAnim.speed = speed - .2f;
        }
    }

    public void SpeedUpSpellBarAnim()
    {
        float speed = spellBarAnim.speed;

        spellBarAnim.speed = speed + .2f;

        if (speed <= .25f)
        {
            spellBarAnim.speed = .25f;
        }
    }

    public void OpenClose(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
    }

    public void UpdateStackSize(IClickable clickable)
    {
        if (clickable.MyCount > 1)
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }

        else
        {
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
            clickable.MyIcon.color = Color.white;
        }

        if (clickable.MyCount == 0)
        {
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
        }
    }

    public void ShowTooltip(Vector3 position, IDescribable description)
    {
        toolTip.SetActive(true);
        toolTip.transform.position = position;
        toolTipTitle.text = description.GetDescription();        
    }

    public void HideTooltip()
    {
        toolTip.SetActive(false);
    }

    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
        if (tmp.text == "Alpha1")
        {
            tmp.text = "1";
        }
        if (tmp.text == "Alpha2")
        {
            tmp.text = "2";
        }
        if (tmp.text == "Alpha3")
        {
            tmp.text = "3";
        }
        if (tmp.text == "Alpha4")
        {
            tmp.text = "4";
        }
        if (tmp.text == "Alpha5")
        {
            tmp.text = "5";
        }
        if (tmp.text == "Alpha6")
        {
            tmp.text = "6";
        }
        if (tmp.text == "Alpha7")
        {
            tmp.text = "7";
        }
        if (tmp.text == "Alpha8")
        {
            tmp.text = "8";
        }
        if (tmp.text == "Alpha9")
        {
            tmp.text = "9";
        }
        if (tmp.text == "Alpha0")
        {
            tmp.text = "0";
        }
        if (tmp.text == "Minus")
        {
            tmp.text = "-";
        }
        if (tmp.text == "Equals")
        {
            tmp.text = "=";
        }
        if (tmp.text == "BackQuote")
        {
            tmp.text = "`";
        }
        if (tmp.text == "LeftBracket")
        {
            tmp.text = "[";
        }
        if (tmp.text == "RightBracket")
        {
            tmp.text = "]";
        }        
        if (tmp.text == "Quote")
        {
            tmp.text = "'";
        }
        if (tmp.text == "Semicolon")
        {
            tmp.text = ";";
        }
        if (tmp.text == "Slash")
        {
            tmp.text = "/";
        }
        if (tmp.text == "Period")
        {
            tmp.text = ".";
        }
        if (tmp.text == "Comma")
        {
            tmp.text = ",";
        }
    }
}
