using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject HoverPanel;

    public GameObject OptionalSecondHighlight;

    public void OnPointerEnter(PointerEventData eventData)
    {
        HoverPanel.SetActive(true);
        // if OptionalSecondHighlight is != null -> apply hover state also
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HoverPanel.SetActive(false);
    }
}
