using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Text btnText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        btnText.color = Color.red; 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        btnText.color = Color.white;
    }
}