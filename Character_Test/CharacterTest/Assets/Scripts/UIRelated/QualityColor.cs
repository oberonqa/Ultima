using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Quality { Common, Uncommon, Rare, Epic, Legendary, Mythical }

public static class QualityColor
{
    private static Dictionary<Quality, string> colors = new Dictionary<Quality, string>()
    {
        {Quality.Common, "#d6d6d6" },
        {Quality.Uncommon, "#00ff3e" },
        {Quality.Rare, "#0000e0" },
        {Quality.Epic, "#b900ff" },
        {Quality.Legendary, "#ff5500" },
        {Quality.Mythical, "#f91111" }
    };

    public static Dictionary<Quality, string> MyColors
    {
        get
        {
            return colors;
        }
    }
}
