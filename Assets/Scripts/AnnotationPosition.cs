using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnotationPosition : MonoBehaviour
{
    public GameObject relatedHighlight;

    void Start()
    {
        this.transform.position = relatedHighlight.transform.position + new Vector3(0, 0.1f, 0);
    }
}
