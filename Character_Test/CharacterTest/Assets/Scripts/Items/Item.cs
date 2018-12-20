﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Quality { Common, Uncommon, Rare, Epic, Legendary, Mythical }

public abstract class Item : ScriptableObject, IMoveable, IDescribable
{
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

    public virtual string GetDescription()
    {
        string color = string.Empty;        

        switch (quality)
        {
            case Quality.Common:
                color = "#d6d6d6";
                UIManager.MyInstance.MyToolTipFrame.sprite = UIManager.MyInstance.CommonQualityFrame;
                break;
            case Quality.Uncommon:
                color = "#00ff3e";
                UIManager.MyInstance.MyToolTipFrame.sprite = UIManager.MyInstance.UncommonQualityFrame;
                break;
            case Quality.Rare:
                color = "#0000e0";
                UIManager.MyInstance.MyToolTipFrame.sprite = UIManager.MyInstance.RareQualityFrame;
                break;
            case Quality.Epic:
                color = "#b900ff";
                UIManager.MyInstance.MyToolTipFrame.sprite = UIManager.MyInstance.EpicQualityFrame;
                break;
            case Quality.Legendary:
                color = "#ff5500";
                UIManager.MyInstance.MyToolTipFrame.sprite = UIManager.MyInstance.LegendaryQualityFrame;
                break;
            case Quality.Mythical:
                color = "#f91111";
                UIManager.MyInstance.MyToolTipFrame.sprite = UIManager.MyInstance.MythicalQualityFrame;
                break;
        }

        return string.Format("<color={0}> {1}</color>", color, title);
    }

    public void Remove()
    {
        if (MySlot != null)
        {
            MySlot.RemoveItem(this);
        }
    }
}
