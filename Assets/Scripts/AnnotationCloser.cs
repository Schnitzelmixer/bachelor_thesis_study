using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnnotationCloser : MonoBehaviour
{
    private GameObject annotation;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger) || OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            this.annotation.SetActive(false);
        }
    }

    public void saveCurrentlyActiveAnnotation(GameObject annotation)
    {
        this.annotation = annotation;
    }
}