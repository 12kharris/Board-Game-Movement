using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]

public class SquarePrice : MonoBehaviour
{
    [SerializeField] int squareCost;
    int squareCharge;
    public int SquareCharge {get {return squareCharge;}}

    Balance balance;

    public int SquareCost {get {return squareCost;}}

    TextMeshPro squareLabel;
    
    void Start() 
    {
        
        squareLabel = GetComponent<TextMeshPro>();

        SetSquareCharge();
    }

    void SetSquareCharge()
    {
        squareCharge = Mathf.RoundToInt(squareCost * 0.1f);
        squareLabel.text = squareCharge.ToString();
    }

}
