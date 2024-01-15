using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HighlightAndAnnotationManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject relatedAnnotation;

    public GameObject optionalSecondHighlight;

    private GameObject currentlyActiveAnnotation;

    private Image image;

    private Color defaultColor = new Color(1.0f, 1.0f, 0.0f, 1.0f);

    private Color hoverColor = new Color(1.0f, 0.97f, 0.0f, 1.0f);

    void Start()
    {
		this.image = this.GetComponent<Image>();
    }

    void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger) || OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            this.currentlyActiveAnnotation.SetActive(false);
        }
    }

	public void ActivateAnnotation()
    {
        relatedAnnotation.SetActive(true);
        this.currentlyActiveAnnotation = relatedAnnotation;
	}

    public void ActivateHoverState()
    {
        image.color = hoverColor;
        if (optionalSecondHighlight != null)
        {
            optionalSecondHighlight.GetComponent<Image>().color = hoverColor;
        }        
    }

    public void DeactivateHoverState()
    {
        image.color = defaultColor;
        if (optionalSecondHighlight != null)
        {
            optionalSecondHighlight.GetComponent<Image>().color = defaultColor;
        }        
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        ActivateHoverState();
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        DeactivateHoverState();
    }
}
