using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HighlightAndAnnotationManager : MonoBehaviour {
    public GameObject HoverPanel;

    public GameObject OptionalSecondHighlight;

    private void Start()
    {
		Button btn = this.GetComponent<Button>();
		btn.onClick.AddListener(SetAnnotationActive);
    }

	private void SetAnnotationActive()
    {
		Debug.Log ("You have clicked the button!");
        HoverPanel.SetActive(true);
	}

    private void SetOptionalSecondHighlightToHoverState()
    {
        if (OptionalSecondHighlight != null)
        {
            Debug.Log("Hover");
        }        
    }
}
