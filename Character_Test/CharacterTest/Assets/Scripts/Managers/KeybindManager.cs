using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KeybindManager : MonoBehaviour
{

    private static KeybindManager instance;

    public static KeybindManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<KeybindManager>();
            }

            return instance;
        }
    }    

    public Dictionary<string, KeyCode> Keybinds { get; private set; }

    public Dictionary<string, KeyCode> ActionBinds { get; private set; }

    private string bindName;

    // Use this for initialization
    void Start ()
    {
        Keybinds = new Dictionary<string, KeyCode>();

        ActionBinds = new Dictionary<string, KeyCode>();

        BindKey("UP", KeyCode.W);
        BindKey("LEFT", KeyCode.A);
        BindKey("DOWN", KeyCode.S);
        BindKey("RIGHT", KeyCode.D);

        BindKey("ACT1", KeyCode.Alpha1);        
        BindKey("ACT2", KeyCode.Alpha2);
        BindKey("ACT3", KeyCode.Alpha3);
        BindKey("ACT4", KeyCode.Alpha4);
        BindKey("ACT5", KeyCode.Alpha5);
        BindKey("ACT6", KeyCode.Alpha6);
        BindKey("ACT7", KeyCode.Alpha7);
        BindKey("ACT8", KeyCode.Alpha8);
        BindKey("ACT9", KeyCode.Alpha9);

        BindKey("KEYBIND", KeyCode.Escape);
        BindKey("SPELLBOOK", KeyCode.P);
        BindKey("BAGS", KeyCode.B);
    }
	
    public void BindKey(string key, KeyCode keyBind)
    {
        Dictionary<string, KeyCode> currentDictionary = Keybinds;        

        if (key.Contains("ACT"))
            {
                currentDictionary = ActionBinds;
            }

        if (!currentDictionary.ContainsKey(key))
        {
            currentDictionary.Add(key, keyBind);
            UIManager.MyInstance.UpdateKeyText(key, keyBind);            
        }
        else if (currentDictionary.ContainsValue(keyBind))
        {
            string myKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;
            currentDictionary[myKey] = KeyCode.None;
            UIManager.MyInstance.UpdateKeyText(key, KeyCode.None);
        }

        currentDictionary[key] = keyBind;
        UIManager.MyInstance.UpdateKeyText(key, keyBind);
        bindName = string.Empty;        
    }

    public void KeyBindOnClick(string bindName)
    {
        Text txt = Array.Find(UIManager.MyInstance.MyKeybindButtons, x => x.name == bindName).GetComponentInChildren<Text>();
        txt.text = "Press a key";
        this.bindName = bindName;
    }

    private void OnGUI()
    {
        if (bindName != string.Empty)
        {
            Event e = Event.current;

            if (e.isKey)
            {
                BindKey(bindName, e.keyCode);
            }
        }
    }
}
