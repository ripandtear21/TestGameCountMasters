using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GatesScript : MonoBehaviour
{
    [SerializeField] private int randomNumber;
    [SerializeField] private bool mult;
    [SerializeField] private TextMeshPro textGate;
    public bool Multiply
    {
        get => mult;
        set => mult = value;
    }
    public int RandomNumber
    {
        get => randomNumber;
        set => randomNumber = value;
    }
    void Start()
    {
        if (mult)
        {
            randomNumber = Random.Range(1, 3);
            textGate.text = "x" + randomNumber;
        }
        else
        {
            randomNumber = Random.Range(10, 100);

            if (randomNumber % 2 != 0)
                randomNumber += 1;
            
            textGate.text = randomNumber.ToString();
        }
    }
}
