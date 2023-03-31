using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishColorScript : MonoBehaviour
{
    public float saturation = 1f; 
    public float brightness = 1f; 

    void Start()
    {
        foreach (Transform child in transform)
        {
            Color randomColor = Random.ColorHSV(0f, 1f, saturation, saturation, 
                brightness, brightness);
            child.GetComponent<Renderer>().material.color = randomColor; 
        }
    }
}
