﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Item : ScriptableObject, IMoveable, IDescribable
{
    private static Item instance;

    public static Item MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Item>();
            }

            return instance;
        }
    }

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int stackSize;

    [SerializeField]
    private string title;

    [SerializeField]
    private Quality quality;

    private SlotScript slot;

    public Quality MyQuality
    {
        get
        {
            return quality;
        }
    }

    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
    }

    public int MyStackSize
    {
        get
        {
            return stackSize;
        }
    }

    public SlotScript MySlot
    {
        get
        {
            return slot;
        }

        set
        {
            slot = value;
        }
    }

    public string MyTitle
    {
        get
        {
            return title;
        }
    }

    public virtual string GetDescription()
    {
        switch (quality)
        {
            case Quality.Common:                
                UIManager.MyInstance.MyToolTipFrame.sprite = UIManager.MyInstance.CommonQualityFrame;
                break;
            case Quality.Uncommon:                
                UIManager.MyInstance.MyToolTipFrame.sprite = UIManager.MyInstance.UncommonQualityFrame;
                break;
            case Quality.Rare:                
                UIManager.MyInstance.MyToolTipFrame.sprite = UIManager.MyInstance.RareQualityFrame;
                break;
            case Quality.Epic:                
                UIManager.MyInstance.MyToolTipFrame.sprite = UIManager.MyInstance.EpicQualityFrame;
                break;
            case Quality.Legendary:                
                UIManager.MyInstance.MyToolTipFrame.sprite = UIManager.MyInstance.LegendaryQualityFrame;
                break;
            case Quality.Mythical:                
                UIManager.MyInstance.MyToolTipFrame.sprite = UIManager.MyInstance.MythicalQualityFrame;
                break;
        }

        return string.Format("<color={0}> {1}</color>", QualityColor.MyColors[quality], MyTitle);
    }

    public void Remove()
    {
        if (MySlot != null)
        {
            MySlot.RemoveItem(this);
        }
    }
}
