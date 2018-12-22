using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellButton : MonoBehaviour, IPointerClickHandler
{
    private static SpellButton instance;

    public static SpellButton MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpellButton>();
            }

            return instance;
        }
    }

    public bool isMovingSpell
    {
        get
        {
            return movingSpell;
        }

        set
        {
            movingSpell = value;
        }
    }

    private bool movingSpell = false;

    [SerializeField]
    private string spellName;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            HandScript.MyInstance.TakeMoveable(SpellBook.MyInstance.GetSpell(spellName));
            movingSpell = true;
        }
    }
}
