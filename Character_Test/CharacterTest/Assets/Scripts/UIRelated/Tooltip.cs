using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;

    public static Tooltip MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Tooltip>();
            }

            return instance;
        }
    }

    Image MyRarityFrame
    {
        get;
        set;
    }
    Image MyIcon
    {
        get;
        set;
    }
    Text MyItemName
    {
        get;
        set;
    }
    Text MyItemQuality
    {
        get;
        set;
    }
    Text MyItemStats
    {
        get;
        set;
    }
    Text MyItemModifiers
    {
        get;
        set;
    }
    
    public virtual void SetRarityFrame()
    {
        switch (Item.MyInstance.MyQuality)
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
    }
}
